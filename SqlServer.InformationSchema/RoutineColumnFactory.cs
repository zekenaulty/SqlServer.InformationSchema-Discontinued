namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class RoutineColumnFactory
	{
		public static List<SqlServer.InformationSchema.RoutineColumn> FindAll(SqlConnection connection)
		{
			if ((connection == null))
			{
				throw new Exception("Connection can not be null/Nothing.");
			}
			if ((connection.State != ConnectionState.Open))
			{
				connection.Open();
			}
            SqlDataReader reader = null;
			try
			{
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.ROUTINE_COLUMNS", false);
				return ReadRecords(reader);
			}
			finally
			{
				if ((reader != null))
				{
					reader.Close();
					reader = null;
				}
			}
		}
		public static List<SqlServer.InformationSchema.RoutineColumn> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.RoutineColumn> result = null;
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			int ColumnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
			int OrdinalPositionOrdinal = reader.GetOrdinal("ORDINAL_POSITION");
			int ColumnDefaultOrdinal = reader.GetOrdinal("COLUMN_DEFAULT");
			int IsNullableOrdinal = reader.GetOrdinal("IS_NULLABLE");
			int DataTypeOrdinal = reader.GetOrdinal("DATA_TYPE");
			int CharacterMaximumLengthOrdinal = reader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
			int CharacterOctetLengthOrdinal = reader.GetOrdinal("CHARACTER_OCTET_LENGTH");
			int NumericPrecisionOrdinal = reader.GetOrdinal("NUMERIC_PRECISION");
			int NumericPrecisionRadixOrdinal = reader.GetOrdinal("NUMERIC_PRECISION_RADIX");
			int NumericScaleOrdinal = reader.GetOrdinal("NUMERIC_SCALE");
			int DatetimePrecisionOrdinal = reader.GetOrdinal("DATETIME_PRECISION");
			int CharacterSetCatalogOrdinal = reader.GetOrdinal("CHARACTER_SET_CATALOG");
			int CharacterSetSchemaOrdinal = reader.GetOrdinal("CHARACTER_SET_SCHEMA");
			int CharacterSetNameOrdinal = reader.GetOrdinal("CHARACTER_SET_NAME");
			int CollationCatalogOrdinal = reader.GetOrdinal("COLLATION_CATALOG");
			int CollationSchemaOrdinal = reader.GetOrdinal("COLLATION_SCHEMA");
			int CollationNameOrdinal = reader.GetOrdinal("COLLATION_NAME");
			int DomainCatalogOrdinal = reader.GetOrdinal("DOMAIN_CATALOG");
			int DomainSchemaOrdinal = reader.GetOrdinal("DOMAIN_SCHEMA");
			int DomainNameOrdinal = reader.GetOrdinal("DOMAIN_NAME");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.RoutineColumn>();
				}
				SqlServer.InformationSchema.RoutineColumn tmp = new SqlServer.InformationSchema.RoutineColumn();
				if (reader.IsDBNull(TableCatalogOrdinal))
				{
					tmp.TableCatalog = string.Empty;
				}
				else
				{
					tmp.TableCatalog = reader.GetString(TableCatalogOrdinal);
				}
				if (reader.IsDBNull(TableSchemaOrdinal))
				{
					tmp.TableSchema = string.Empty;
				}
				else
				{
					tmp.TableSchema = reader.GetString(TableSchemaOrdinal);
				}
				if (reader.IsDBNull(TableNameOrdinal))
				{
					tmp.TableName = string.Empty;
				}
				else
				{
					tmp.TableName = reader.GetString(TableNameOrdinal);
				}
				if (reader.IsDBNull(ColumnNameOrdinal))
				{
					tmp.ColumnName = string.Empty;
				}
				else
				{
					tmp.ColumnName = reader.GetString(ColumnNameOrdinal);
				}
				if (reader.IsDBNull(OrdinalPositionOrdinal))
				{
					tmp.OrdinalPosition = int.MinValue;
				}
				else
				{
					tmp.OrdinalPosition = reader.GetInt32(OrdinalPositionOrdinal);
				}
				if (reader.IsDBNull(ColumnDefaultOrdinal))
				{
					tmp.ColumnDefault = string.Empty;
				}
				else
				{
					tmp.ColumnDefault = reader.GetString(ColumnDefaultOrdinal);
				}
				if (reader.IsDBNull(IsNullableOrdinal))
				{
					tmp.IsNullable = string.Empty;
				}
				else
				{
					tmp.IsNullable = reader.GetString(IsNullableOrdinal);
				}
				if (reader.IsDBNull(DataTypeOrdinal))
				{
					tmp.DataType = string.Empty;
				}
				else
				{
					tmp.DataType = reader.GetString(DataTypeOrdinal);
				}
				if (reader.IsDBNull(CharacterMaximumLengthOrdinal))
				{
					tmp.CharacterMaximumLength = int.MinValue;
				}
				else
				{
					tmp.CharacterMaximumLength = reader.GetInt32(CharacterMaximumLengthOrdinal);
				}
				if (reader.IsDBNull(CharacterOctetLengthOrdinal))
				{
					tmp.CharacterOctetLength = int.MinValue;
				}
				else
				{
					tmp.CharacterOctetLength = reader.GetInt32(CharacterOctetLengthOrdinal);
				}
				if (reader.IsDBNull(NumericPrecisionOrdinal))
				{
					tmp.NumericPrecision = byte.MinValue;
				}
				else
				{
					tmp.NumericPrecision = reader.GetByte(NumericPrecisionOrdinal);
				}
				if (reader.IsDBNull(NumericPrecisionRadixOrdinal))
				{
					tmp.NumericPrecisionRadix = short.MinValue;
				}
				else
				{
					tmp.NumericPrecisionRadix = reader.GetInt16(NumericPrecisionRadixOrdinal);
				}
				if (reader.IsDBNull(NumericScaleOrdinal))
				{
					tmp.NumericScale = int.MinValue;
				}
				else
				{
					tmp.NumericScale = reader.GetInt32(NumericScaleOrdinal);
				}
				if (reader.IsDBNull(DatetimePrecisionOrdinal))
				{
					tmp.DatetimePrecision = short.MinValue;
				}
				else
				{
					tmp.DatetimePrecision = reader.GetInt16(DatetimePrecisionOrdinal);
				}
				if (reader.IsDBNull(CharacterSetCatalogOrdinal))
				{
					tmp.CharacterSetCatalog = string.Empty;
				}
				else
				{
					tmp.CharacterSetCatalog = reader.GetString(CharacterSetCatalogOrdinal);
				}
				if (reader.IsDBNull(CharacterSetSchemaOrdinal))
				{
					tmp.CharacterSetSchema = string.Empty;
				}
				else
				{
					tmp.CharacterSetSchema = reader.GetString(CharacterSetSchemaOrdinal);
				}
				if (reader.IsDBNull(CharacterSetNameOrdinal))
				{
					tmp.CharacterSetName = string.Empty;
				}
				else
				{
					tmp.CharacterSetName = reader.GetString(CharacterSetNameOrdinal);
				}
				if (reader.IsDBNull(CollationCatalogOrdinal))
				{
					tmp.CollationCatalog = string.Empty;
				}
				else
				{
					tmp.CollationCatalog = reader.GetString(CollationCatalogOrdinal);
				}
				if (reader.IsDBNull(CollationSchemaOrdinal))
				{
					tmp.CollationSchema = string.Empty;
				}
				else
				{
					tmp.CollationSchema = reader.GetString(CollationSchemaOrdinal);
				}
				if (reader.IsDBNull(CollationNameOrdinal))
				{
					tmp.CollationName = string.Empty;
				}
				else
				{
					tmp.CollationName = reader.GetString(CollationNameOrdinal);
				}
				if (reader.IsDBNull(DomainCatalogOrdinal))
				{
					tmp.DomainCatalog = string.Empty;
				}
				else
				{
					tmp.DomainCatalog = reader.GetString(DomainCatalogOrdinal);
				}
				if (reader.IsDBNull(DomainSchemaOrdinal))
				{
					tmp.DomainSchema = string.Empty;
				}
				else
				{
					tmp.DomainSchema = reader.GetString(DomainSchemaOrdinal);
				}
				if (reader.IsDBNull(DomainNameOrdinal))
				{
					tmp.DomainName = string.Empty;
				}
				else
				{
					tmp.DomainName = reader.GetString(DomainNameOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.RoutineColumn>();
			}
			return result;
		}
	}
}
