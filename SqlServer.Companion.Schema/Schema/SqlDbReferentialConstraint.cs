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
	public sealed class SqlDbReferentialConstraint : SqlDbConstraintBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbReferentialConstraint(){}
		#endregion
		#region UNIQUE_CONSTRAINT_CATALOG
		string unique_constraint_catalog = string.Empty;
		public string UniqueConstraintCatalog
		{
			get{return unique_constraint_catalog;}
			//set{if(value != unique_constraint_catalog) unique_constraint_catalog = value;}
		}
		#endregion
		#region UNIQUE_CONSTRAINT_SCHEMA 
		string unique_constraint_schema = string.Empty;
		public string UniqueConstraintSchema
		{
			get{return unique_constraint_schema;}
			//set{if(value != unique_constraint_schema) unique_constraint_schema = value;}
		}
		#endregion
		#region UNIQUE_CONSTRAINT_NAME 
		string unique_constraint_name = string.Empty;
		public string UniqueConstraintName
		{
			get{return unique_constraint_name;}
			//set{if(value != unique_constraint_name) unique_constraint_name = value;}
		}
		#endregion
		private SqlDbTableFieldConstraint uniqueConstraint = null;
		public SqlDbTableFieldConstraint UniqueConstraint
		{
			get
			{
				if(null == uniqueConstraint)
					uniqueConstraint = SqlSchema.FindTableFieldConstraint(__conn, unique_constraint_name);
				return uniqueConstraint;
			}
		}
		#region UPDATE_RULE
		string update_rule = string.Empty;
		public string UpdateRule
		{
			get{return update_rule;}
			//set{if(value != update_rule) update_rule = value;}
		}
		#endregion
		#region DELETE_RULE
		string delete_rule = string.Empty;
		public string DeleteRule
		{
			get{return delete_rule;}
			//set{if(value != delete_rule) delete_rule = value;}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbReferentialConstraint Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbReferentialConstraint rc = new SqlDbReferentialConstraint();

			rc.__conn = connection;

			rc.constraint_catalog			=	(r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			rc.constraint_schema			=	(r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			rc.constraint_name				=	(r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			rc.unique_constraint_catalog	=	(r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			rc.unique_constraint_schema		=	(r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			rc.unique_constraint_name		=	(r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			rc.update_rule					=	(r.IsDBNull(6)) ? string.Empty : r.GetString(6);
			rc.delete_rule					=	(r.IsDBNull(7)) ? string.Empty : r.GetString(7);
			rc.__conn = connection;

			return rc;
		}
		#endregion
		#region +BuildCollection DataReader
		internal static SqlDbReferentialConstraintCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbReferentialConstraintCollection rcc = new SqlDbReferentialConstraintCollection();
			
			rcc.Add(Build(r, connection));
			while(r.Read())
				rcc.Add(Build(r, connection));

			return rcc;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}