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
	public sealed class SqlDbViewCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbViewCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbView value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbView value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbView value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbView value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbView this[int index]
		{
			get{return (SqlDbView)base[index];}
		}
		#endregion
	}
}
