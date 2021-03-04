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
	public sealed class SqlDbRoutine : SqlDbRoutineBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbRoutine(){}
		#endregion
		#region ROUTINE_CATALOG
		string routine_catalog = string.Empty;
		public string RoutineCatalog
		{
			get{return routine_catalog;}
			//set{if(value != routine_catalog) routine_catalog = value;}
		}
		#endregion
		#region ROUTINE_SCHEMA
		string routine_schema = string.Empty;
		public string RoutineSchema
		{
			get{return routine_schema;}
			//set{if(value != routine_schema) routine_schema = value;}
		}
		#endregion
		#region ROUTINE_TYPE
		string routine_type = string.Empty;
		public string RoutineType
		{
			get{return routine_type;}
			//set{if(value != routine_type) routine_type = value;}
		}
		#endregion
		#region COLLATION_NAME
		string collation_name = string.Empty;
		public string CollationName
		{
			get{return collation_name;}
			//set{if(value != collation_name) collation_name = value;}
		}
		#endregion
		#region ROUTINE_BODY
		string routine_body = string.Empty;
		public string RoutineBody
		{
			get{return routine_body;}
			//set{if(value != routine_body) routine_body = value;}
		}
		#endregion
		#region ROUTINE_NAME
		string routine_name = string.Empty;
		public string RoutineName
		{
			get{return routine_name;}
			//set{if(value != routine_name) routine_name = value;}
		}
		#endregion
		#region DATA_TYPE
		string data_type = string.Empty;
		public string DataType
		{
			get{return data_type;}
			//set{if(value != data_type) data_type = value;}
		}
		#endregion
		#region ROUTINE_DEFINITION
		string routine_defiition = string.Empty;
		public string RoutineDefinition
		{
			get{return routine_defiition;}
			//set{if(value != routine_defiition) routine_defiition = value;}
		}
		#endregion
		#region IS_DETERMINISTIC
		string is_detreministic = string.Empty;
		public string IsDeterministic
		{
			get{return is_detreministic;}
			//set{if(value != is_detreministic) is_detreministic = value;}
		}
		#endregion
		#region IS_IMPLICITLY_INVOCABLE
		string is_implicitly_invocable = string.Empty;
		public string IsImplicitlyInvocable
		{
			get{return is_implicitly_invocable;}
			//set{if(value != is_implicitly_invocable) is_implicitly_invocable = value;}
		}
		#endregion
		#region CREATED
		DateTime created = DateTime.MinValue;
		public DateTime Created
		{
			get{return created;}
			//set{if(value != created) created = value;}
		}
		#endregion
		#region LAST_ALTERED
		DateTime last_altered = DateTime.MinValue;
		public DateTime LastAltered
		{
			get{return last_altered;}
			//set{if(value != last_altered) last_altered = value;}
		}
		#endregion
		#region Parameters
		SqlDbRoutineParameterCollection param = null;
		public SqlDbRoutineParameterCollection Parameters
		{
			get
			{
				if(null == param)
					param = SqlSchema.FindRoutineParameters(__conn, this.SpecificName);

				return param;
			}
		}
		#endregion
		#region Columns
		SqlDbRoutineFieldCollection col = null;
		public SqlDbRoutineFieldCollection Columns
		{
			get
			{
				if(null == col)
					col = SqlSchema.FindRoutineFields(__conn, this.SpecificName);

				return col;
			}
		}
		#endregion
		#region +BUILD DataReader
		internal static SqlDbRoutine Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbRoutine p = new SqlDbRoutine();

			p.__conn = connection;

			p.specific_catalog				= (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			p.specific_schema				= (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			p.specific_name					= (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			p.routine_catalog				= (r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			p.routine_schema				= (r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			p.routine_name					= (r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			p.routine_type					= (r.IsDBNull(6)) ? string.Empty : r.GetString(6);
			p.data_type						= (r.IsDBNull(7)) ? string.Empty : r.GetString(7);
			p.charachter_maximum_length		= (r.IsDBNull(8)) ? int.MinValue : r.GetInt32(8);
			p.charachter_octet_length		= (r.IsDBNull(9)) ? int.MinValue : r.GetInt32(9);
			p.numeric_precision				= (r.IsDBNull(10))? short.MaxValue : r.GetInt16(10);
			p.numeric_precision_radix		= (r.IsDBNull(11))? short.MaxValue : r.GetInt16(11);
			p.numeric_scale					= (r.IsDBNull(12))? short.MaxValue : r.GetInt16(12);
			p.routine_body					= (r.IsDBNull(13)) ? string.Empty : r.GetString(13);
			p.is_detreministic				= (r.IsDBNull(14)) ? string.Empty : r.GetString(14);
			p.is_implicitly_invocable		= (r.IsDBNull(15)) ? string.Empty : r.GetString(15);
			p.routine_type					= (r.IsDBNull(16)) ? string.Empty : r.GetString(16);
			p.created						= (r.IsDBNull(17)) ? DateTime.MinValue : r.GetDateTime(17);
			p.last_altered					= (r.IsDBNull(18)) ? DateTime.MinValue : r.GetDateTime(18);

			return p;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbRoutineCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbRoutineCollection rc = new SqlDbRoutineCollection();

			rc.Add(Build(r, connection));
			while(r.Read())
				rc.Add(Build(r, connection));

			return rc;
		}
		#endregion
		#region +Attach Parameters
		internal static SqlDbRoutine Attach(SqlDbRoutine routine, SqlDbRoutineParameterCollection param)
		{
			routine.param = param;
			return routine;
		}
		#endregion
		#region +Attach Columns
		internal static SqlDbRoutine Attach(SqlDbRoutine routine, SqlDbRoutineFieldCollection fields)
		{
			routine.col = fields;
			return routine;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}