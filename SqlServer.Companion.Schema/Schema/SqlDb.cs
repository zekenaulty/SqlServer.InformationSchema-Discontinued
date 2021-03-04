using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary>Provides access the structure of a SqlSever database.</summary>
	/// <remarks>
	/// Intended to be used in the creation of dynaimc sql, stored procedure generation, 
	/// n-tier object and object collection generation, typed data set generation, 
	/// DataReader generation, and extensable code generation. 
	/// See SqlServer.Companion.CodeDom. 
	/// 
	/// </remarks>
	[Serializable]
	public sealed class SqlDb : SqlBase, ISqlDataObject
	{
		#region Construction
		internal SqlDb(){}
		#endregion
		#region CATALOG_NAME
		string name = string.Empty;
		public string CatalogName
		{
			get{return name;}
			//set{if(value != name) name = value;}
		}
		#endregion
		#region Routines
		SqlDbRoutineCollection routines = null;
		public SqlDbRoutineCollection Routines
		{
			get{return routines;}
		}
		#endregion
		#region Tables
		SqlDbTableCollection tables = null;
		public SqlDbTableCollection Tables
		{
			get
			{
				if(null == tables)
					tables = SqlSchema.FindTables(__conn, LoadType.Lazy);

				return tables;
			}
		}
		#endregion
		#region Views
		SqlDbViewCollection views = null;
		public SqlDbViewCollection Views
		{
			get
			{
				if(views == null)
					views = SqlSchema.FindViews(__conn, LoadType.Lazy);

				return views;
			}
		}
		#endregion
		#region +Build String
		internal static SqlDb Build(string catalog, SqlConnectionSource connection)
		{
			SqlDb db = new SqlDb();
			db.name = catalog;
			db.__conn = connection;
			return db;
		}
		#endregion
		#region +Attach Routines
		internal static SqlDb Attach(SqlDb db, SqlDbRoutineCollection routines)
		{
			db.routines = routines;
			return db;
		}
		#endregion
		#region +Attach Tables
		internal static SqlDb Attach(SqlDb db, SqlDbTableCollection tables)
		{
			db.tables = tables;
			return db;
		}
		#endregion
		#region +Attach CheckConstraints
//		internal static SqlDb Attach(SqlDb db, SqlDbCheckConstraintCollection check_constraints)
//		{
//			db.check_con = check_constraints;
//			return db;
//		}
		#endregion
		#region +Attach Views
		internal static SqlDb Attach(SqlDb db, SqlDbViewCollection views)
		{
			db.views = views;
			return db;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}