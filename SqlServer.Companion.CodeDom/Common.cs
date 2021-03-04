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
	/// Generation output language.
	/// </summary>
	public enum Language
	{
		/// <summary>
		/// Output code in the C# programming language.
		/// </summary>
		CSharp,
		/// <summary>
		/// Output code in the VB programming language.
		/// </summary>
		VB
	}

	/// <summary>Routines/Methods/Stuff that did not require it's own file or class.</summary>
	internal sealed class Common
	{
		internal static void GenerateXmlSerializationSaveToXML(CodeTypeDeclaration @ref, string @namespace)
		{
			CodeMemberMethod saveXml = new CodeMemberMethod();

			saveXml.ReturnType = new CodeTypeReference(typeof(void));
			saveXml.Name = "WriteXML";
			saveXml.Attributes = MemberAttributes.Public;
			saveXml.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.String), "filepath"));

			saveXml.Statements.Add(new CodeVariableDeclarationStatement(
				typeof(System.Xml.Serialization.XmlSerializer),
				"serial",
				new CodeObjectCreateExpression(
					typeof(System.Xml.Serialization.XmlSerializer),
				new CodeExpression[]{
										new CodeMethodInvokeExpression(new CodeThisReferenceExpression(), "GetType", new CodeExpression[0])
									})));

			saveXml.Statements.Add(new CodeVariableDeclarationStatement(
				typeof(System.IO.TextWriter), 
				"write",
				new CodeObjectCreateExpression(
										typeof(System.IO.StreamWriter),
										new CodeExpression[]{new CodeVariableReferenceExpression("filepath")})
									));
										 

			saveXml.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("serial"),
				"Serialize",
				new CodeExpression[]{new CodeVariableReferenceExpression("write"), new CodeThisReferenceExpression()}));

			saveXml.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("write"), "Close", new CodeExpression[0]));

			@ref.Members.Add(saveXml);
		}

		internal static void GenerateXmlSerializationLoadFromXML(CodeTypeDeclaration @ref, string @namespace)
		{
			CodeMemberMethod loadXml = new CodeMemberMethod();

			loadXml.ReturnType = new CodeTypeReference(@namespace + "." + @ref.Name);
			loadXml.Name = "LoadXML";
			loadXml.Attributes = MemberAttributes.Public | MemberAttributes.Static;
			loadXml.Parameters.Add(new CodeParameterDeclarationExpression(typeof(System.String), "filepath"));

		
			loadXml.Statements.Add(new CodeVariableDeclarationStatement(
				typeof(System.Xml.Serialization.XmlSerializer),
				"serial",
				new CodeObjectCreateExpression(
				typeof(System.Xml.Serialization.XmlSerializer),
				new CodeExpression[]{
										new CodeTypeOfExpression(@namespace + "." + @ref.Name)
									})));

			loadXml.Statements.Add(new CodeVariableDeclarationStatement(
				typeof(System.IO.TextReader), 
				"read",
				new CodeObjectCreateExpression(
										typeof(System.IO.StreamReader),
										new CodeExpression[]{new CodeVariableReferenceExpression("filepath")})
									));

			loadXml.Statements.Add(new CodeVariableDeclarationStatement(@namespace + "." + @ref.Name, "ret", new CodeCastExpression(@namespace + "." + @ref.Name, new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("serial"), "Deserialize", new CodeExpression[]{ new CodeVariableReferenceExpression("read")}))));
		
			loadXml.Statements.Add(new CodeMethodInvokeExpression(new CodeVariableReferenceExpression("read"), "Close", new CodeExpression[0]));

			loadXml.Statements.Add(new CodeMethodReturnStatement(new CodeVariableReferenceExpression("ret")));

			@ref.Members.Add(loadXml);

		}
	}
}
