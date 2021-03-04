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
	public sealed class SqlDbTableConstraint : SqlDbTableConstraintBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTableConstraint(){}
		#endregion
		#region CONSTRAINT_TYPE
		string constraint_type = string.Empty;
		public string ConstraintType
		{
			get{return constraint_type;}
			//set{if(value != constraint_type) constraint_type = value;}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbTableConstraint Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableConstraint t = new SqlDbTableConstraint();

			t.__conn = connection;

			t.constraint_catalog = (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			t.constraint_schema = (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			t.constraint_name = (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			t.table_catalog = (r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			t.table_schema = (r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			t.table_name = (r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			t.constraint_type = (r.IsDBNull(6)) ? string.Empty : r.GetString(6);

			//create sql to get pk column name
			string sql = "SELECT INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE.COLUMN_NAME "
				+ "FROM "
				+ "INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE "
				+ "WHERE "
				+ "CONSTRAINT_NAME = '" + t.constraint_name + "' ";

			SqlDataReader rc = t.__conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			if(rc.Read())
				t.col = (rc.IsDBNull(0)) ? string.Empty : rc.GetString(0);

			if(t.constraint_type == "PRIMARY KEY")
			{
				t.isPK = true; //mark as primary key


			}
			else if(t.constraint_type == "FOREIGN KEY")
			{
				t.isFK = true; //mark as being a foreign key
			}
			else if(t.constraint_type == "CHECK")
			{
				t.isCHK = true; // as being a check constraint
			}
			return t;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbTableConstraintCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableConstraintCollection tcc = new SqlDbTableConstraintCollection();

			tcc.Add(Build(r, connection));
			while(r.Read())
				tcc.Add(Build(r, connection));

			return tcc;
		}
		#endregion 
		new public object Clone()
		{
			return base.Clone();
		}

		string col = string.Empty;
		public string ConstraintColumnName
		{
			get{return col;}
		}
		bool isPK = false;
		public bool IsPrimaryKey
		{
			get{return isPK;}
		}
		bool isFK = false;
		public bool IsForeignKey
		{
			get{return isFK;}
		}
		bool isCHK = false;
		public bool IsCheck
		{
			get{return isCHK;}
		}


	}
}
