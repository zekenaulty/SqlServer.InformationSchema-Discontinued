using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace SqlServer.Companion
{
	/// <summary>
	/// Used to mark classes/structs as being a represintation of a row of data found in a table.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct,AllowMultiple = false, Inherited = true)]
	public class TableRowAttribute : Attribute
	{
		#region Construction
		/// <summary>
		/// Create a TableRow mark on an class/struct.
		/// </summary>
		/// <param name="name">The name of the table.</param>
		public TableRowAttribute(string name)
		{
			this.name = name;
		}
		#endregion
		#region Name
		string name = string.Empty;
		/// <summary>
		/// The name of the table.
		/// </summary>
		public string TableName
		{
			get{return name;}
		}
		#endregion

		//add GetAttributes
	}
}