namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ColumnFactory
	{
		public static List<SqlServer.InformationSchema.Column> FindAll(System.Data.SqlClient.SqlConnection connection)
		{
			if ((connection == null))
			{
				throw new System.Exception("Connection can not be null/Nothing.");
			}
			if ((connection.State != ConnectionState.Open))
			{
				connection.Open();
			}
			System.Data.SqlClient.SqlDataReader reader = null;
			try
			{
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.COLUMNS", false);
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
		public static List<SqlServer.InformationSchema.Column> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.Column> result = null;
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
					result = new List<SqlServer.InformationSchema.Column>();
				}
				SqlServer.InformationSchema.Column tmp = new SqlServer.InformationSchema.Column();
				if (reader.IsDBNull(TableCatalogOrdinal))
				{
					tmp.TableCatalog = String.Empty;
				}
				else
				{
					tmp.TableCatalog = reader.GetString(TableCatalogOrdinal);
				}
				if (reader.IsDBNull(TableSchemaOrdinal))
				{
					tmp.TableSchema = String.Empty;
				}
				else
				{
					tmp.TableSchema = reader.GetString(TableSchemaOrdinal);
				}
				if (reader.IsDBNull(TableNameOrdinal))
				{
					tmp.TableName = String.Empty;
				}
				else
				{
					tmp.TableName = reader.GetString(TableNameOrdinal);
				}
				if (reader.IsDBNull(ColumnNameOrdinal))
				{
					tmp.ColumnName = String.Empty;
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
					tmp.OrdinalPosition = reader.GetInt(OrdinalPositionOrdinal);
				}
				if (reader.IsDBNull(ColumnDefaultOrdinal))
				{
					tmp.ColumnDefault = String.Empty;
				}
				else
				{
					tmp.ColumnDefault = reader.GetString(ColumnDefaultOrdinal);
				}
				if (reader.IsDBNull(IsNullableOrdinal))
				{
					tmp.IsNullable = String.Empty;
				}
				else
				{
					tmp.IsNullable = reader.GetString(IsNullableOrdinal);
				}
				if (reader.IsDBNull(DataTypeOrdinal))
				{
					tmp.DataType = String.Empty;
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
					tmp.CharacterMaximumLength = reader.GetInt(CharacterMaximumLengthOrdinal);
				}
				if (reader.IsDBNull(CharacterOctetLengthOrdinal))
				{
					tmp.CharacterOctetLength = int.MinValue;
				}
				else
				{
					tmp.CharacterOctetLength = reader.GetInt(CharacterOctetLengthOrdinal);
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
					tmp.NumericPrecisionRadix = reader.GetShort(NumericPrecisionRadixOrdinal);
				}
				if (reader.IsDBNull(NumericScaleOrdinal))
				{
					tmp.NumericScale = int.MinValue;
				}
				else
				{
					tmp.NumericScale = reader.GetInt(NumericScaleOrdinal);
				}
				if (reader.IsDBNull(DatetimePrecisionOrdinal))
				{
					tmp.DatetimePrecision = short.MinValue;
				}
				else
				{
					tmp.DatetimePrecision = reader.GetShort(DatetimePrecisionOrdinal);
				}
				if (reader.IsDBNull(CharacterSetCatalogOrdinal))
				{
					tmp.CharacterSetCatalog = String.Empty;
				}
				else
				{
					tmp.CharacterSetCatalog = reader.GetString(CharacterSetCatalogOrdinal);
				}
				if (reader.IsDBNull(CharacterSetSchemaOrdinal))
				{
					tmp.CharacterSetSchema = String.Empty;
				}
				else
				{
					tmp.CharacterSetSchema = reader.GetString(CharacterSetSchemaOrdinal);
				}
				if (reader.IsDBNull(CharacterSetNameOrdinal))
				{
					tmp.CharacterSetName = String.Empty;
				}
				else
				{
					tmp.CharacterSetName = reader.GetString(CharacterSetNameOrdinal);
				}
				if (reader.IsDBNull(CollationCatalogOrdinal))
				{
					tmp.CollationCatalog = String.Empty;
				}
				else
				{
					tmp.CollationCatalog = reader.GetString(CollationCatalogOrdinal);
				}
				if (reader.IsDBNull(CollationSchemaOrdinal))
				{
					tmp.CollationSchema = String.Empty;
				}
				else
				{
					tmp.CollationSchema = reader.GetString(CollationSchemaOrdinal);
				}
				if (reader.IsDBNull(CollationNameOrdinal))
				{
					tmp.CollationName = String.Empty;
				}
				else
				{
					tmp.CollationName = reader.GetString(CollationNameOrdinal);
				}
				if (reader.IsDBNull(DomainCatalogOrdinal))
				{
					tmp.DomainCatalog = String.Empty;
				}
				else
				{
					tmp.DomainCatalog = reader.GetString(DomainCatalogOrdinal);
				}
				if (reader.IsDBNull(DomainSchemaOrdinal))
				{
					tmp.DomainSchema = String.Empty;
				}
				else
				{
					tmp.DomainSchema = reader.GetString(DomainSchemaOrdinal);
				}
				if (reader.IsDBNull(DomainNameOrdinal))
				{
					tmp.DomainName = String.Empty;
				}
				else
				{
					tmp.DomainName = reader.GetString(DomainNameOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.Column>();
			}
			return result;
		}
	}
}
