using System;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	[Serializable]
	public sealed class SqlDbViewFieldCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbViewFieldCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbViewField value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbViewField value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbViewField value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbViewField value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbViewField this[int index]
		{
			get{return (SqlDbViewField)base[index];}
		}
		#endregion
	}
}
