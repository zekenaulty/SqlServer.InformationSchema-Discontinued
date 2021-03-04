using System;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	public class SqlDbTablePrimaryKey: SqlDbTableFieldBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTablePrimaryKey(){}
		#endregion
		#region Constraints
		SqlDbTableFieldConstraintCollection con = null;
		public SqlDbTableFieldConstraintCollection Constraints
		{
			get
			{
				return con;
			}
		}
		#endregion
		#region Privileges
		SqlDbTableFieldPrivilegeCollection priv = null;
		public SqlDbTableFieldPrivilegeCollection Privileges
		{
			get
			{
				return priv;
			}
		}
		#endregion
		#region (SqlDbTableField)
		
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
