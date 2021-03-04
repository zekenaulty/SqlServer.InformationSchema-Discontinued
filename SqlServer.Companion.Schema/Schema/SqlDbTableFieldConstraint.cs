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
	public sealed class SqlDbTableFieldConstraint : SqlDbTableConstraintBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTableFieldConstraint(){}
		#endregion
		#region COLUMN_NAME
		string column_name = string.Empty;
		public string ColumnName
		{
			get{return column_name;}
			set{if(value != column_name) column_name = value;}
		}
		#endregion
		#region CheckConstraints
		SqlDbCheckConstraint check_con = null;
		bool checked_for_cc = false;
		public SqlDbCheckConstraint CheckConstraint
		{
			get
			{
				if(check_con == null && !checked_for_cc)
				{
					check_con = SqlSchema.FindDbCheckConstraint(__conn, this.constraint_name);
					checked_for_cc = true;
				}
				return check_con;
			}
		}
		#endregion
		#region ReferentialConstraint
		SqlDbReferentialConstraint ref_ = null;
		public SqlDbReferentialConstraint ReferentialConstraint
		{
			get
			{
				if(ref_ == null)
					ref_ = SqlSchema.FindRefererentialConstraint(__conn, this.constraint_name);

				return ref_;
			}
		}
		#endregion
		#region ReferentialConstraints
		SqlDbReferentialConstraintCollection ref_con = null;
		public SqlDbReferentialConstraintCollection ReferentialConstraints
		{
			get
			{
				if(ref_con == null)
					ref_con = SqlSchema.FindRefererentialConstraints(__conn, this.constraint_name);

				return ref_con;
			}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbTableFieldConstraint Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableFieldConstraint fc = new SqlDbTableFieldConstraint();
			
			fc.__conn = connection;

			fc.table_catalog = (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			fc.table_schema = (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			fc.table_name = (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			fc.column_name = (r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			fc.constraint_catalog = (r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			fc.constraint_schema = (r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			fc.constraint_name = (r.IsDBNull(6)) ? string.Empty : r.GetString(6);

			return fc;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbTableFieldConstraintCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableFieldConstraintCollection fcc = new SqlDbTableFieldConstraintCollection();

			fcc.Add(Build(r, connection));
			while(r.Read())
				fcc.Add(Build(r, connection));

			return fcc;
		}
		#endregion
		#region +Attach CheckConstraint
		internal static SqlDbTableFieldConstraint Attach(SqlDbTableFieldConstraint field_constraint, SqlDbCheckConstraint check_constraint)
		{
			field_constraint.check_con = check_constraint;
			return field_constraint;
		}
		#endregion
		#region +Attach ReferentialConstraints
		internal static SqlDbTableFieldConstraint Attach(SqlDbTableFieldConstraint field_constraint,SqlDbReferentialConstraintCollection ref_constraints)
		{
			field_constraint.ref_con = ref_constraints;
			return field_constraint;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}