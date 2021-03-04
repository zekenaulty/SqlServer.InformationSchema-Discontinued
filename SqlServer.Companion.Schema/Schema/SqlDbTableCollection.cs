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
	public sealed class SqlDbTableCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbTableCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbTable value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbTable value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbTable value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbTable value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbTable this[int index]
		{
			get{return (SqlDbTable)base[index];}
		}
		#endregion
	}
}
