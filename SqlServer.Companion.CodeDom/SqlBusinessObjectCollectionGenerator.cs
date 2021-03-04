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
	/// Summary description for SqlBusinessObjectCollectionGenerator.
	/// </summary>
	public class SqlBusinessObjectCollectionGenerator
	{
		public static CodeTypeDeclaration GenerateBusinessCollection(
			CodeCompileUnit unit,
			SqlDbTable tbl,
			string @namespace,
			string dataNamespace)
		{
			//business namespace
			CodeNamespace nspace = new CodeNamespace(@namespace);
			CodeTypeDeclaration obj = new CodeTypeDeclaration(tbl.TableName + "Collection");
			System.CodeDom.CodeConstructor objCon = new CodeConstructor();

			obj.IsClass = true;
			
			obj.Attributes = MemberAttributes.Public;
			obj.TypeAttributes = TypeAttributes.Public | TypeAttributes.Class;
			obj.BaseTypes.Add("SqlServer.Companion.Base.SqlDataObjectCollectionBase");

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
			xmlSerial.Arguments.Add(new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(tbl.TableName + "Collection")));
			xmlSerial.Arguments.Add(new CodeAttributeArgument("Namespace", new CodePrimitiveExpression("")));
			xmlSerial.Arguments.Add(new CodeAttributeArgument("IsNullable", new CodePrimitiveExpression(false)));
			obj.CustomAttributes.Add(xmlSerial);
			
			GenerateAddMethod(obj, tbl, @namespace);
			GenerateIndexOfMethod(obj, tbl, @namespace);
			GenerateInsertMethod(obj, tbl, @namespace);
			GenerateRemoveMethod(obj, tbl, @namespace);
			GenerateIndexer(obj, tbl, @namespace);

			GenerateFromBaseArray(obj, tbl, @namespace, dataNamespace);

			nspace.Types.Add(obj);

			unit.Namespaces.Add(nspace);
			return obj;

		}

		internal static void GenerateAddMethod(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace)
		{
			CodeMemberMethod add = new CodeMemberMethod();

			add.Name = "Add";
			add.ReturnType = new CodeTypeReference(typeof(int));
			add.Attributes = MemberAttributes.Public;
			add.Parameters.Add(new CodeParameterDeclarationExpression(@namespace + "." + tbl.TableName, "value"));
			add.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "Add", new CodeExpression[]{new CodeVariableReferenceExpression("value")})));

			@ref.Members.Add(add);
		}

		internal static void GenerateIndexOfMethod(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace)
		{
			CodeMemberMethod indexOf = new CodeMemberMethod();

			indexOf.Name = "IndexOf";
			indexOf.ReturnType = new CodeTypeReference(typeof(int));
			indexOf.Attributes = MemberAttributes.Public;
			indexOf.Parameters.Add(new CodeParameterDeclarationExpression(@namespace + "." + tbl.TableName, "value"));
			indexOf.Statements.Add(new CodeMethodReturnStatement(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "IndexOf", new CodeExpression[]{new CodeVariableReferenceExpression("value")})));

			@ref.Members.Add(indexOf);
		}

		internal static void GenerateInsertMethod(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace)
		{
			CodeMemberMethod insert = new CodeMemberMethod();

			insert.Name = "Insert";
			insert.ReturnType = new CodeTypeReference(typeof(void));
			insert.Attributes = MemberAttributes.Public;
			insert.Parameters.Add(new CodeParameterDeclarationExpression(typeof(int), "index"));
			insert.Parameters.Add(new CodeParameterDeclarationExpression(@namespace + "." + tbl.TableName, "value"));
			insert.Statements.Add(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "Insert", new CodeExpression[]{new CodeVariableReferenceExpression("index"), new CodeVariableReferenceExpression("value")}));

			@ref.Members.Add(insert);
		}

		internal static void GenerateRemoveMethod(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace)
		{
			CodeMemberMethod remove = new CodeMemberMethod();

			remove.Name = "Remove";
			remove.ReturnType = new CodeTypeReference(typeof(void));
			remove.Attributes = MemberAttributes.Public;
			remove.Parameters.Add(new CodeParameterDeclarationExpression(@namespace + "." + tbl.TableName, "value"));
			remove.Statements.Add(new CodeMethodInvokeExpression(new CodeBaseReferenceExpression(), "Remove", new CodeExpression[]{new CodeVariableReferenceExpression("value")}));

			@ref.Members.Add(remove);
		}

		internal static void GenerateIndexer(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace)
		{
			CodeMemberProperty indexer = new CodeMemberProperty();

			string typeName = @namespace + "." + tbl.TableName;
			indexer.Type = new CodeTypeReference(typeName);
			indexer.Name = "Item";
			indexer.Attributes = MemberAttributes.New | MemberAttributes.Public | MemberAttributes.Final;
			indexer.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(int)),"index"));

			indexer.GetStatements.Add(new CodeMethodReturnStatement(new CodeCastExpression(typeName, new CodeIndexerExpression(new CodeBaseReferenceExpression(), new CodeVariableReferenceExpression("index")))));

			@ref.Members.Add(indexer);
		}

		internal static void GenerateFromBaseArray(
			CodeTypeDeclaration @ref,
			SqlDbTable tbl,
			string @namespace,
			string dataNamespace)
		{
			CodeMemberMethod fromBase = new CodeMemberMethod();

			string typeName = @namespace + "." + tbl.TableName;
			string dataTypeName = dataNamespace + "." + tbl.TableName;

			fromBase.Name = "FromBaseArray";
			fromBase.ReturnType = new CodeTypeReference(typeName + "Collection");
			fromBase.Attributes = MemberAttributes.Public | MemberAttributes.Static | MemberAttributes.Final;
			fromBase.Parameters.Add(new CodeParameterDeclarationExpression("System.Collections.ArrayList", "a"));

			fromBase.Statements.Add(new CodeVariableDeclarationStatement(typeName + "Collection", "ret", new CodePrimitiveExpression(null)));

			CodeConditionStatement if_null = new CodeConditionStatement();
			if_null.Condition = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("ret"), CodeBinaryOperatorType.ValueEquality, new CodePrimitiveExpression(null));
			if_null.TrueStatements.Add(new CodeAssignStatement(new CodeVariableReferenceExpression("ret"), new CodeObjectCreateExpression(typeName + "Collection", new CodeExpression[0])));

			CodeIterationStatement transfer = new CodeIterationStatement();

			transfer.InitStatement = new CodeVariableDeclarationStatement(typeof(int), "i", new CodePrimitiveExpression(0));
			transfer.TestExpression = new CodeBinaryOperatorExpression(new CodeVariableReferenceExpression("i"), CodeBinaryOperatorType.LessThan, new CodePropertyReferenceExpression(new CodeVariableReferenceExpression("a"), "Count"));
			transfer.IncrementStatement = new CodeSnippetStatement("i++");

			transfer.Statements.Add(if_null);
			transfer.Statements.Add(
				new CodeMethodInvokeExpression(
					new CodeVariableReferenceExpression("ret"),
					"Add",
				new CodeExpression[]{
										new CodeMethodInvokeExpression(
										new CodeTypeReferenceExpression(typeName),
										"FromBase",
										new CodeExpression[]{
																new CodeCastExpression(
																	dataTypeName,
																	new CodeIndexerExpression(
																		new CodeVariableReferenceExpression("a"),
																		new CodeExpression[]{
																								new CodeVariableReferenceExpression("i")
																							}))})}));
					

			fromBase.Statements.Add(transfer);

			fromBase.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));
			@ref.Members.Add(fromBase);

		}

	}
}
