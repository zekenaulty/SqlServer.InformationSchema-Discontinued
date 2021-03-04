using System;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary>
	/// Summary description for SqlDbReferentialConstraintCollection.
	/// </summary>
	public sealed class SqlDbReferentialConstraintCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbReferentialConstraintCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbReferentialConstraint value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbReferentialConstraint value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbReferentialConstraint value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbReferentialConstraint value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbReferentialConstraint this[int index]
		{
			get{return (SqlDbReferentialConstraint)base[index];}
		}
		#endregion
	}

}
