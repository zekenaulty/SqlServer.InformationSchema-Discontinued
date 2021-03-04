using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	public abstract class SqlDbFieldBase : SqlDbTableBase, ISqlDataObject
	{
		#region COLUMN_NAME
		protected string column_name = string.Empty;
		public string ColumnName
		{
			get{return column_name;}
			//set{if(value != column_name) column_name = value;}
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}