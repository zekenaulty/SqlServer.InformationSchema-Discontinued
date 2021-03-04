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
	public sealed class SqlDbRoutineCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbRoutineCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbRoutine value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbRoutine value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbRoutine value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbRoutine value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbRoutine this[int index]
		{
			get{return (SqlDbRoutine)base[index];}
		}
		#endregion
	}
}
