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
	[Serializable]
	public sealed class SqlDbTableFieldPrivilege : SqlDbPrivilegeBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTableFieldPrivilege(){}
		#endregion
		#region COLUMN_NAME
		string column_name = string.Empty;
		public string ColumnName
		{
			get{return column_name;}
			//set{if(value != column_name) column_name = value;}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbTableFieldPrivilege Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableFieldPrivilege fp = new SqlDbTableFieldPrivilege();
			
			fp.__conn = connection;

			fp.grantor = (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			fp.grantee = (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			fp.table_catalog = (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			fp.table_schema = (r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			fp.table_name = (r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			fp.column_name = (r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			fp.privilege_type = (r.IsDBNull(6)) ? string.Empty : r.GetString(6);
			fp.is_grantable = (r.IsDBNull(7)) ? string.Empty : r.GetString(7);

			return fp;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbTableFieldPrivilegeCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableFieldPrivilegeCollection fpc = new SqlDbTableFieldPrivilegeCollection();

			fpc.Add(Build(r, connection));
			while(r.Read())
				fpc.Add(Build(r, connection));

			return fpc;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
