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
	public sealed class SqlDbCheckConstraint : SqlDbConstraintBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbCheckConstraint(){}
		#endregion
		#region CHECK_CLAUSE
		string check_clause = string.Empty;
		public string CheckClause
		{
			get{return check_clause;}
			set{if(value != check_clause) check_clause = value;}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbCheckConstraint Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbCheckConstraint cc = new SqlDbCheckConstraint();

			cc.__conn = connection;

			cc.constraint_catalog	=	(r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			cc.constraint_schema	=	(r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			cc.constraint_name	=	(r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			cc.check_clause	=	(r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			cc.__conn = connection;
			return cc;
		}
		#endregion
		#region +BuildCollection DataReader
		internal static SqlDbCheckConstraintCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbCheckConstraintCollection ccc = new SqlDbCheckConstraintCollection();
			
			ccc.Add(Build(r, connection));
			while(r.Read())
				ccc.Add(Build(r, connection));

			return ccc;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
