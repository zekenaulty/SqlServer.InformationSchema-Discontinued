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
	public sealed class SqlDbTablePrivilegeCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbTablePrivilegeCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbTablePrivilege value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbTablePrivilege value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbTablePrivilege value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbTablePrivilege value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbTablePrivilege this[int index]
		{
			get{return (SqlDbTablePrivilege)base[index];}
		}
		#endregion
	}
}
