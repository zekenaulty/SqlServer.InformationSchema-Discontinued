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
	public sealed class SqlDbRoutineField : SqlDbTableFieldBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbRoutineField(){}
		#endregion
		#region +Build DataReader
		internal static SqlDbRoutineField Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbRoutineField f = new SqlDbRoutineField();

			f.__conn = connection;

			f.table_catalog				= (r.IsDBNull(0)) ? string.Empty	: r.GetString(0);
			f.table_schema				= (r.IsDBNull(1)) ? string.Empty	: r.GetString(1);
			f.table_name				= (r.IsDBNull(2)) ? string.Empty	: r.GetString(2);
			f.column_name				= (r.IsDBNull(3)) ? string.Empty	: r.GetString(3);
			f.ordinal_position			= (r.IsDBNull(4)) ? short.MinValue	: r.GetInt16(4);
			f.column_default			= (r.IsDBNull(5)) ? string.Empty	: r.GetString(5);
			f.is_nullable				= (r.IsDBNull(6)) ? string.Empty	: r.GetString(6);
			f.charachter_maximum_length	= (r.IsDBNull(7)) ? int.MinValue	: r.GetInt32(7);
			f.charachter_octet_length	= (r.IsDBNull(8)) ? int.MinValue	: r.GetInt32(8);
			f.numeric_precision			= (r.IsDBNull(9)) ? short.MinValue	: r.GetInt16(9);
			f.numeric_precision_radix	= (r.IsDBNull(10)) ? short.MinValue	: r.GetInt16(10);
			f.numeric_scale				= (r.IsDBNull(11)) ? short.MinValue	: r.GetInt16(11);

			return f;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbRoutineFieldCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbRoutineFieldCollection rfc = new SqlDbRoutineFieldCollection();

			rfc.Add(Build(r, connection));
			while(r.Read())
				rfc.Add(Build(r, connection));

			return rfc;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
