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
	public sealed class SqlDbTableFieldCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbTableFieldCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbTableField value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbTableField value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbTableField value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbTableField value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbTableField this[int index]
		{
			get{return (SqlDbTableField)base[index];}
		}
		#endregion
	}
}
