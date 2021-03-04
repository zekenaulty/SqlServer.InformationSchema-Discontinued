using System;
using System.Data;

using SqlServer.Companion.Base;
namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	public abstract class SqlDbTableFieldBase : SqlDbFieldBase
	{
		#region ORDINAL_POSITION
		protected int ordinal_position = -1;
		public int OridinalPosition
		{
			get{return ordinal_position;}
			//set{if(value != oridnal_position) value = oridinal_position;}
		}
		#endregion
		#region IS_NULLABLE
		protected string is_nullable = string.Empty;
		public string IsNullable
		{
			get{return is_nullable;}
			//set{if(value != is_nullable) is_nullable = value;}
		}
		#endregion
		#region DATA_TYPE
		protected string data_type = string.Empty;
		public string DataType
		{
			get{return data_type;}
			//set{if(value != data_type;}
		}
		#endregion
		#region CHARACTER_MAXIMUM_LENGTH
		protected int charachter_maximum_length = -1;
		public int CharacterMaximumLength
		{
			get{return charachter_maximum_length;}
			//set{if(value != charachter_maximum_length) charachter_maximum_length = value;}
		}
		#endregion
		#region CHARACTER_OCTET_LENGTH
		protected int charachter_octet_length = -1;
		public int CharacterOctetLength
		{
			get{return charachter_octet_length;}
			//set{if(value != charachter_octet_length) charachter_octet_length = value;}
		}
		#endregion
		#region NUMERIC_PRECISION
		protected int numeric_precision = -1;
		public int NumericPrecision
		{
			get{return numeric_precision;}
			//set{if(value != numeric_precision) numeric_precision = value;}
		}
		#endregion
		#region NUMERIC_PRECISION_RADIX
		protected int numeric_precision_radix = -1;
		public int NumericPrecisionRadix
		{
			get{return numeric_precision_radix;}
			//set{if(value != numeric_precision_radix) numeric_precision_radix = value;}
		}
		#endregion
		#region NUMERIC_SCALE
		protected int numeric_scale = -1;
		public int NumericScale
		{
			get{return numeric_scale;}
			//set{if(value != numeric_scale) numeric_scale = value;}
		}
		#endregion
		#region COLUMN_DEFAULT
		protected string column_default = string.Empty;
		public string ColumnDefault
		{
			get{return column_default;}
			//set{if(value != column_default) column_default = value;}
		}
		#endregion
		#region SqlDataType
		bool read_type = false;
		SqlDbType sql_type = SqlDbType.Variant;
		public SqlDbType SqlDataType
		{
			get
			{
				if(!read_type && null != data_type && data_type != string.Empty)
				{
					sql_type = SqlDbTableFieldBase.GetSqlDbType(data_type);
					read_type = true;
				}
				return sql_type;
			}
		}
		#endregion
		#region SqlType
		Type sqlType = null;
		public Type SqlType
		{
			get
			{
				if(null == sqlType)
				{
					#region Type Switch
					switch(SqlDataType)
					{
						case SqlDbType.BigInt:
							sqlType = typeof(System.Data.SqlTypes.SqlInt64);
							break;
						case SqlDbType.Binary:
							sqlType = typeof(System.Data.SqlTypes.SqlBinary);
							break;
						case SqlDbType.Bit:
							sqlType = typeof(System.Data.SqlTypes.SqlBoolean);
							break;
						case SqlDbType.Char:
							sqlType = typeof(System.Data.SqlTypes.SqlString);
							break;
						case SqlDbType.DateTime:
							sqlType = typeof(System.Data.SqlTypes.SqlDateTime);
							break;
						case SqlDbType.Decimal:
							sqlType = typeof(System.Data.SqlTypes.SqlDecimal);
							break;
						case SqlDbType.Float:
							sqlType = typeof(System.Data.SqlTypes.SqlDouble);
							break;
						case SqlDbType.Image:
							sqlType = typeof(System.Data.SqlTypes.SqlBinary);
							break;
						case SqlDbType.Int:
							sqlType = typeof(System.Data.SqlTypes.SqlInt32);
							break;
						case SqlDbType.Money:
							sqlType = typeof(System.Data.SqlTypes.SqlMoney);
							break;
						case SqlDbType.NChar:
							sqlType = typeof(System.Data.SqlTypes.SqlString);
							break;
						case SqlDbType.NText:
							sqlType = typeof(System.Data.SqlTypes.SqlString);
							break;
						case SqlDbType.NVarChar:
							sqlType = typeof(System.Data.SqlTypes.SqlString);
							break;
						case SqlDbType.Real:
							sqlType = typeof(System.Data.SqlTypes.SqlSingle);
							break;
						case SqlDbType.SmallDateTime:
							sqlType = typeof(System.Data.SqlTypes.SqlDateTime);
							break;
						case SqlDbType.SmallInt:
							sqlType = typeof(System.Data.SqlTypes.SqlInt16);
							break;
						case SqlDbType.SmallMoney:
							sqlType = typeof(System.Data.SqlTypes.SqlMoney);
							break;
						case SqlDbType.Text:
							sqlType = typeof(System.Data.SqlTypes.SqlString);
							break;
						case SqlDbType.Timestamp:
							sqlType = typeof(System.Data.SqlTypes.SqlBinary);
							break;
						case SqlDbType.TinyInt:
							sqlType = typeof(System.Data.SqlTypes.SqlByte);
							break;
						case SqlDbType.UniqueIdentifier:
							sqlType = typeof(System.Data.SqlTypes.SqlGuid);
							break;
						case SqlDbType.VarBinary:
							sqlType = typeof(System.Data.SqlTypes.SqlBinary);
							break;
						case SqlDbType.VarChar:
							sqlType = typeof(System.Data.SqlTypes.SqlString);
							break;
						default:
							sqlType = typeof(System.Object);
							break;
					}
					#endregion
				}
				return sqlType;
			}
		}
		#endregion
		#region ValueType
		Type valueType = null;
		public Type ValueType
		{
			get
			{
				if(null == valueType)
				{
					#region Type Switch
					switch(SqlDataType)
					{
						case SqlDbType.BigInt:
							valueType = typeof(System.Int64);
							break;
						case SqlDbType.Binary:
							valueType = new System.Data.SqlTypes.SqlBinary(new byte[0]).Value.GetType();
							break;
						case SqlDbType.Bit:
							valueType = typeof(System.Boolean);
							break;
						case SqlDbType.Char:
							valueType = typeof(System.String);
							break;
						case SqlDbType.DateTime:
							valueType = typeof(System.DateTime);
							break;
						case SqlDbType.Decimal:
							valueType = typeof(System.Decimal);
							break;
						case SqlDbType.Float:
							valueType = typeof(System.Double);
							break;
						case SqlDbType.Image:
							valueType = new System.Data.SqlTypes.SqlBinary(new byte[0]).Value.GetType();
							break;
						case SqlDbType.Int:
							valueType = typeof(System.Int32);
							break;
						case SqlDbType.Money:
							valueType = typeof(System.Decimal);
							break;
						case SqlDbType.NChar:
							valueType = typeof(System.String);
							break;
						case SqlDbType.NText:
							valueType = typeof(System.String);
							break;
						case SqlDbType.NVarChar:
							valueType = typeof(System.String);
							break;
						case SqlDbType.Real:
							valueType = typeof(System.Single);
							break;
						case SqlDbType.SmallDateTime:
							valueType = typeof(System.DateTime);
							break;
						case SqlDbType.SmallInt:
							valueType = typeof(System.Int16);
							break;
						case SqlDbType.SmallMoney:
							valueType = typeof(System.Decimal);
							break;
						case SqlDbType.Text:
							valueType = typeof(System.String);
							break;
						case SqlDbType.Timestamp:
							valueType = new System.Data.SqlTypes.SqlBinary(new byte[0]).Value.GetType();
							break;
						case SqlDbType.TinyInt:
							valueType = typeof(System.Byte);
							break;
						case SqlDbType.UniqueIdentifier:
							valueType = typeof(System.Guid);
							break;
						case SqlDbType.VarBinary:
							valueType = new System.Data.SqlTypes.SqlBinary(new byte[0]).Value.GetType();
							break;
						case SqlDbType.VarChar:
							valueType = typeof(System.String);
							break;
						default:
							valueType = typeof(System.Object);
							break;
					}
					#endregion
				}
				return valueType;
			}
		}
		#endregion
		#region GetSqlDbType
		internal static SqlDbType GetSqlDbType(string type)
		{
			switch(type.ToLower())
			{
				case "bigint":
					return SqlDbType.BigInt;
				case "binary":
					return SqlDbType.Binary;
				case "bit":
					return SqlDbType.Bit;
				case "char":
					return SqlDbType.Char;
				case "datetime":
					return SqlDbType.DateTime;
				case "decimal":
					return SqlDbType.Decimal;
				case "float":
					return SqlDbType.Float;
				case "image":
					return SqlDbType.Image;
				case "int":
					return SqlDbType.Int;
				case "money":
					return SqlDbType.Money;
				case "nchar":
					return SqlDbType.NChar;
				case "ntext":
					return SqlDbType.NText;
				case "nvarchar":
					return SqlDbType.NVarChar;
				case "numeric":
					return SqlDbType.Decimal;
				case "real":
					return SqlDbType.Real;
				case "smalldatetime":
					return SqlDbType.SmallDateTime;
				case "smallint":
					return SqlDbType.SmallInt;
				case "smallmoney":
					return SqlDbType.SmallMoney;
				case "text":
					return SqlDbType.Text;
				case "timestamp":
					return SqlDbType.Timestamp;
				case "tinyint":
					return SqlDbType.TinyInt;
				case "uniqueidentifier":
					return SqlDbType.UniqueIdentifier;
				case "varbinary":
					return SqlDbType.VarBinary;
				case "varchar":
					return SqlDbType.VarChar;
				case "variant":
					return SqlDbType.Variant;
				default:
					return SqlDbType.Variant;
			}
		}
		#endregion
	}
}
