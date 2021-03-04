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
	public sealed class SqlDbTablePrivilege : SqlDbPrivilegeBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTablePrivilege(){}
		#endregion
		#region +Build DataReader
		internal static SqlDbTablePrivilege Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTablePrivilege tp = new SqlDbTablePrivilege();

			tp.__conn = connection;

			tp.grantor			= (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			tp.grantee			= (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			tp.table_catalog	= (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			tp.table_schema		= (r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			tp.table_name		= (r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			tp.privilege_type	= (r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			tp.is_grantable		= (r.IsDBNull(6)) ? string.Empty : r.GetString(6);

			return tp;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbTablePrivilegeCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTablePrivilegeCollection tpc = new SqlDbTablePrivilegeCollection();

			tpc.Add(Build(r, connection));
			while(r.Read())
				tpc.Add(Build(r, connection));

			return tpc;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
