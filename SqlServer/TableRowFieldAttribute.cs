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
	/// Summary description for TableRowFieldAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field ,AllowMultiple = false, Inherited = true)]
	public class TableRowFieldAttribute : Attribute
	{
		#region Construction
		/// <summary>
		/// Mark a property or field as the representation of a table field.
		/// </summary>
		/// <param name="name">The name of the field.</param>
		/// <param name="datatype">The datatype of the field.</param>
		/// <param name="length">The max length or numeric precision of the field.</param>
		public TableRowFieldAttribute(string name, string datatype, bool nullable, int length, int oridinal_position)
		{
			_name = name;
			_datatype = datatype;
			_len = length;
			_ord = oridinal_position;
			_null = nullable;
		}
		#endregion
		#region Name
		string _name = string.Empty;
		/// <summary>
		/// The name of the field.
		/// </summary>
		public string FieldName
		{
			get{return _name;}
		}
		#endregion
		private string _datatype = "";
		public string DataType{get{return _datatype;}}

		private int _len = 0;
		public int Length{get{return _len;}}

		private int _ord = 0;
		public int OridinalPosition{get{return _ord;}}

		private bool _null = true;
		public bool Nullable{get{return _null;}}
		
		//add GetAttributes

	}
}
