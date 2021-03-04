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
	public sealed class SqlDbCheckConstraintCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbCheckConstraintCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbCheckConstraint value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbCheckConstraint value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbCheckConstraint value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbCheckConstraint value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbCheckConstraint this[int index]
		{
			get{return (SqlDbCheckConstraint)base[index];}
		}
		#endregion
	}
}