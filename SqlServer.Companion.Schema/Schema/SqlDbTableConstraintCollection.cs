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
	public sealed class SqlDbTableConstraintCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbTableConstraintCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbTableConstraint value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbTableConstraint value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbTableConstraint value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbTableConstraint value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbTableConstraint this[int index]
		{
			get{return (SqlDbTableConstraint)base[index];}
		}
		#endregion
	}
}
