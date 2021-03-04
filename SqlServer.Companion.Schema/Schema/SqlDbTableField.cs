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
	public sealed class SqlDbTableField : SqlDbTableFieldBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbTableField(){}
		#endregion
		#region Constraints
		SqlDbTableFieldConstraintCollection con = null;
		public SqlDbTableFieldConstraintCollection Constraints
		{
			get
			{
				if(null == con)
					con = SqlSchema.FindTableFieldConstraints(__conn, this.TableName, this.ColumnName);

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
				if(null == priv)
					priv = SqlSchema.FindTableFieldPrivileges(__conn, this.TableName, this.ColumnName);

				return priv;
			}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbTableField Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableField f = new SqlDbTableField();

			f.__conn = connection;

			f.table_catalog				= (r.IsDBNull(0)) ? string.Empty	: r.GetString(0);
			f.table_schema				= (r.IsDBNull(1)) ? string.Empty	: r.GetString(1);
			f.table_name				= (r.IsDBNull(2)) ? string.Empty	: r.GetString(2);
			f.column_name				= (r.IsDBNull(3)) ? string.Empty	: r.GetString(3);
			f.ordinal_position			= (r.IsDBNull(4)) ? short.MinValue	: r.GetInt32(4);
			f.column_default			= (r.IsDBNull(5)) ? string.Empty	: r.GetString(5);
			f.is_nullable				= (r.IsDBNull(6)) ? string.Empty	: r.GetString(6);
			f.data_type					= (r.IsDBNull(7)) ? string.Empty	: r.GetString(7);
			f.charachter_maximum_length	= (r.IsDBNull(8)) ? int.MinValue	: r.GetInt32(8);
			f.charachter_octet_length	= (r.IsDBNull(9)) ? int.MinValue	: r.GetInt32(9);
			f.numeric_precision			= (r.IsDBNull(10)) ? int.MinValue	: int.Parse(r[10].ToString());
			f.numeric_precision_radix	= (r.IsDBNull(11)) ? int.MinValue	: int.Parse(r[11].ToString());
			f.numeric_scale				= (r.IsDBNull(12)) ? int.MinValue	: int.Parse(r[12].ToString());

			return f;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbTableFieldCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbTableFieldCollection flds = new SqlDbTableFieldCollection();

			flds.Add(Build(r,connection));
			while(r.Read())
			{
				flds.Add(Build(r,connection));
			}

			return flds;
		}
		#endregion
		#region +Attach Constraints
		internal static SqlDbTableField Attach(SqlDbTableField fld, SqlDbTableFieldConstraintCollection constraints)
		{
			fld.con = constraints;
			return fld;
		}
		#endregion
		#region +Attach Privileges
		internal static SqlDbTableField Attach(SqlDbTableField fld, SqlDbTableFieldPrivilegeCollection privileges)
		{
			fld.priv = privileges;
			return fld;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}