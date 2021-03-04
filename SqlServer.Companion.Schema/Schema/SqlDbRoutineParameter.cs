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
	public sealed class SqlDbRoutineParameter : SqlDbRoutineBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbRoutineParameter(){}
		#endregion
		#region ORDINAL_POSITION
		short ordinal_position = -1;
		public short OridinalPosition
		{
			get{return ordinal_position;}
			//set{if(value != oridnal_position) value = oridinal_position;}
		}
		#endregion
		#region PARAMETER_MODE
		string parameter_mode = string.Empty;
		public string ParameterMode
		{
			get{return parameter_mode;}
			//set{if(value != parameter_mode) value = parameter_mode;}
		}
		#endregion
		#region IS_RESULT
		string is_result = string.Empty;
		public string IsResult
		{
			get{return is_result;}
			//set{if(value != is_result) value = is_result;}
		}
		#endregion
		#region AS_LOCATOR
		string as_locator = string.Empty;
		public string AsLocator
		{
			get{return as_locator;}
			//set{if(value != as_locator) value = as_locator;}
		}
		#endregion
		#region PARAMETER_NAME
		string parameter_name = string.Empty;
		public string ParameterName
		{
			get{return parameter_name;}
			//set{if(value != parameter_name) value = parameter_name;}
		}
		#endregion
		#region DATA_TYPE
		string data_type = string.Empty;
		public string DataType
		{
			get{return data_type;}
			//set{if(value != data_type;}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbRoutineParameter Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbRoutineParameter p = new SqlDbRoutineParameter();

			p.__conn = connection;

			p.specific_catalog				= (r.IsDBNull(0))	? string.Empty	: r.GetString(0);
			p.specific_schema				= (r.IsDBNull(1))	? string.Empty	: r.GetString(1);
			p.specific_name					= (r.IsDBNull(2))	? string.Empty	: r.GetString(2);
			p.ordinal_position				= (r.IsDBNull(3))	? short.MinValue	: r.GetInt16(3);
			p.parameter_mode				= (r.IsDBNull(4))	? string.Empty	: r.GetString(4);
			p.is_result						= (r.IsDBNull(5))	? string.Empty	: r.GetString(5);
			p.as_locator					= (r.IsDBNull(6))	? string.Empty	: r.GetString(6);
			p.parameter_name				= (r.IsDBNull(7))	? string.Empty	: r.GetString(7);
			p.data_type						= (r.IsDBNull(8))	? string.Empty	: r.GetString(8);
			p.charachter_maximum_length		= (r.IsDBNull(9))	? int.MinValue	: r.GetInt32(9);
			p.charachter_octet_length		= (r.IsDBNull(10))	? int.MinValue	: r.GetInt32(10);
			p.numeric_precision				= (r.IsDBNull(11))	? short.MaxValue	: r.GetInt16(11);
			p.numeric_precision_radix		= (r.IsDBNull(12))	? short.MaxValue	: r.GetInt16(12);
			p.numeric_scale					= (r.IsDBNull(13))	? short.MaxValue	: r.GetInt16(13);

			return p;
		}
		#endregion
		#region +BuildCollection
		internal static SqlDbRoutineParameterCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbRoutineParameterCollection rpc = new SqlDbRoutineParameterCollection();

			rpc.Add(Build(r, connection));
			while(r.Read())
				rpc.Add(Build(r, connection));

			return rpc;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
