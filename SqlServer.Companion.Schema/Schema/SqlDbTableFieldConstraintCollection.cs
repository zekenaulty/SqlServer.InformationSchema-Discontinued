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
	public sealed class SqlDbTableFieldConstraintCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbTableFieldConstraintCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbTableFieldConstraint value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbTableFieldConstraint value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbTableFieldConstraint value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbTableFieldConstraint value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbTableFieldConstraint this[int index]
		{
			get{return (SqlDbTableFieldConstraint)base[index];}
		}
		#endregion
	}
}
