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
	public sealed class SqlDbTable : SqlDbTableBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTable(){}
		#endregion
		#region TABLE_TYPE
		string table_type = string.Empty;
		public string TableType
		{
			get{return table_type;}
			//set{if(value != table_type) table_type = value;}
		}
		#endregion
		#region Columns
		SqlDbTableFieldCollection col = null;
		public SqlDbTableFieldCollection Columns
		{
			get
			{
				if(null == col)
					col = SqlSchema.FindTableFields(__conn, this.TableName);

				return col;
			}
		}
		#endregion
		#region Constraints
		SqlDbTableConstraintCollection con = null;
		public SqlDbTableConstraintCollection Constraints
		{
			get
			{
				if(null == con)
					con = SqlSchema.FindTableConstraints(__conn, this.TableName);

				return con;
			}
		}
		#endregion
		#region Previleges
		SqlDbTablePrivilegeCollection priv = null;
		public SqlDbTablePrivilegeCollection Privileges
		{
			get
			{
				if(null == priv)
					priv = SqlSchema.FindTablePrivileges(__conn, this.TableName);

				return priv;
			}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbTable Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTable t = new SqlDbTable();

			t.__conn = connection;

			t.table_catalog = (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			t.table_schema	= (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			t.table_name	= (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			t.table_type	= (r.IsDBNull(3)) ? string.Empty : r.GetString(3);

			//t.con = SqlSchema.FindTableConstraints(t.__conn, t.TableName);
			//t.priv = SqlSchema.FindTablePrivileges(t.__conn, t.TableName);

			return t;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbTableCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableCollection tc = new SqlDbTableCollection();

			tc.Add(Build(r, connection));
			while(r.Read())
				tc.Add(Build(r, connection));

			return tc;
		}
		#endregion
		#region +Attach Columns
		internal static SqlDbTable Attach(SqlDbTable tbl, SqlDbTableFieldCollection columns)
		{
			tbl.col = columns;
			return tbl;

		}
		#endregion
		#region +Attach Constraints
		internal static SqlDbTable Attach(SqlDbTable tbl, SqlDbTableConstraintCollection constraints)
		{
			tbl.con = constraints;
			return tbl;
		}
		#endregion
		#region +Attach Privileges
		internal static SqlDbTable Attach(SqlDbTable tbl, SqlDbTablePrivilegeCollection privileges)
		{
			tbl.priv = privileges;
			return tbl;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}