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
	public sealed class SqlDbRoutineFieldCollection : SqlDataObjectCollectionBase
	{
		#region Construction
		public SqlDbRoutineFieldCollection(){}
		#endregion
		#region Add
		public int Add(SqlDbRoutineField value)
		{
			return base.Add(value);
		}
		#endregion
		#region IndexOf
		public int IndexOf(SqlDbRoutineField value)
		{
			return base.IndexOf(value);
		}
		#endregion
		#region Insert
		public void Insert(int index, SqlDbRoutineField value)
		{
			base.Insert(index, value);
		}
		#endregion
		#region Remove
		public void Remove(SqlDbRoutineField value)
		{
			base.Remove(value);
		}
		#endregion
		#region Indexer
		new public SqlDbRoutineField this[int index]
		{
			get{return (SqlDbRoutineField)base[index];}
		}
		#endregion
	}
}
