using System;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	[Serializable]
	public sealed class SqlDbTableFieldPrivilegeCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbTableFieldPrivilegeCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbTableFieldPrivilege value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbTableFieldPrivilege value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbTableFieldPrivilege value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbTableFieldPrivilege value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbTableFieldPrivilege this[int index]
		{
			get{return (SqlDbTableFieldPrivilege)base[index];}
		}
		#endregion
	}
}
