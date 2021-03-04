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
	public abstract class SqlDbTableBase : SqlBase, ISqlDataObject
	{
		#region TABLE_CATALOG
		protected string table_catalog = string.Empty;
		public string TableCatalog
		{
			get{return table_catalog;}
			//set{if(value != table_catalog) table_catalog = value;}
		}
		#endregion
		#region TABLE_SCHEMA
		protected string table_schema = string.Empty;
		public string TableSchema
		{
			get{return table_schema;}
			//set{if(value != table_schema) table_schema = value;}
		}
		#endregion
		#region TABLE_NAME
		protected string table_name = string.Empty;
		public string TableName
		{
			get{return table_name;}
			//set{if(value != table_name) table_name = value;}
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
