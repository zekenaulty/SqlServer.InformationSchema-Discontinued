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
	public sealed class SqlDbRoutineParameterCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbRoutineParameterCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbRoutineParameter value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbRoutineParameter value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbRoutineParameter value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbRoutineParameter value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbRoutineParameter this[int index]
		{
			get{return (SqlDbRoutineParameter)base[index];}
		}
		#endregion
	}
}
