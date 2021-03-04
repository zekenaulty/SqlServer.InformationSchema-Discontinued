using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace SqlServer.Companion.Exceptions
{
	/// <summary>
	/// Summary description for ExceptionBase.
	/// </summary>
	public class ExceptionBase : System.Exception
	{
		internal ExceptionBase(){}
		internal ExceptionBase(string message) : base(message){}
		internal ExceptionBase(string message, System.Exception inner): base(message, inner){}
	}
}
