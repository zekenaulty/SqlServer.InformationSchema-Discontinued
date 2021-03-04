using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

using Microsoft.CSharp;
using Microsoft.VisualBasic;

using SqlServer.Companion;
using SqlServer.Companion.Schema;


namespace SqlServer.Companion.CodeDom
{
	/// <summary></summary>
	public sealed class SqlBusinessObjectGenerator
	{
		internal static void GenerateFieldProperty(CodeTypeDeclaration @ref, SqlDbTableField field, SqlDbTableConstraintCollection table_constraints)
		{
			#region Variables
			//create property member
			CodeMemberField member = new CodeMemberField(field.ValueType, "_" + field.ColumnName);

			//create property
			CodeMemberProperty property = new CodeMemberProperty();

			property.Attributes = MemberAttributes.Public | MemberAttributes.Final | MemberAttributes.New;
			bool allowSet = true;
			property.Comments.Add(new CodeCommentStatement("----------------------------------------------------------"));
			#endregion
			#region Mark as table field
			//create table field attribute
			CodeAttributeDeclaration fldAttrib = new CodeAttributeDeclaration("SqlServer.Companion.TableRowField");

			//set attibute up
			fldAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.ColumnName)));
			fldAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.DataType)));
			fldAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression((field.IsNullable.ToUpper() == "YES") ? true : false)));

			if(field.CharacterMaximumLength != int.MinValue)
				fldAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.CharacterMaximumLength)));
			else
				fldAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.NumericPrecision)));
			
			fldAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.OridinalPosition)));

			property.CustomAttributes.Add(fldAttrib); //add attribute to property
			#endregion
			#region Do Primary and Foreign Key Special Case
			foreach(SqlDbTableConstraint tc in table_constraints)
			{
				if(tc.ConstraintColumnName == field.ColumnName)
				{
					if(tc.IsPrimaryKey)
					{
						allowSet = false;
						//GenerateIsNew(@ref, field);

						property.Comments.Add(new CodeCommentStatement("\tPRIMARY KEY"));
						property.CustomAttributes.Add(new CodeAttributeDeclaration("SqlServer.Companion.TableRowPrimaryKey"));
					}
					else if(tc.IsForeignKey)
					{
						property.Comments.Add(new CodeCommentStatement("\tFOREIGN KEY"));
						
						string tbl = "";
						string col = "";
						foreach(SqlDbTableFieldConstraint fc in field.Constraints)
						{
							if(tc.ConstraintName == fc.ConstraintName)
							{
								if(null != fc.ReferentialConstraint && null != fc.ReferentialConstraint.UniqueConstraint)
								{
									tbl = fc.ReferentialConstraint.UniqueConstraint.TableName;
									col = fc.ReferentialConstraint.UniqueConstraint.ColumnName;
								}
							}
						}
						if(tbl != "" && col != "")
						{
							CodeAttributeDeclaration fk = new CodeAttributeDeclaration("SqlServer.Companion.TableRowForeignKey");
							fk.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tbl)));
							fk.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(col)));
							property.CustomAttributes.Add(fk);
						}
					}
				}
			}
			#endregion
			#region Set Property Data
			string c = "\t";
			//property.CustomAttributes.Add(xmlEl);
			property.Comments.Add(new CodeCommentStatement("\tCatalog: " + field.TableCatalog + ", Table: " + field.TableName));
			property.Comments.Add(new CodeCommentStatement("\tField: " + field.ColumnName + ", DataType: " + field.DataType));

			property.Comments.Add( new CodeCommentStatement("Gets/Sets the value of " + field.ColumnName + ".", true));
			if(field.ValueType != typeof(string))
				c += "Precision: " + field.NumericPrecision.ToString();
			else
				c += "Length: " + field.CharacterMaximumLength.ToString();

			property.Comments.Add(new CodeCommentStatement(c));
			
			string name = field.ColumnName.Substring(0, 1).ToUpper();
			
			if(field.ColumnName.Length > 1)
				name += field.ColumnName.Substring(1);

			property.Name = name;	//assign prpoerty name
			property.Type = new CodeTypeReference(field.ValueType.FullName); //assign property type
			//property.Attributes = MemberAttributes.Public;
			#endregion
			#region Get

			property.GetStatements.Add( new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeBaseReferenceExpression(), "_" + field.ColumnName)));
			
			#endregion
			#region Set

			if(allowSet)
			{
				property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeBaseReferenceExpression(), member.Name), new CodeVariableReferenceExpression("value")));
				
				CodeMethodInvokeExpression dirty = new CodeMethodInvokeExpression();
				dirty.Method = new CodeMethodReferenceExpression(new CodeBaseReferenceExpression(), "MarkDirty");

				property.SetStatements.Add(dirty);//new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "MarkDirty", null));
			}
			else
			{
				property.Comments.Add(new CodeCommentStatement("This property can be set but it should never be set. It is allowed to be set to support XmlSerialization and only for that reason.", true));
				property.SetStatements.Add(new CodeCommentStatement("This set should not be here, but in order to fully serialize the object all properties that will be serialized must be read/write. I leave it to the developer to know better than to set the PK."));
				property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeBaseReferenceExpression(), member.Name), new CodeVariableReferenceExpression("value")));
			}
			#endregion

			property.Comments.Add(new CodeCommentStatement("----------------------------------------------------------"));
			@ref.Members.Add(property);
		}
		public static CodeTypeDeclaration GenerateBusiness(
			CodeCompileUnit unit,
			SqlDbTable tbl,
			string @namespace,
			string dataNamespace)
		{
			//business namespace
			CodeNamespace nspace = new CodeNamespace(@namespace);
			CodeTypeDeclaration obj = new CodeTypeDeclaration(tbl.TableName);
			System.CodeDom.CodeConstructor objCon = new CodeConstructor();

			obj.IsClass = true;
			
			obj.Attributes = MemberAttributes.Public;
			obj.TypeAttributes = TypeAttributes.Public | TypeAttributes.Class;
			obj.BaseTypes.Add(dataNamespace + "." + tbl.TableName);
			obj.BaseTypes.Add(SqlInterfaceGenerator.GetInterfaceName(tbl, @namespace));

			#region Class Using Statements
			//add system imports/using
			nspace.Imports.Add(new CodeNamespaceImport("System"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Collections"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.Common"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.SqlTypes"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Xml"));
			
			//add SqlServer.Companion imports/using
			nspace.Imports.Add(new CodeNamespaceImport("SqlServer.Companion"));
			nspace.Imports.Add(new CodeNamespaceImport("SqlServer.Companion.Base"));
			#endregion

			CodeAttributeDeclaration xmlSerial = new CodeAttributeDeclaration("System.Xml.Serialization.XmlRootAttribute");
			xmlSerial.Arguments.Add(new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(tbl.TableName)));
			xmlSerial.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression("")));
			xmlSerial.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(false)));
			obj.CustomAttributes.Add(xmlSerial);
			
			//foreach(SqlDbTableField f in tbl.Columns)
				//GenerateFieldProperty(obj, f, tbl.Constraints);

			GenerateFromBase(obj, tbl, @namespace, dataNamespace);

			nspace.Types.Add(obj);
			unit.Namespaces.Add(nspace);
			return obj;

		}
		internal static void GenerateFromBase(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace,
			string dataNamespace)
		{
			CodeMemberMethod fromBase = new CodeMemberMethod();

			string typeName = @namespace + "." + tbl.TableName;
			string dataTypeName = dataNamespace + "." + tbl.TableName;

			fromBase.Name = "FromBase";
			fromBase.ReturnType = new CodeTypeReference(typeName);
			fromBase.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final;
			fromBase.Parameters.Add(new CodeParameterDeclarationExpression(dataTypeName, "data"));

			fromBase.Statements.Add(new CodeVariableDeclarationStatement(typeName, "ret", new CodeObjectCreateExpression(typeName, new CodeExpression[0])));

			//loop through the fields adding members and properties for each
			foreach(SqlDbTableField f in tbl.Columns)
			{
				CodeAssignStatement fb = new CodeAssignStatement();				
				//set the target
				fb.Left	= new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("ret"), "_" + f.ColumnName); 
				//set the source
				fb.Right =	new CodeFieldReferenceExpression(new CodeVariableReferenceExpression("data"), f.ColumnName);

				fromBase.Statements.Add(fb);
			}
			fromBase.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));

			@ref.Members.Add(fromBase);
		}


		public static void GenerateFindOne(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string dataNamespace,
			string procName)
		{
			string bObj = @namespace + "." + tbl.TableName;
			string dObj = dataNamespace + "." + tbl.TableName;

			CodeMemberMethod find = new CodeMemberMethod();

			find.Name = procName;
			find.ReturnType = new CodeTypeReference(bObj);
			find.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final | MemberAttributes.New;
			CodeExpression[] methodFilters = new CodeExpression[]{};

			find.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SqlServer.Companion.SqlConnectionSource), "conn"));

			ArrayList paramRef = new ArrayList();
			paramRef.Add(new CodeVariableReferenceExpression("conn"));
			foreach(SqlDbTableField fld in filters)
			{
				string vname = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
				find.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, vname));
				paramRef.Add(new CodeVariableReferenceExpression(vname));
			}

			methodFilters = new CodeExpression[paramRef.Count];
			for(int i = 0; i < paramRef.Count; i++)
				methodFilters[i] = (CodeExpression)paramRef[i];

			CodeMethodInvokeExpression callDataProc = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(dObj), procName), methodFilters);
			CodeMethodInvokeExpression callFromBase = new CodeMethodInvokeExpression(new CodeMethodReferenceExpression(new CodeTypeReferenceExpression(bObj), "FromBase"), new CodeExpression[]{callDataProc});
			CodeMethodReturnStatement ret = new CodeMethodReturnStatement(callFromBase);

			find.Statements.Add(ret);
			@ref.Members.Add(find);
		}

		public static void GenerateFindMany(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string dataNamespace,
			string procName)
		{
			string bObj = @namespace + "." + tbl.TableName;
			string bObjCol = @namespace + "." + tbl.TableName + "Collection";
			string dObj = dataNamespace + "." + tbl.TableName;

			CodeMemberMethod find = new CodeMemberMethod();

			find.Name = procName;
			find.ReturnType = new CodeTypeReference(bObjCol);
			find.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final | MemberAttributes.New;
			
			CodeExpression[] methodFilters = new CodeExpression[]{};

			find.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SqlServer.Companion.SqlConnectionSource), "conn"));

			ArrayList paramRef = new ArrayList();
			paramRef.Add(new CodeVariableReferenceExpression("conn"));
			foreach(SqlDbTableField fld in filters)
			{
				string vname = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
				find.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, vname));
				paramRef.Add(new CodeVariableReferenceExpression(vname));
			}

			methodFilters = new CodeExpression[paramRef.Count];
			for(int i = 0; i < paramRef.Count; i++)
				methodFilters[i] = (CodeExpression)paramRef[i];

			CodeMethodInvokeExpression callDataProc = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(dObj), procName, methodFilters);
			CodeMethodInvokeExpression callFromBase = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(bObjCol), "FromBaseArray", new CodeExpression[]{callDataProc});
			CodeMethodReturnStatement ret = new CodeMethodReturnStatement(callFromBase);

			find.Statements.Add(ret);
			@ref.Members.Add(find);
		}

		public static void GenerateFindManyPaged(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string dataNamespace,
			string procName)
		{
			string bObj = @namespace + "." + tbl.TableName;
			string bObjCol = @namespace + "." + tbl.TableName + "Collection";
			string dObj = dataNamespace + "." + tbl.TableName;

			CodeMemberMethod find = new CodeMemberMethod();

			find.Name = procName;
			find.ReturnType = new CodeTypeReference(bObjCol);
			find.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final | MemberAttributes.New;
			
			CodeExpression[] methodFilters = new CodeExpression[]{};

			find.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SqlServer.Companion.SqlConnectionSource), "conn"));

			ArrayList paramRef = new ArrayList();
			paramRef.Add(new CodeVariableReferenceExpression("conn"));
			foreach(SqlDbTableField fld in filters)
			{
				string vname = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
				find.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, vname));
				paramRef.Add(new CodeVariableReferenceExpression(vname));
			}

			find.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "pageIndex"));
			paramRef.Add(new CodeVariableReferenceExpression("pageIndex"));

			find.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "numResults"));
			paramRef.Add(new CodeVariableReferenceExpression("numResults"));

			methodFilters = new CodeExpression[paramRef.Count];
			for(int i = 0; i < paramRef.Count; i++)
				methodFilters[i] = (CodeExpression)paramRef[i];

			CodeMethodInvokeExpression callDataProc = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(dObj), procName, methodFilters);
			CodeMethodInvokeExpression callFromBase = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(bObjCol), "FromBaseArray", new CodeExpression[]{callDataProc});
			CodeMethodReturnStatement ret = new CodeMethodReturnStatement(callFromBase);

			find.Statements.Add(ret);
			@ref.Members.Add(find);
		}

		public static void GenerateFindCount(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string dataNamespace,
			string procName)
		{
			string bObj = @namespace + "." + tbl.TableName;
			string dObj = dataNamespace + "." + tbl.TableName;

			CodeMemberMethod find = new CodeMemberMethod();

			find.Name = procName;
			find.ReturnType = new CodeTypeReference(typeof(int));
			find.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final | MemberAttributes.New;
			CodeExpression[] methodFilters = new CodeExpression[]{};

			find.Parameters.Add(new CodeParameterDeclarationExpression(typeof(SqlServer.Companion.SqlConnectionSource), "conn"));

			ArrayList paramRef = new ArrayList();
			paramRef.Add(new CodeVariableReferenceExpression("conn"));
			foreach(SqlDbTableField fld in filters)
			{
				string vname = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
				find.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, vname));
				paramRef.Add(new CodeVariableReferenceExpression(vname));
			}

			methodFilters = new CodeExpression[paramRef.Count];
			for(int i = 0; i < paramRef.Count; i++)
				methodFilters[i] = (CodeExpression)paramRef[i];

			CodeMethodInvokeExpression callDataProc = new CodeMethodInvokeExpression(new CodeTypeReferenceExpression(dObj), procName, methodFilters);
			CodeMethodReturnStatement ret = new CodeMethodReturnStatement(callDataProc);

			find.Statements.Add(ret);
			@ref.Members.Add(find);
		}
	}
}
