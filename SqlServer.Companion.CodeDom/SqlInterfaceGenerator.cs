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
	public class SqlInterfaceGenerator
	{
		public static string GetInterfaceName(
			SqlDbTable table,
			string @namespace)
		{
			string my_namespace = @namespace.Substring(0, @namespace.IndexOf("."));
			my_namespace += ".Structure";
			my_namespace += ".I" + table.TableName;
			return my_namespace;
		}
		public static void GenerateTableInterface(
			SqlDbTable table,
			Language lang,
			string @namespace,
			string root_dir)
		{
			CodeCompileUnit comp = new CodeCompileUnit();

			string my_namespace = @namespace.Substring(0, @namespace.IndexOf("."));
			my_namespace += ".Structure";
			CodeNamespace nspace = new CodeNamespace(my_namespace);
			CodeTypeDeclaration obj = new CodeTypeDeclaration("I" + table.TableName);
			//System.CodeDom.CodeConstructor objCon = new CodeConstructor();

			obj.Attributes = MemberAttributes.Public;
			obj.TypeAttributes = TypeAttributes.Public | TypeAttributes.Interface;

			nspace.Imports.Add(new CodeNamespaceImport("System"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.Common"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.SqlClient"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Data.SqlTypes"));
			nspace.Imports.Add(new CodeNamespaceImport("System.Xml"));
			
			//add SqlServer.Companion imports/using
			nspace.Imports.Add(new CodeNamespaceImport("SqlServer.Companion"));
			nspace.Imports.Add(new CodeNamespaceImport("SqlServer.Companion.Base"));

			foreach(SqlDbTableField fld in table.Columns)
			{
				CodeMemberProperty prop = new CodeMemberProperty();
				prop.Name = fld.ColumnName;
				prop.Type = new CodeTypeReference(fld.ValueType);

				prop.GetStatements.Add(new CodeSnippetExpression(""));
				prop.SetStatements.Add(new CodeSnippetExpression(""));
				obj.Members.Add(prop);
			}

			nspace.Types.Add(obj);
			comp.Namespaces.Add(nspace);

			SqlObjectWriter.WriteTableInterface(comp, lang, table, my_namespace, root_dir, false);

		}
	}
}
