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
	/// <summary>
	/// Provides static methods to generate a data class and related CRUD.
	/// </summary>
	/// <remarks>Used to generate a basic data object for DAL/DLL level data access.</remarks>
	/// <devloper-ToDo>
	/// Implement smart data objects:
	/// ---------------------------------------------------------------------------
	/// Smart data objects will when moved between databases automaticly create the
	/// reguired Table and CRUD. With Included XmlSerialization this will 
	/// allow for ease of transfering of data between SqlServer databases(that do 
	/// require an alternate schema?). With the proper amount of thought 
	/// "Smart Data Objects" could also use an implemented schema 
	/// to run/perform data migration and tranformation between databases. DTS...?
	/// ---------------------------------------------------------------------------
	/// 
	/// Automation:
	/// ---------------------------------------------------------------------------
	/// Use attributes to provide basic table/field information to be used with
	/// reflecting methods to create automation and higher level code/UI generation.
	/// ---------------------------------------------------------------------------
	/// 
	/// * Scratch Generated Region!!:
	/// ---------------------------------------------------------------------------
	/// Classes will be generated.
	/// 
	/// These classes will be inherted by an intially empty wrapper class that the 
	/// developer can extend without worry of code being overwritten.
	/// ---------------------------------------------------------------------------
	/// 
	/// Implement Generated Region: Danger Will Robinson!!!
	/// ---------------------------------------------------------------------------
	/// Generated Region will enclose all generated code
	/// and allow developers to easily add custom code that will not be overwritten.
	/// ---------------------------------------------------------------------------
	/// </devloper-ToDo>
	public sealed class SqlDataObjectGenerator
	{
		#region Gen. Constructors
		/// <summary>
		/// Adds all avaliable constructors to the object. This includes empty, required fields, and all fields.
		/// </summary>
		/// <param name="ref">The object that will have constructors added to it.</param>
		/// <param name="tbl">The table that the object represents.</param>
		internal static void GenerateContructors(CodeTypeDeclaration @ref, SqlDbTable tbl)
		{
			ArrayList req = new ArrayList();
			ArrayList op = new ArrayList();
			string pn = "";

			//create empty constructor member
			CodeConstructor icon = new CodeConstructor();
			icon.Attributes = MemberAttributes.Public;

			@ref.Members.Add(icon);//add contructor to object

			foreach(SqlDbTableField fld in tbl.Columns)
			{
				if(fld.IsNullable.ToUpper() == "YES")
					op.Add(fld); //add to optional contructors list
				else
					req.Add(fld); //add to required list
			}

			if(req.Count > 0)
			{	//build required contructor
				CodeConstructor rcon = new CodeConstructor();
				
				rcon.Attributes = MemberAttributes.Public;
				
				foreach(SqlDbTableField fld in req)
				{	//add each required field to the method param list
					pn += fld.ColumnName[0].ToString().ToLower();
					pn += fld.ColumnName.Substring(1);

					//create a parameter object
					CodeParameterDeclarationExpression param = new CodeParameterDeclarationExpression();

					param.Name = pn; //set name and type
					param.Type = new CodeTypeReference(fld.ValueType);

					rcon.Parameters.Add(param);//add it and it's data trsfer statement
					rcon.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(),"_" + fld.ColumnName), new CodeVariableReferenceExpression(pn)));
					pn = ""; //clear name var
				}
				//add required
				@ref.Members.Add(rcon);
			}

			if(op.Count > 0)
			{
				//create the contructor that has optional and required param
				CodeConstructor ocon = new CodeConstructor();
				ocon.Attributes = MemberAttributes.Public;
				if(req.Count > 0)
				{	//always include required members
					foreach(SqlDbTableField fld in req)
					{
						//get the name
						pn += fld.ColumnName[0].ToString().ToLower();
						pn += fld.ColumnName.Substring(1);

						//create the parameter variable
						CodeParameterDeclarationExpression param = new CodeParameterDeclarationExpression();

						param.Name = pn;	//set name and type
						param.Type = new CodeTypeReference(fld.ValueType);

						ocon.Parameters.Add(param); //add it and it data transfer statment
						ocon.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld.ColumnName), new CodeVariableReferenceExpression(pn)));
						pn = "";
					}
				}

				foreach(SqlDbTableField fld in op)
				{	//the optional parameters
					//get name
					pn += fld.ColumnName[0].ToString().ToLower();
					pn += fld.ColumnName.Substring(1);

					//create param var
					CodeParameterDeclarationExpression param = new CodeParameterDeclarationExpression();

					param.Name = pn; //set name and type
					param.Type = new CodeTypeReference(fld.ValueType);

					ocon.Parameters.Add(param);//add it and it's statement
					ocon.Statements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + fld.ColumnName), new CodeVariableReferenceExpression(pn)));
					pn = "";
				}
				//this really should be extended to add 1 constructor per optional var, var pair, var triplat and so on but frak that
				@ref.Members.Add(ocon); //add to object
			}
		}

		#endregion
		#region Gen. Intit Expressions
		/// <summary>
		/// Generates a CodeExpresion that represents the initial/default value of a variable based on the in passed SqlDbTableField.
		/// </summary>
		/// <param name="field">SqlDbTableField used to create the initialize CodeExpression.</param>
		/// <returns></returns>
		internal static CodeExpression GenerateFieldInitExpression(SqlDbTableField field)
		{
			string t = field.ValueType.FullName; //type less

			byte[] b = new byte[0];
			//special case strings to insure cross language acuracy
			string bA = b.GetType().FullName; //System.Byte[] not sure if language has an effect but better safe than sorry

			//use unrolled switch to allow use of variables
			if(t == bA)
				return new CodeArrayCreateExpression(typeof(System.Byte), 0);
			else if(t == "System.Byte")
				return new CodeSnippetExpression("System.Byte.MinValue");
			else if(t == "System.Boolean")
				return new CodePrimitiveExpression(false);
			else if(t == "System.String")
				return new CodeSnippetExpression("System.String.Empty");
			else if(t == "System.Int64")
				return new CodeSnippetExpression("System.Int64.MinValue");
			else if(t == "System.Int32")
				return new CodeSnippetExpression("System.Int32.MinValue");
			else if(t == "System.Int16")
				return new CodeSnippetExpression("System.Int16.MinValue");
			else if(t == "System.DateTime")
				return new CodeSnippetExpression("System.DateTime.MinValue");
			else if(t == "System.Double")
				return new CodeSnippetExpression("System.Double.MinValue");
			else if(t == "System.Decimal")
				return new CodeSnippetExpression("System.Decimal.MinValue");
			else if(t == "System.Single")
				return new CodeSnippetExpression("System.Single.MinValue");
			else if(t == "System.Guid")
				return new CodeSnippetExpression("System.Guid.Empty");
			else			
				return new CodePrimitiveExpression(null);
		}
		#endregion
		#region Gen. Properties
		/// <summary>
		/// Generate a property and member for field on the object ref.
		/// </summary>
		/// <param name="ref">The object that is being generated.</param>
		/// <param name="field">The SqlDbTableField that that the property represents.</param>
		/// <param name="table_constraints">Coolection of SqlDBTableConstraints used to determine rules and mark up for the property.</param>
		internal static void GenerateFieldProperty(CodeTypeDeclaration @ref, SqlDbTableField field, SqlDbTableConstraintCollection table_constraints)
		{
			#region Variables
			//create property member
			CodeMemberField member = new CodeMemberField(field.ValueType, "_" + field.ColumnName);

			//create property
			CodeMemberProperty property = new CodeMemberProperty();

			property.Attributes = MemberAttributes.Public | MemberAttributes.Final;
			bool allowSet = true;
			property.Comments.Add(new CodeCommentStatement("----------------------------------------------------------"));
			#endregion
			#region Add Property Member Variable
			member.Attributes = MemberAttributes.Family; //set member settings
			member.InitExpression = GenerateFieldInitExpression(field);

			CodeAttributeDeclaration xmlEl = new CodeAttributeDeclaration("System.Xml.Serialization.XmlElement");
			xmlEl.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(field.ColumnName)));
			xmlEl.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(field.ValueType)));
			//member.CustomAttributes.Add(xmlEl);

			@ref.Members.Add(member);
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
						GenerateIsNew(@ref, field);

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
			property.CustomAttributes.Add(xmlEl);
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

			property.GetStatements.Add( new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + field.ColumnName)));
			
			#endregion
			#region Set

			if(allowSet)
			{
				property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), member.Name), new CodeVariableReferenceExpression("value")));
				
				CodeMethodInvokeExpression dirty = new CodeMethodInvokeExpression();
				dirty.Method = new CodeMethodReferenceExpression(new CodeThisReferenceExpression(), "MarkDirty");

				property.SetStatements.Add(dirty);//new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "MarkDirty", null));
			}
			else
			{
				property.Comments.Add(new CodeCommentStatement("This property can be set but it should never be set. It is allowed to be set to support XmlSerialization and only for that reason.", true));
				property.SetStatements.Add(new CodeCommentStatement("This set should not be here, but in order to fully serialize the object all properties that will be serialized must be read/write. I leave it to the developer to know better than to set the PK."));
				property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), member.Name), new CodeVariableReferenceExpression("value")));
			}
			#endregion

			property.Comments.Add(new CodeCommentStatement("----------------------------------------------------------"));
			@ref.Members.Add(property);
		}
		/// <summary>
		/// Generates the IsDirty property on the object.
		/// </summary>
		/// <param name="ref">The object being generated.</param>
		internal static void GenerateIsDirty(CodeTypeDeclaration @ref)
		{
			CodeMemberProperty property = new CodeMemberProperty();
			CodeMemberField member = new CodeMemberField(typeof(bool), "_isDirty");
			

			member.Attributes = MemberAttributes.Family; //set member settings
			member.InitExpression = new CodePrimitiveExpression(false);
			CodeAttributeDeclaration xmlEl = new CodeAttributeDeclaration("System.Xml.Serialization.XmlElement");
			xmlEl.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression("IsDirty")));
			xmlEl.Arguments.Add(new CodeAttributeArgument(new CodeTypeOfExpression(typeof(bool))));

			@ref.Members.Add(member);
		
			property.CustomAttributes.Add(xmlEl);
			property.Name = "IsDirty"; //set name, type, and protection level
			property.Type = new CodeTypeReference(typeof(bool));
			property.Attributes = MemberAttributes.Public;

			property.SetStatements.Add(new CodeCommentStatement("This set should not be here, but in order to fully serialize the object all properties that will be serialized must be read/write. I leave it to the developer to know better than to set the dirty property manualy."));
			property.SetStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), member.Name), new CodeVariableReferenceExpression("value")));

			//get only
			property.GetStatements.Add( new CodeMethodReturnStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_isDirty")));

			//add it to the object
			@ref.Members.Add(property);

			//create the mark dirty method
			CodeMemberMethod method = new CodeMemberMethod();

			method.Name = "MarkDirty"; //set it's name, protection level, and return type
			method.Attributes = MemberAttributes.Family;
			method.ReturnType = new CodeTypeReference(typeof(void));

			//add assignment statement to set _isDirty = true
			method.Statements.Add(
				new CodeAssignStatement(
					new CodeFieldReferenceExpression(
						new CodeThisReferenceExpression(), 
						"_isDirty"), 
					new CodeSnippetExpression("true")));
			
			//add the method
			@ref.Members.Add(method);

		}
		/// <summary>
		/// Generates the IsNew property on the object.
		/// </summary>
		/// <param name="ref">The object being genrated.</param>
		/// <param name="field">The tbles primary key field.</param>
		internal static void GenerateIsNew(CodeTypeDeclaration @ref, SqlDbTableField field)
		{
			CodeMemberProperty property = new CodeMemberProperty();

			property.Name = "IsNew"; //set name, type, and protection level
			property.Type = new CodeTypeReference(typeof(bool));
			property.Attributes = MemberAttributes.Public;

			//create an if statement used to determine return value
			CodeConditionStatement @if = new CodeConditionStatement();

			//set the if condition
			@if.Condition = new CodeBinaryOperatorExpression(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_" + field.ColumnName), CodeBinaryOperatorType.ValueEquality, GenerateFieldInitExpression(field));
			//add return true to if true statements
			@if.TrueStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(true)));
			//add return false to if false statements
			@if.FalseStatements.Add(new CodeMethodReturnStatement(new CodePrimitiveExpression(false)));
			
			//add if to property get statements
			property.GetStatements.Add(@if);

			//add property to object
			@ref.Members.Add(property);

		}
		#endregion
		#region Generate Builds
		/// <summary>
		/// Generate a Method that will construct an object from an open data reader.
		/// </summary>
		/// <param name="ref">The type that contains the method.</param>
		/// <param name="namespace">The name space the type is in.</param>
		/// <param name="tbl">The sqlDbTable that is being read.</param>
		internal static void GenerateBuild(
			CodeTypeDeclaration @ref,
			string @namespace,
			SqlDbTable tbl)
		{
			CodeMemberMethod build = new CodeMemberMethod();
			CodeObjectCreateExpression create = new CodeObjectCreateExpression();
			string type = @namespace + "." + @ref.Name;
			create.CreateType = new CodeTypeReference(type);

			build.Name = "Build";//intialize the basic method attributes
			build.Attributes = MemberAttributes.Family | MemberAttributes.Static;
			build.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(System.Data.SqlClient.SqlDataReader),
				"r"));
			//we are passing out the constructed object
			build.ReturnType = new CodeTypeReference(type);
			build.Statements.Add(new CodeVariableDeclarationStatement(
				type,
				"ret",
				create));

			//unfortunatly if it has no columns it will be a waste of memory
			if(null != tbl && null != tbl.Columns)
			{
				for(int i = 0; i < tbl.Columns.Count; i++)
				{
					build.Statements.Add(new CodeSnippetStatement("\r\n"));

					//is this thing empty?
					CodeConditionStatement @if = new CodeConditionStatement();
					@if.Condition = new CodeMethodInvokeExpression(
						new CodeVariableReferenceExpression("r"),
						"IsDBNull",
						new CodeExpression[]{new CodePrimitiveExpression(i)});

					//Curses!!! This value is nothing, null, ^*%#$#*^ I need that
					@if.TrueStatements.Add(
						new CodeAssignStatement(
							new CodeFieldReferenceExpression(
								new CodeSnippetExpression("ret"),
								"_" + tbl.Columns[i].ColumnName),
							GenerateFieldInitExpression(tbl.Columns[i])));

					//thank you please drive through
					@if.FalseStatements.Add(
						new CodeAssignStatement(
							new CodeFieldReferenceExpression(new CodeSnippetExpression("ret"), "_" + tbl.Columns[i].ColumnName),
							GenerateReaderGetExpression(tbl.Columns[i], i, "r")));

					build.Statements.Add(@if);
				}
				build.Statements.Add(new CodeSnippetStatement("\r\n"));
			}
			
			//here is your object sir. and sir. if you would like another will be here sir.
			build.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));

			@ref.Members.Add(build);
		}

		/// <summary>
		/// Geberates a method that will read all object of type from a data reader into an ArrayList.
		/// </summary>
		/// <param name="ref">The type that will contain the method.</param>
		/// <param name="namespace">The namespace that will contain type.</param>
		/// <param name="tbl">The SqlDbTable that is being built.</param>
		internal static void GenerateBuildArrayList(
			CodeTypeDeclaration @ref,
			string @namespace,
			SqlDbTable tbl)
		{
			CodeMemberMethod build = new CodeMemberMethod(); //create method
			CodeObjectCreateExpression create = new CodeObjectCreateExpression();
			CodeObjectCreateExpression rcreate = new CodeObjectCreateExpression();
			
			string type = @namespace + "." + @ref.Name;
			string rtype = "System.Collections.ArrayList";
			
			create.CreateType = new CodeTypeReference(type);
			rcreate.CreateType = new CodeTypeReference(rtype);

			//intialize the basic method attributes
			build.Name = "BuildArray";
			build.Attributes = MemberAttributes.Family | MemberAttributes.Static;
			build.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(System.Data.SqlClient.SqlDataReader),
				"r"));

			//set the return type to ArrayList
			build.ReturnType = new CodeTypeReference(rtype);
			build.Statements.Add(new CodeVariableDeclarationStatement(
				rtype,
				"ret",
				new CodePrimitiveExpression(null)));

			//setup the loop to read from the reader
			CodeIterationStatement while_read = new CodeIterationStatement(
				new CodeSnippetStatement(""),
				new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("r"), "Read", new CodeExpression[0]),
				new CodeSnippetStatement("")
				);

			//if the arraylist is null create it
			CodeConditionStatement if_null = new CodeConditionStatement();
			if_null.Condition = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("ret"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(null));
			if_null.TrueStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("ret"), rcreate));
			
			//add null check and ret.Add(Build(r))
			while_read.Statements.Add(if_null);
			while_read.Statements.Add(
				new CodeMethodInvokeExpression(
					new CodeVariableReferenceExpression("ret"),
					"Add",
					new CodeExpression[]{
											new CodeMethodInvokeExpression(
												new CodeVariableReferenceExpression(type),
												"Build",
												new CodeExpression[]{new CodeVariableReferenceExpression("r")})
										}));

			//line feed
			build.Statements.Add(new CodeSnippetStatement("\r\n"));//remark on how bad this looks in C#
			build.Statements.Add(new CodeCommentStatement("very poorly generated while loop... good job Microsoft.. NOT"));

			//add loop to method
			build.Statements.Add(while_read);

			//add comment to the method
			build.Comments.Add(new CodeCommentStatement("Build an ArrayList from an inpassed SqlDataReader."));
			//add return statement
			build.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));
			@ref.Members.Add(build);//add method to object
		}

		#endregion
		#region DataReader Expresions
		/// <summary>
		/// Generates a code expression that reads a value from an SqlDataReader.
		/// </summary>
		/// <param name="field">The SqlDbTableField that is being read.</param>
		/// <param name="idx">The index of the field in the reader.</param>
		/// <param name="reader">The name of the reader being read.</param>
		/// <returns></returns>
		internal static CodeExpression GenerateReaderGetExpression(SqlDbTableField field, int idx, string reader)
		{	//your type three widget with purple spots? If you say so...
			string type = field.ValueType.FullName.Substring(field.ValueType.FullName.LastIndexOf(".")+1);

			if(type != "Byte[]" && type != "Object" && type != "Single")
			{	//a normal type cross over... until they add more..
				return new CodeMethodInvokeExpression(
					new CodeSnippetExpression(reader), 
					"Get" + type, 
					new CodeExpression[]{new CodePrimitiveExpression(idx)});
			}
			else if (type == "Single")
			{	//name not the same!!! Curses!!!
				return new CodeMethodInvokeExpression(
					new CodeSnippetExpression(reader), 
					"GetFloat", 
					new CodeExpression[]{new CodePrimitiveExpression(idx)});
			}
			else if(type == "Byte[]")
			{	//no not an array
				return new CodeCastExpression(field.ValueType, new CodeArrayIndexerExpression(new CodeSnippetExpression(reader), new CodeExpression[]{new CodePrimitiveExpression(idx)}));
			}
			else if(type == "Object")
			{	//i hope you know wtf your doing with this chunk oh thing, because it's a complex object that has been writen in SQL, or serialized, or what not, and why you need that....
				return new CodeArrayIndexerExpression(new CodeSnippetExpression(reader), new CodeExpression[]{new CodePrimitiveExpression(idx)});
			}
			else
			{	//well  it looks good to have an else
				return new CodeCastExpression(field.ValueType, new CodeArrayIndexerExpression(new CodeSnippetExpression(reader), new CodeExpression[]{new CodePrimitiveExpression(idx)}));
			}
		}

		#endregion
		#region Generate Param Variables
		internal static CodeObjectCreateExpression GenerateCreateSqlParamExpression(
			SqlDbTableField fld,
			bool is_static)
		{
			string n = "_" + fld.ColumnName;

			if(is_static == true)
			{
				n = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
				return 	new CodeObjectCreateExpression(
					typeof(System.Data.SqlClient.SqlParameter),
					new CodeExpression[]{
											new CodePrimitiveExpression("@" + fld.ColumnName),
											new CodeVariableReferenceExpression(n)});
			}

			return 	new CodeObjectCreateExpression(
						typeof(System.Data.SqlClient.SqlParameter),
						new CodeExpression[]{
							new CodePrimitiveExpression("@" + fld.ColumnName),
							new CodeFieldReferenceExpression(
											new CodeThisReferenceExpression(), n)});

		}


		internal static CodeArrayCreateExpression GenrateCreateSqlParamArrayStatement(
			ArrayList param,
			bool is_static)
		{
			CodeArrayCreateExpression pa = new CodeArrayCreateExpression();

			pa.CreateType = new CodeTypeReference(typeof(System.Data.SqlClient.SqlParameter));
			
			foreach(SqlDbTableField  fld in param)
			{
				pa.Initializers.Add(GenerateCreateSqlParamExpression(fld, is_static));
			}

			//CodeVariableDeclarationStatement ret = new CodeVariableDeclarationStatement(
			//	typeof(System.Data.SqlClient.SqlParameter[]),
			//	name,
			//	pa);
			
			return pa;
		}


		internal static CodeArrayCreateExpression GenrateCreateSqlPagerParamArrayStatement(
			ArrayList param,
			bool is_static)
		{
			CodeArrayCreateExpression pa = new CodeArrayCreateExpression();

			pa.CreateType = new CodeTypeReference(typeof(System.Data.SqlClient.SqlParameter));
			
			foreach(SqlDbTableField  fld in param)
			{
				pa.Initializers.Add(GenerateCreateSqlParamExpression(fld, is_static));
			}

			pa.Initializers.Add(
				new CodeObjectCreateExpression(
				typeof(System.Data.SqlClient.SqlParameter),
				new CodeExpression[]{
										new CodePrimitiveExpression("@PageIndex"),
										new CodeVariableReferenceExpression("pageIndex")}));

			pa.Initializers.Add(
				new CodeObjectCreateExpression(
				typeof(System.Data.SqlClient.SqlParameter),
				new CodeExpression[]{
										new CodePrimitiveExpression("@NumResults"),
										new CodeVariableReferenceExpression("numResults")}));
			
			return pa;
		}

		#endregion
		#region Generate Class
		/// <summary>
		/// Generates a class that represents a table in an SqlServer database.
		/// </summary>
		/// <param name="code">The unit of code that will contain the class.</param>
		/// <param name="tbl">The table that this class will represent.</param>
		/// <param name="namespace">The namespace this class will reside in.</param>
		/// <returns>The class CodeTypeDecleration is returned to allow further extention before it is written or generated.</returns>
		public static CodeTypeDeclaration GenerateTableClass(
			CodeCompileUnit code, 
			SqlDbTable tbl, 
			string @namespace)
		{
			#region Variables
			CodeNamespace nspace = new CodeNamespace(@namespace);
			CodeTypeDeclaration obj = new CodeTypeDeclaration(tbl.TableName);
			System.CodeDom.CodeConstructor objCon = new CodeConstructor();
			
			string pkCol = string.Empty;
			StringDictionary fkCol = new StringDictionary();
			#endregion
			#region Setup Class
			obj.IsClass = true;
			
			obj.Attributes = MemberAttributes.Public;
			obj.TypeAttributes = TypeAttributes.Public | TypeAttributes.Class;// | TypeAttributes.Abstract;
			#endregion
			#region Class Using Statements
			//add system imports/using
			nspace.Imports.Add(new CodeNamespaceImport("System"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.Common"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.SqlTypes"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Xml"));
			
			//add SqlServer.Companion imports/using
			nspace.Imports.Add(new CodeNamespaceImport("SqlServer.Companion"));
			nspace.Imports.Add(new CodeNamespaceImport("SqlServer.Companion.Base"));
			#endregion
			#region Add Class Attributes
			CodeAttributeDeclaration serial = new CodeAttributeDeclaration("System.Serializable");
			obj.CustomAttributes.Add(serial);

			CodeAttributeDeclaration tblAttrib = new CodeAttributeDeclaration("SqlServer.Companion.TableRow");
			tblAttrib.Arguments.Add(new CodeAttributeArgument(new CodePrimitiveExpression(tbl.TableName)));
			obj.CustomAttributes.Add(tblAttrib);

			CodeAttributeDeclaration xmlSerial = new CodeAttributeDeclaration("System.Xml.Serialization.XmlRootAttribute");
			xmlSerial.Arguments.Add(new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(tbl.TableName)));
			xmlSerial.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression("")));
			xmlSerial.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(false)));
			obj.CustomAttributes.Add(xmlSerial);

			#endregion

			GenerateContructors(obj, tbl);
			GenerateIsDirty(obj);
			GenerateBuild(obj, @namespace, tbl);
			GenerateBuildArrayList(obj, @namespace, tbl);

			#region Add Field Properties
			//add table fields
			if(null != tbl.Columns)
			{
				foreach(SqlDbTableField f in tbl.Columns)
					GenerateFieldProperty(obj, f, tbl.Constraints);
			}
			#endregion
			
			Common.GenerateXmlSerializationSaveToXML(obj, @namespace);
			Common.GenerateXmlSerializationLoadFromXML(obj, @namespace);

			obj.BaseTypes.Add(typeof(SqlServer.Companion.Base.ISqlDataObject));
			obj.BaseTypes.Add(SqlInterfaceGenerator.GetInterfaceName(tbl, @namespace));

			nspace.Types.Add(obj);//add the new object to the namespace
			code.Namespaces.Add(nspace);//add the namespace to the code unit

			return obj;
		}

		#endregion
		#region Generate CRUD
		/// <summary>
		/// Add Delete methods to a class.
		/// </summary>
		/// <param name="ref">The class to add delete methods too.</param>
		/// <param name="tbl">The table that will have data deleted.</param>
		public static void GenerateTableDelete(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl)
		{

			string pk = "";
			if(null != tbl.Constraints)
			{
				foreach(SqlDbTableConstraint tc in tbl.Constraints)
				{
					if(tc.IsPrimaryKey)
					{
						pk = tc.ConstraintColumnName;
						break;
					}
				}
			}

			if(null == pk || pk == "")
				return;


			CodeMemberMethod del_rec = new CodeMemberMethod();
			CodeMemberMethod del_rec_tran = new CodeMemberMethod();
			
			del_rec.Name = "Delete";
			del_rec_tran.Name = "Delete";

			del_rec.Attributes = MemberAttributes.Public;
			del_rec_tran.Attributes = MemberAttributes.Public;

			del_rec.ReturnType = new CodeTypeReference(typeof(void));
			del_rec_tran.ReturnType = new CodeTypeReference(typeof(void));

			del_rec.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(SqlServer.Companion.SqlConnectionSource),
				"conn"));

			del_rec_tran.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(SqlServer.Companion.SqlConnectionSource),
				"conn"));

			del_rec_tran.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(System.Data.SqlClient.SqlTransaction),
				"tran"));


			CodeConditionStatement if_new = new CodeConditionStatement();
			if_new.Condition = new CodeBinaryOperatorExpression(
				new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IsNew"),
				CodeBinaryOperatorType.ValueEquality,
				new CodePrimitiveExpression(false));

			CodeConditionStatement if_new_tran = new CodeConditionStatement();
			if_new_tran.Condition = new CodeBinaryOperatorExpression(
				new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IsNew"),
				CodeBinaryOperatorType.ValueEquality,
				new CodePrimitiveExpression(false));

			CodeVariableDeclarationStatement p = new CodeVariableDeclarationStatement(
				typeof(System.Data.SqlClient.SqlParameter),
				"pk",
				new CodeObjectCreateExpression(
					typeof(System.Data.SqlClient.SqlParameter),
					new CodeExpression[]{
						new CodePrimitiveExpression("@" + pk),
						new CodeFieldReferenceExpression(
											new CodeThisReferenceExpression(), "_" + pk)}));

			CodeVariableDeclarationStatement p_tran = new CodeVariableDeclarationStatement(
				typeof(System.Data.SqlClient.SqlParameter),
				"pk",
				new CodeObjectCreateExpression(
				typeof(System.Data.SqlClient.SqlParameter),
				new CodeExpression[]{
										new CodePrimitiveExpression("@" + pk),
										new CodeFieldReferenceExpression(
										new CodeThisReferenceExpression(), "_" + pk)}));
			if_new.TrueStatements.Add(p);
			if_new_tran.TrueStatements.Add(p);

			if_new.TrueStatements.Add(
				new CodeMethodInvokeExpression(
				new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("conn"),"DbAccessor"),"ExecuteProcedure"),
				"ExecuteNonQuery",
				new CodeExpression[]{new CodePrimitiveExpression(tbl.TableName + "__DELETE_ROW"), new CodeVariableReferenceExpression("pk")}));

			if_new_tran.TrueStatements.Add(
				new CodeMethodInvokeExpression(
					new CodePropertyReferenceExpression(
						new CodePropertyReferenceExpression(
							new CodeVariableReferenceExpression("conn"),
							"DbAccessor"),
						"ExecuteProcedure"),
					"ExecuteNonQuery",
					new CodeExpression[]{
										new CodePrimitiveExpression(tbl.TableName + "__DELETE_ROW"), 
										new CodeVariableReferenceExpression("pk"), 
										new CodeVariableReferenceExpression("tran")}));
		
			del_rec.Statements.Add(if_new);
			del_rec_tran.Statements.Add(if_new_tran);

			@ref.Members.Add(del_rec);
			@ref.Members.Add(del_rec_tran);

		}

		/// <summary>
		/// Add Insert methods to a class.
		/// </summary>
		/// <param name="ref">The class to have insert methods added./param>
		/// <param name="tbl">The table that will have data inserted.</param>
		public static void GenerateTableInsert(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl)
		{
			CodeMemberMethod in_rec = new CodeMemberMethod();
			CodeMemberMethod in_rec_tran = new CodeMemberMethod();

			in_rec.Name = "Insert";
			in_rec.Attributes = MemberAttributes.Public;
			in_rec.ReturnType = new CodeTypeReference(typeof(void));
			in_rec.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(SqlServer.Companion.SqlConnectionSource),
				"conn"));

			in_rec_tran.Name = "Insert";
			in_rec_tran.Attributes = MemberAttributes.Public;
			in_rec_tran.ReturnType = new CodeTypeReference(typeof(void));
			in_rec_tran.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(SqlServer.Companion.SqlConnectionSource),
				"conn"));
			in_rec_tran.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(System.Data.SqlClient.SqlTransaction),
				"tran"));

			ArrayList param = new ArrayList();

			string pk = "";
			SqlDbTableField pkFld = null;
			if(null != tbl.Constraints)
			{
				foreach(SqlDbTableConstraint tc in tbl.Constraints)
				{
					if(tc.IsPrimaryKey)
					{
						pk = tc.ConstraintColumnName;
						
						break;
					}
				}
			}

			
			if(null != tbl.Columns)
			{
				for(int i = 0; i < tbl.Columns.Count; i++)
				{
					if(pk != tbl.Columns[i].ColumnName)
					{
						param.Add(tbl.Columns[i]);
					}
					else
					{
						pkFld = tbl.Columns[i];
					}
				}
			}

			CodeVariableDeclarationStatement dp = null;
			CodeArrayCreateExpression pa = GenrateCreateSqlParamArrayStatement(param, false);

			CodeConditionStatement if_new = new CodeConditionStatement();
			CodeConditionStatement if_new_tran = new CodeConditionStatement();

			if_new.Condition = new CodeBinaryOperatorExpression(
				new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IsNew"),
				CodeBinaryOperatorType.ValueEquality,
				new CodePrimitiveExpression(true));

			if_new_tran.Condition = new CodeBinaryOperatorExpression(
				new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IsNew"),
				CodeBinaryOperatorType.ValueEquality,
				new CodePrimitiveExpression(true));

			if(null != pkFld)
			{
				if(pkFld.ValueType == Type.GetType("System.Int32") || pkFld.ValueType == Type.GetType("System.Guid"))
				{
					pa.Initializers.Add(GenerateCreateSqlParamExpression(pkFld, false));
				}
			}
			
			dp = new CodeVariableDeclarationStatement(typeof(System.Data.SqlClient.SqlParameter[]), "param", pa);
			if_new.TrueStatements.Add(dp);
			if_new_tran.TrueStatements.Add(dp);

			if(null != pkFld)
			{
				if(pkFld.ValueType == Type.GetType("System.Int32") || pkFld.ValueType == Type.GetType("System.Guid"))
				{
					CodeArrayIndexerExpression a_idx = new CodeArrayIndexerExpression(new CodeSnippetExpression("param"), new CodePrimitiveExpression(pa.Initializers.Count -1));
					CodeAssignStatement m_output = new CodeAssignStatement(new CodePropertyReferenceExpression(a_idx, "Direction"), new CodeSnippetExpression("System.Data.ParameterDirection.Output"));
					
					if_new.TrueStatements.Add(m_output);
					if_new_tran.TrueStatements.Add(m_output);
				}
			}

			if_new.TrueStatements.Add(
				new CodeMethodInvokeExpression(
				new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("conn"),"DbAccessor"),"ExecuteProcedure"),
				"ExecuteNonQuery",
				new CodeExpression[]{new CodePrimitiveExpression(tbl.TableName + "__INSERT_ROW"), new CodeVariableReferenceExpression("param")}));

			if_new_tran.TrueStatements.Add(
				new CodeMethodInvokeExpression(
				new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("conn"),"DbAccessor"),"ExecuteProcedure"),
				"ExecuteNonQuery",
				new CodeExpression[]{new CodePrimitiveExpression(tbl.TableName + "__INSERT_ROW"), new CodeVariableReferenceExpression("param"), new CodeVariableReferenceExpression("tran")}));

			if(null != pkFld)
			{
				if(pkFld.ValueType == Type.GetType("System.Int32") || pkFld.ValueType == Type.GetType("System.Guid"))
				{
					CodeArrayIndexerExpression a_idx = new CodeArrayIndexerExpression(new CodeSnippetExpression("param"), new CodePrimitiveExpression(pa.Initializers.Count -1));
					CodeAssignStatement pkAssign = new CodeAssignStatement(
						new CodeFieldReferenceExpression(
							new CodeThisReferenceExpression(),
							"_" + pkFld.ColumnName),
						new CodeCastExpression(pkFld.ValueType, new CodePropertyReferenceExpression(a_idx, "Value")));
					
					if_new.TrueStatements.Add(pkAssign);
					if_new_tran.TrueStatements.Add(pkAssign);
				}
			}

			if_new.TrueStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_isDirty"), new CodePrimitiveExpression(false)));
			if_new_tran.TrueStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_isDirty"), new CodePrimitiveExpression(false)));

			in_rec.Statements.Add(if_new);
			in_rec_tran.Statements.Add(if_new_tran);

			@ref.Members.Add(in_rec);
			@ref.Members.Add(in_rec_tran);

		}

		/// <summary>
		/// Add Update methods to a class.
		/// </summary>
		/// <param name="ref">The class that will have methods added.</param>
		/// <param name="tbl">The table that will be updated.</param>
		public static void GenerateTableUpdate(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl)
		{
			CodeMemberMethod up_rec = new CodeMemberMethod();
			CodeMemberMethod up_rec_tran = new CodeMemberMethod();
			
			up_rec.Name = "Update";
			up_rec.Attributes = MemberAttributes.Public;
			up_rec.ReturnType = new CodeTypeReference(typeof(void));
			up_rec.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(SqlServer.Companion.SqlConnectionSource),
				"conn"));

			up_rec_tran.Name = "Update";
			up_rec_tran.Attributes = MemberAttributes.Public;
			up_rec_tran.ReturnType = new CodeTypeReference(typeof(void));
			up_rec_tran.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(SqlServer.Companion.SqlConnectionSource),
				"conn"));
			up_rec_tran.Parameters.Add(new CodeParameterDeclarationExpression(
				typeof(System.Data.SqlClient.SqlTransaction),
				"tran"));

			ArrayList param = new ArrayList();

			string pk = "";
			SqlDbTableField pkFld = null;
			if(null != tbl.Constraints)
			{
				foreach(SqlDbTableConstraint tc in tbl.Constraints)
				{
					if(tc.IsPrimaryKey)
					{
						pk = tc.ConstraintColumnName;
						
						break;
					}
				}
			}

			
			if(null != tbl.Columns)
			{
				for(int i = 0; i < tbl.Columns.Count; i++)
				{
					if(pk != tbl.Columns[i].ColumnName)
					{
						param.Add(tbl.Columns[i]);
					}
					else
					{
						pkFld = tbl.Columns[i];
					}
				}
			}

			CodeVariableDeclarationStatement dp = null;
			CodeArrayCreateExpression pa = GenrateCreateSqlParamArrayStatement(param, false);

			CodeConditionStatement if_new = new CodeConditionStatement();
			CodeConditionStatement if_new_tran = new CodeConditionStatement();

			if_new.Condition = new CodeBinaryOperatorExpression(
				new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IsNew"),
				CodeBinaryOperatorType.ValueEquality,
				new CodePrimitiveExpression(false));

			if_new_tran.Condition = new CodeBinaryOperatorExpression(
				new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "IsNew"),
				CodeBinaryOperatorType.ValueEquality,
				new CodePrimitiveExpression(false));

			if(null != pkFld)
			{
				if(pkFld.ValueType == Type.GetType("System.Int32") || pkFld.ValueType == Type.GetType("System.Guid"))
				{
					pa.Initializers.Add(GenerateCreateSqlParamExpression(pkFld, false));
				}
			}
			
			dp = new CodeVariableDeclarationStatement(typeof(System.Data.SqlClient.SqlParameter[]), "param", pa);
			if_new.TrueStatements.Add(dp);
			if_new_tran.TrueStatements.Add(dp);

			if_new.TrueStatements.Add(
				new CodeMethodInvokeExpression(
				new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("conn"),"DbAccessor"),"ExecuteProcedure"),
				"ExecuteNonQuery",
				new CodeExpression[]{new CodePrimitiveExpression(tbl.TableName + "__UPDATE_ROW"), new CodeVariableReferenceExpression("param")}));

			if_new_tran.TrueStatements.Add(
				new CodeMethodInvokeExpression(
				new CodePropertyReferenceExpression(new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("conn"),"DbAccessor"),"ExecuteProcedure"),
				"ExecuteNonQuery",
				new CodeExpression[]{new CodePrimitiveExpression(tbl.TableName + "__UPDATE_ROW"), new CodeVariableReferenceExpression("param"), new CodeVariableReferenceExpression("tran")}));

			if_new.TrueStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_isDirty"), new CodePrimitiveExpression(false)));
			if_new_tran.TrueStatements.Add(new CodeAssignStatement(new CodeFieldReferenceExpression(new CodeThisReferenceExpression(), "_isDirty"), new CodePrimitiveExpression(false)));
			
			up_rec.Statements.Add(if_new);
			up_rec_tran.Statements.Add(if_new_tran);

			@ref.Members.Add(up_rec);
			@ref.Members.Add(up_rec_tran);
			
		}

		#endregion
		#region Generate FETCH/FIND
		/// <summary>
		/// Add a Method to load data from the database to a class.
		/// </summary>
		/// <param name="ref">The class.</param>
		/// <param name="tbl">Table to be queried.</param>
		/// <param name="filters">ArrayList of SqlDBTableFields used to filter the query.</param>
		/// <param name="namespace">The namespace the object resides in.</param>
		/// <param name="proc_name">The name of the stored procedure.</param>
		/// <param name="findMany">Is this a find one or find many query?</param>
		public static void GenerateTableQuery(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string proc_name,
			bool findMany)
		{
			CodeMemberMethod qry = new CodeMemberMethod();
			
			qry.Name = proc_name; //set method name and protection level
			qry.Attributes = MemberAttributes.Public | MemberAttributes.Static;

			//determine and set the return type
			if(findMany == true)
			{	//setup return type for array list and add the return variable
				qry.ReturnType = new CodeTypeReference("System.Collections.ArrayList");
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
						typeof(System.Collections.ArrayList),
						"ret",
						new CodePrimitiveExpression(null))
					);
			}
			else
			{
				//create a refreace to this type
				string onm = @namespace + "." + tbl.TableName;
				//setup method to return a single instance of this type and create the return variable
				qry.ReturnType = new CodeTypeReference(onm);
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
						onm,
						"ret",
						new CodePrimitiveExpression(null))
					);
			}
			
			//add the connection parameter
			qry.Parameters.Add(new CodeParameterDeclarationExpression(
				"SqlServer.Companion.SqlConnectionSource",
				"conn"));

			string n = "";
			if(null != filters)
			{	//loop through and create param list
				foreach(SqlDbTableField fld in filters)
				{	//up hold basic naming standard
					n = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
					qry.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, n));
				}

				if(null != filters && filters.Count > 0)
					n = ((SqlDbTableField)filters[0]).ColumnName[0].ToString().ToLower() + ((SqlDbTableField)filters[0]).ColumnName.Substring(1);
			}

			//create a datareader variable
			CodeVariableDeclarationStatement reader = new CodeVariableDeclarationStatement(
				typeof(System.Data.SqlClient.SqlDataReader),
				"r", 
				new CodePrimitiveExpression(null));


			CodeAssignStatement setReader = null;

			//setup the parameters for the sql procedure if any
			if(null != filters && filters.Count > 1)
			{
				//create a variable array of all the paramters and add it
				CodeArrayCreateExpression pa = GenrateCreateSqlParamArrayStatement(filters, true);
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
					typeof(System.Data.SqlClient.SqlParameter[]),
					"param",
					pa));

			}
			else if(null != filters && filters.Count == 1)
			{
				//create and add parameter var
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
					typeof(System.Data.SqlClient.SqlParameter), 
					"param", 
					new CodeObjectCreateExpression(
					typeof(System.Data.SqlClient.SqlParameter),
					new CodeExpression[]{
											new CodePrimitiveExpression("@" + ((SqlDbTableField)filters[0]).ColumnName),
											new CodeVariableReferenceExpression(n)
										}
					)));
			}

			//add the data reader to the method
			qry.Statements.Add(reader);

			//fugly code to create code to run the procedure
			CodeMethodInvokeExpression runSql = null;
			if(null != filters && filters.Count > 0)
			{
				runSql = new CodeMethodInvokeExpression(
					new CodePropertyReferenceExpression(
					new CodePropertyReferenceExpression(
					new CodeVariableReferenceExpression("conn"),"DbAccessor"),
					"ExecuteProcedure"),
					"ExecuteReader",
					new CodeExpression[]{
											new CodePrimitiveExpression(proc_name), 
											new CodeVariableReferenceExpression("param")}
					);
			}
			else
			{
				runSql = new CodeMethodInvokeExpression(
					new CodePropertyReferenceExpression(
					new CodePropertyReferenceExpression(
					new CodeVariableReferenceExpression("conn"),"DbAccessor"),
					"ExecuteProcedure"),
					"ExecuteReader",
					new CodeExpression[]{
											new CodePrimitiveExpression(proc_name)}
					);
			}
			setReader = new CodeAssignStatement(new CodeVariableReferenceExpression("r"), runSql);

			//add the code to init the reader
			qry.Statements.Add(setReader);

			CodeConditionStatement readerIsNull = new CodeConditionStatement();

			readerIsNull.Condition = new CodeBinaryOperatorExpression(
				new CodePrimitiveExpression(null),
				CodeBinaryOperatorType.ValueEquality,
				new CodeVariableReferenceExpression("r")
				);

			//will run the Build/BuildArray method
			CodeAssignStatement exc = null;

			//invoke the right Build
			exc = new CodeAssignStatement(new CodeVariableReferenceExpression("ret"),
					new CodeMethodInvokeExpression(
					new CodeVariableReferenceExpression(@namespace + "." + tbl.TableName), 
					(findMany == true) ? "BuildArray" : "Build", 
					new CodeExpression[]{new CodeVariableReferenceExpression("r")}
				));

			if(!findMany)
			{
				CodeConditionStatement ifRead = new CodeConditionStatement();
				ifRead.Condition = new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("r"), "Read", new CodeExpression[]{});
				ifRead.TrueStatements.Add(exc);

				readerIsNull.FalseStatements.Add(ifRead);
			}
			else
			{
				readerIsNull.FalseStatements.Add(exc);//build ret, close reader, and set reader = null;
			}

			readerIsNull.FalseStatements.Add(new CodeMethodInvokeExpression(
				new CodeVariableReferenceExpression("r"),
				"Close",
				new CodeExpression[]{}));
			readerIsNull.FalseStatements.Add(
				new CodeAssignStatement(
				new CodeVariableReferenceExpression("r"),
				new CodePrimitiveExpression(null)));
			
			//add if statement
			qry.Statements.Add(readerIsNull);

			//return
			qry.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));

			@ref.Members.Add(qry);
		}

		/// <summary>
		/// Add methods to get a count based on information in a table.
		/// </summary>
		/// <param name="ref">The class.</param>
		/// <param name="tbl">Table to be queried.</param>
		/// <param name="filters">ArrayList of SqlDBTableFields used to filter the query.</param>
		/// <param name="namespace">The namespace the object resides in.</param>
		/// <param name="proc_name">The name of the stored procedure.</param>
		public static void GenerateTableCountQuery(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string proc_name)
		{
			CodeMemberMethod qry = new CodeMemberMethod();
			
			qry.Name = proc_name; //set method name and protection level
			qry.Attributes = MemberAttributes.Public | MemberAttributes.Static;

			//set the return type and add return variable
			qry.ReturnType = new CodeTypeReference("System.Int32");
			qry.Statements.Add(
				new CodeVariableDeclarationStatement(
				typeof(System.Int32),
				"ret",
				new CodePrimitiveExpression(0))
				);

			//add the connection parameter
			qry.Parameters.Add(new CodeParameterDeclarationExpression(
				"SqlServer.Companion.SqlConnectionSource",
				"conn"));

			string n = "";
			if(null != filters)
			{	//loop through and create param list
				foreach(SqlDbTableField fld in filters)
				{	//up hold basic naming standard
					n = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
					qry.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, n));
				}
			}

			CodeMethodInvokeExpression runSql = null;
			if(null != filters && filters.Count > 1)
			{
				//create a variable array of all the paramters and add it
				CodeArrayCreateExpression pa = GenrateCreateSqlParamArrayStatement(filters, true);
				qry.Statements.Add(
					new CodeVariableDeclarationStatement(
					typeof(System.Data.SqlClient.SqlParameter[]),
					"param",
					pa));
			
				runSql = new CodeMethodInvokeExpression(
					new CodePropertyReferenceExpression(
					new CodePropertyReferenceExpression(
					new CodeVariableReferenceExpression("conn"),"DbAccessor"),
					"ExecuteProcedure"),
					"ExecuteScalar",
					new CodeExpression[]{
											new CodePrimitiveExpression(proc_name), 
											new CodeVariableReferenceExpression("param")}
					);
			}
			else
			{
				runSql = new CodeMethodInvokeExpression(
					new CodePropertyReferenceExpression(
					new CodePropertyReferenceExpression(
					new CodeVariableReferenceExpression("conn"),"DbAccessor"),
					"ExecuteProcedure"),
					"ExecuteScalar",
					new CodeExpression[]{
											new CodePrimitiveExpression(proc_name)}
					);
			}

			CodeCastExpression toInt = new CodeCastExpression(
				typeof(System.Int32),
				runSql);

			System.CodeDom.CodeAssignStatement setRet = new CodeAssignStatement(
				new CodeVariableReferenceExpression("ret"), 
				toInt);

			qry.Statements.Add(setRet);

			//return
			qry.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));

			@ref.Members.Add(qry);
		
		}
	
		/// <summary>
		/// Add methods that get a sub section of results based on page index and results per page.
		/// </summary>
		/// <param name="ref">The class.</param>
		/// <param name="tbl">Table to be queried.</param>
		/// <param name="filters">ArrayList of SqlDBTableFields used to filter the query.</param>
		/// <param name="namespace">The namespace the object resides in.</param>
		/// <param name="proc_name">The name of the stored procedure.</param>
		public static void GenerateTablePagedQuery(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			ArrayList filters,
			string @namespace,
			string proc_name)
		{
			CodeMemberMethod qry = new CodeMemberMethod();
			
			qry.Name = proc_name; //set method name and protection level
			qry.Attributes = MemberAttributes.Public | MemberAttributes.Static;

			//set the return type and add return variable
			qry.ReturnType = new CodeTypeReference("System.Collections.ArrayList");
			qry.Statements.Add(
				new CodeVariableDeclarationStatement(
				typeof(System.Collections.ArrayList),
				"ret",
				new CodePrimitiveExpression(null))
				);

			//add the connection parameter
			qry.Parameters.Add(new CodeParameterDeclarationExpression(
				"SqlServer.Companion.SqlConnectionSource",
				"conn"));

			string n = "";
			if(null != filters)
			{	//loop through and create param list
				foreach(SqlDbTableField fld in filters)
				{	//up hold basic naming standard
					n = fld.ColumnName[0].ToString().ToLower() + fld.ColumnName.Substring(1);
					qry.Parameters.Add(new CodeParameterDeclarationExpression(fld.ValueType, n));
				}
			}

			//add paging parameters
			qry.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Int32), "pageIndex"));
			qry.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.Int32), "numResults"));

		
			//create a datareader variable
			CodeVariableDeclarationStatement reader = new CodeVariableDeclarationStatement(
				typeof(System.Data.SqlClient.SqlDataReader),
				"r", 
				new CodePrimitiveExpression(null));


			CodeAssignStatement setReader = null;

			//create a variable array of all the paramters and add it
			CodeArrayCreateExpression pa = GenrateCreateSqlPagerParamArrayStatement(filters, true);
			qry.Statements.Add(
				new CodeVariableDeclarationStatement(
				typeof(System.Data.SqlClient.SqlParameter[]),
				"param",
				pa));


			//add the data reader to the method
			qry.Statements.Add(reader);

			//fugly code to create code to run the procedure
			CodeMethodInvokeExpression runSql = null;
			runSql = new CodeMethodInvokeExpression(
				new CodePropertyReferenceExpression(
				new CodePropertyReferenceExpression(
				new CodeVariableReferenceExpression("conn"),"DbAccessor"),
				"ExecuteProcedure"),
				"ExecuteReader",
				new CodeExpression[]{
										new CodePrimitiveExpression(proc_name), 
										new CodeVariableReferenceExpression("param")}
				);

			setReader = new CodeAssignStatement(new CodeVariableReferenceExpression("r"), runSql);

			//add the code to init the reader
			qry.Statements.Add(setReader);

			CodeConditionStatement readerIsNull = new CodeConditionStatement();

			readerIsNull.Condition = new CodeBinaryOperatorExpression(
				new CodePrimitiveExpression(null),
				CodeBinaryOperatorType.ValueEquality,
				new CodeVariableReferenceExpression("r")
				);

			//will run the Build/BuildArray method
			CodeAssignStatement exc = null;

			//invoke the right Build
			exc = new CodeAssignStatement(new CodeVariableReferenceExpression("ret"),
				new CodeMethodInvokeExpression(
				new CodeVariableReferenceExpression(@namespace + "." + tbl.TableName), 
				"BuildArray", 
				new CodeExpression[]{new CodeVariableReferenceExpression("r")}
				));
			
			readerIsNull.FalseStatements.Add(exc);//build ret, close reader, and set reader = null;
			readerIsNull.FalseStatements.Add(new CodeMethodInvokeExpression(
				new CodeVariableReferenceExpression("r"),
				"Close",
				new CodeExpression[]{}));
			readerIsNull.FalseStatements.Add(
				new CodeAssignStatement(
				new CodeVariableReferenceExpression("r"),
				new CodePrimitiveExpression(null)));
			
			//add if statement
			qry.Statements.Add(readerIsNull);

			//return
			qry.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));

			@ref.Members.Add(qry);
		}
		#endregion
	}
}