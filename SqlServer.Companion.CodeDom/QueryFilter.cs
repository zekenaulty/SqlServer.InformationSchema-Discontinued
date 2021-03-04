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
	/// Determines how the Sql variable will be comparied to it's table field counter part; 
	/// in relation to the Query being filtered.
	/// </summary>
	public enum FilterOperatorType
	{
		/// <summary>
		/// =
		/// </summary>
		Equal,
		/// <summary>
		/// !=
		/// </summary>
		NotEqual,
		/// <summary>
		/// <
		/// </summary>
		LessThan,
		/// <summary>
		/// >
		/// </summary>
		GreaterThan,
		/// <summary>
		/// <=
		/// </summary>
		LessThanEqual,
		/// <summary>
		/// >=
		/// </summary>
		GreaterThanEqual,
		/// <summary>
		/// Like
		/// </summary>
		Like
	}

	public class QueryFilterList:ArrayList
	{
		private ArrayList _fields = null;
		public ArrayList Fields
		{
			get
			{
				if(null == _fields)
				{
					_fields = new ArrayList();
					for(int i = 0; i < base.Count; i++)
						_fields.Add(((QueryFilter)base[i]).Field);
				}

				return _fields;
			}
		}
	}

	/// <summary>Provides the SqlStoredProcedureGenerator with information on how to filter a generated query.</summary>
	public class QueryFilter
	{
		#region Contruction
		public QueryFilter(){}
		#endregion
		#region Properties
		protected FilterOperatorType _op = FilterOperatorType.Equal;
		/// <summary>
		/// Determines how the Sql variable will be comparied to it's table field counter part; 
		/// in relation to the Query being filtered.
		/// </summary>
		public FilterOperatorType Operator
		{
			get{return _op;}
			set{_op = value;}
		}
		protected SqlDbTableField _fld = null;
		/// <summary>
		/// The field that is being used to derive the left and right side of the filter in the generated query.
		/// </summary>
		public SqlDbTableField Field
		{
			get{return _fld;}
			set{_fld = value;}
		}
		#endregion
		#region Methods
		/// <summary>
		/// Get the string representing the sql comparison that will filter the where clause of generated query.
		/// </summary>
		/// <returns>A string.</returns>
		public string FilterString(){return this.Field.ColumnName + " " + OperatorString(this.Operator) + " @" + this.Field.ColumnName;}
		/// <summary>
		/// Returns =, !=, ... depending on the in passed FilterOperatorType
		/// </summary>
		/// <param name="op">The FilterOperatorType to get the string operator for.</param>
		/// <returns>string</returns>
		public static string OperatorString(FilterOperatorType op)
		{
			string ret = "";

			switch(op.ToString())
			{
				case "Equal":
					ret = "=";
					break;
				case "NotEqual":
					ret = "!=";
					break;
				case "LessThan":
					ret = "<";
					break;
				case "GreaterThan":
					ret = ">";
					break;
				case "LessThanEqual":
					ret = "<=";
					break;
				case "GreaterThanEqual":
					ret = ">=";
					break;
				case "Like":
					ret = "LIKE";
					break;
			}

			return ret;
		}
		#endregion
	}
}
