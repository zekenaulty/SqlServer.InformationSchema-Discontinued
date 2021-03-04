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
	public sealed class SqlObjectWriter
	{
		public static void WriteTableInterface(
			CodeCompileUnit code,
			Language lang,
			SqlDbTable tbl,
			string @namespace,
			string root_dir,
			bool isMember)
		{
			#region Variables
			ICodeGenerator gen = null;
			IndentedTextWriter tw = null;
			CodeGeneratorOptions op = null;

			//string rootnspace = tbl.TableCatalog + ".Data";
			string nspace = @namespace;
			string file = root_dir;

			#endregion
			#region Configure File path and Namespaces
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";

			file += nspace.Replace(".", "\\");
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";
			
			if(Directory.Exists(file) == false)
				Directory.CreateDirectory(file);

			if(lang == Language.CSharp)
			{
				file += "I" + tbl.TableName;
				if (isMember == true) 
					file += "Members.cs"; 
				else
					file += ".cs";
			}
			else
			{
				file += "I" + tbl.TableName;
				if (isMember == true) 
					file += "Members.vb"; 
				else
					file += ".vb";
			}

			if(File.Exists(file) == true)
				File.Delete(file);

			#endregion
			#region Prep Genrator
			if(lang == Language.CSharp)
			{
				CSharpCodeProvider p = new CSharpCodeProvider();
				gen = p.CreateGenerator();
			}
			else
			{
				VBCodeProvider p = new VBCodeProvider();
				gen = p.CreateGenerator();
			}
			#endregion
			#region Prep for file writing
			tw = new IndentedTextWriter(new StreamWriter(file, false), "\t");
			op = new CodeGeneratorOptions();

			#endregion
			#region Set Options
			op.BlankLinesBetweenMembers = true;
			op.BracingStyle = "C";
			op.IndentString = "\t";

			#endregion
			#region Output file
			gen.GenerateCodeFromCompileUnit(code, tw, op);
			
			if(null != tw)
				tw.Close();	
			#endregion
		}
		public static void WriteTableDataClass(
			CodeCompileUnit code,
			Language lang,
			SqlDbTable tbl, 
			string @namespace, 
			string root_dir)
		{
			#region Variables
			ICodeGenerator gen = null;
			IndentedTextWriter tw = null;
			CodeGeneratorOptions op = null;

			//string rootnspace = tbl.TableCatalog + ".Data";
			string nspace = @namespace;
			string file = root_dir;

			#endregion
			#region Configure File path and Namespaces
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";

			file += nspace.Replace(".", "\\");
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";
			
			if(Directory.Exists(file) == false)
				Directory.CreateDirectory(file);

			if(lang == Language.CSharp)
				file += tbl.TableName + ".cs";
			else
				file += tbl.TableName + ".vb";

			if(File.Exists(file) == true)
				File.Delete(file);

			#endregion
			#region Prep Genrator
			if(lang == Language.CSharp)
			{
				CSharpCodeProvider p = new CSharpCodeProvider();
				gen = p.CreateGenerator();
			}
			else
			{
				VBCodeProvider p = new VBCodeProvider();
				gen = p.CreateGenerator();
			}
			#endregion
			#region Prep for file writing
			tw = new IndentedTextWriter(new StreamWriter(file, false), "\t");
			op = new CodeGeneratorOptions();

			#endregion
			#region Set Options
			op.BlankLinesBetweenMembers = true;
			op.BracingStyle = "C";
			op.IndentString = "\t";

			#endregion
			#region Output file
			gen.GenerateCodeFromCompileUnit(code, tw, op);
			
			if(null != tw)
				tw.Close();	
			#endregion
		}


		public static void WriteTableBusinessClass(
			CodeCompileUnit code,
			Language lang,
			SqlDbTable tbl, 
			string @namespace, 
			string root_dir)
		{
			#region Variables
			ICodeGenerator gen = null;
			IndentedTextWriter tw = null;
			CodeGeneratorOptions op = null;

			//string rootnspace = tbl.TableCatalog + ".Data";
			string nspace = @namespace;
			string file = root_dir;

			#endregion
			#region Configure File path and Namespaces
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";

			file += nspace.Replace(".", "\\");
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";
			
			if(Directory.Exists(file) == false)
				Directory.CreateDirectory(file);

			if(lang == Language.CSharp)
				file += tbl.TableName + ".cs";
			else
				file += tbl.TableName + ".vb";

			if(File.Exists(file) == true)
				File.Delete(file);

			#endregion
			#region Prep Genrator
			if(lang == Language.CSharp)
			{
				CSharpCodeProvider p = new CSharpCodeProvider();
				gen = p.CreateGenerator();
			}
			else
			{
				VBCodeProvider p = new VBCodeProvider();
				gen = p.CreateGenerator();
			}
			#endregion
			#region Prep for file writing
			tw = new IndentedTextWriter(new StreamWriter(file, false), "\t");
			op = new CodeGeneratorOptions();

			#endregion
			#region Set Options
			op.BlankLinesBetweenMembers = true;
			op.BracingStyle = "C";
			op.IndentString = "\t";

			#endregion
			#region Output file
			gen.GenerateCodeFromCompileUnit(code, tw, op);
			
			if(null != tw)
				tw.Close();	
			#endregion
		}

		public static void WriteTableBusinessCollectionClass(
			CodeCompileUnit code,
			Language lang,
			SqlDbTable tbl, 
			string @namespace, 
			string root_dir)
		{
			#region Variables
			ICodeGenerator gen = null;
			IndentedTextWriter tw = null;
			CodeGeneratorOptions op = null;

			//string rootnspace = tbl.TableCatalog + ".Data";
			string nspace = @namespace;
			string file = root_dir;

			#endregion
			#region Configure File path and Namespaces
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";

			file += nspace.Replace(".", "\\");
			if(file[file.Length - 1].ToString() != "\\")
				file += "\\";
			
			if(Directory.Exists(file) == false)
				Directory.CreateDirectory(file);

			if(lang == Language.CSharp)
				file += tbl.TableName + "Collection.cs";
			else
				file += tbl.TableName + "Collection.vb";

			if(File.Exists(file) == true)
				File.Delete(file);

			#endregion
			#region Prep Genrator
			if(lang == Language.CSharp)
			{
				CSharpCodeProvider p = new CSharpCodeProvider();
				gen = p.CreateGenerator();
			}
			else
			{
				VBCodeProvider p = new VBCodeProvider();
				gen = p.CreateGenerator();
			}
			#endregion
			#region Prep for file writing
			tw = new IndentedTextWriter(new StreamWriter(file, false), "\t");
			op = new CodeGeneratorOptions();

			#endregion
			#region Set Options
			op.BlankLinesBetweenMembers = true;
			op.BracingStyle = "C";
			op.IndentString = "\t";

			#endregion
			#region Output file
			gen.GenerateCodeFromCompileUnit(code, tw, op);
			
			if(null != tw)
				tw.Close();	
			#endregion
		}
	}
}
