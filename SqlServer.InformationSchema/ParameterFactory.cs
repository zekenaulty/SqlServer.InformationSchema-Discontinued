namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ParameterFactory
	{
		public static List<SqlServer.InformationSchema.Parameter> FindAll(System.Data.SqlClient.SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.PARAMETERS", false);
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
		public static List<SqlServer.InformationSchema.Parameter> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.Parameter> result = null;
			int SpecificCatalogOrdinal = reader.GetOrdinal("SPECIFIC_CATALOG");
			int SpecificSchemaOrdinal = reader.GetOrdinal("SPECIFIC_SCHEMA");
			int SpecificNameOrdinal = reader.GetOrdinal("SPECIFIC_NAME");
			int OrdinalPositionOrdinal = reader.GetOrdinal("ORDINAL_POSITION");
			int ParameterModeOrdinal = reader.GetOrdinal("PARAMETER_MODE");
			int IsResultOrdinal = reader.GetOrdinal("IS_RESULT");
			int AsLocatorOrdinal = reader.GetOrdinal("AS_LOCATOR");
			int ParameterNameOrdinal = reader.GetOrdinal("PARAMETER_NAME");
			int DataTypeOrdinal = reader.GetOrdinal("DATA_TYPE");
			int CharacterMaximumLengthOrdinal = reader.GetOrdinal("CHARACTER_MAXIMUM_LENGTH");
			int CharacterOctetLengthOrdinal = reader.GetOrdinal("CHARACTER_OCTET_LENGTH");
			int CollationCatalogOrdinal = reader.GetOrdinal("COLLATION_CATALOG");
			int CollationSchemaOrdinal = reader.GetOrdinal("COLLATION_SCHEMA");
			int CollationNameOrdinal = reader.GetOrdinal("COLLATION_NAME");
			int CharacterSetCatalogOrdinal = reader.GetOrdinal("CHARACTER_SET_CATALOG");
			int CharacterSetSchemaOrdinal = reader.GetOrdinal("CHARACTER_SET_SCHEMA");
			int CharacterSetNameOrdinal = reader.GetOrdinal("CHARACTER_SET_NAME");
			int NumericPrecisionOrdinal = reader.GetOrdinal("NUMERIC_PRECISION");
			int NumericPrecisionRadixOrdinal = reader.GetOrdinal("NUMERIC_PRECISION_RADIX");
			int NumericScaleOrdinal = reader.GetOrdinal("NUMERIC_SCALE");
			int DatetimePrecisionOrdinal = reader.GetOrdinal("DATETIME_PRECISION");
			int IntervalTypeOrdinal = reader.GetOrdinal("INTERVAL_TYPE");
			int IntervalPrecisionOrdinal = reader.GetOrdinal("INTERVAL_PRECISION");
			int UserDefinedTypeCatalogOrdinal = reader.GetOrdinal("USER_DEFINED_TYPE_CATALOG");
			int UserDefinedTypeSchemaOrdinal = reader.GetOrdinal("USER_DEFINED_TYPE_SCHEMA");
			int UserDefinedTypeNameOrdinal = reader.GetOrdinal("USER_DEFINED_TYPE_NAME");
			int ScopeCatalogOrdinal = reader.GetOrdinal("SCOPE_CATALOG");
			int ScopeSchemaOrdinal = reader.GetOrdinal("SCOPE_SCHEMA");
			int ScopeNameOrdinal = reader.GetOrdinal("SCOPE_NAME");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.Parameter>();
				}
				SqlServer.InformationSchema.Parameter tmp = new SqlServer.InformationSchema.Parameter();
				if (reader.IsDBNull(SpecificCatalogOrdinal))
				{
					tmp.SpecificCatalog = String.Empty;
				}
				else
				{
					tmp.SpecificCatalog = reader.GetString(SpecificCatalogOrdinal);
				}
				if (reader.IsDBNull(SpecificSchemaOrdinal))
				{
					tmp.SpecificSchema = String.Empty;
				}
				else
				{
					tmp.SpecificSchema = reader.GetString(SpecificSchemaOrdinal);
				}
				if (reader.IsDBNull(SpecificNameOrdinal))
				{
					tmp.SpecificName = String.Empty;
				}
				else
				{
					tmp.SpecificName = reader.GetString(SpecificNameOrdinal);
				}
				if (reader.IsDBNull(OrdinalPositionOrdinal))
				{
					tmp.OrdinalPosition = int.MinValue;
				}
				else
				{
					tmp.OrdinalPosition = reader.GetInt32(OrdinalPositionOrdinal);
				}
				if (reader.IsDBNull(ParameterModeOrdinal))
				{
					tmp.ParameterMode = String.Empty;
				}
				else
				{
					tmp.ParameterMode = reader.GetString(ParameterModeOrdinal);
				}
				if (reader.IsDBNull(IsResultOrdinal))
				{
					tmp.IsResult = String.Empty;
				}
				else
				{
					tmp.IsResult = reader.GetString(IsResultOrdinal);
				}
				if (reader.IsDBNull(AsLocatorOrdinal))
				{
					tmp.AsLocator = String.Empty;
				}
				else
				{
					tmp.AsLocator = reader.GetString(AsLocatorOrdinal);
				}
				if (reader.IsDBNull(ParameterNameOrdinal))
				{
					tmp.ParameterName = String.Empty;
				}
				else
				{
					tmp.ParameterName = reader.GetString(ParameterNameOrdinal);
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
				if (reader.IsDBNull(IntervalTypeOrdinal))
				{
					tmp.IntervalType = String.Empty;
				}
				else
				{
					tmp.IntervalType = reader.GetString(IntervalTypeOrdinal);
				}
				if (reader.IsDBNull(IntervalPrecisionOrdinal))
				{
					tmp.IntervalPrecision = short.MinValue;
				}
				else
				{
					tmp.IntervalPrecision = reader.GetInt16(IntervalPrecisionOrdinal);
				}
				if (reader.IsDBNull(UserDefinedTypeCatalogOrdinal))
				{
					tmp.UserDefinedTypeCatalog = String.Empty;
				}
				else
				{
					tmp.UserDefinedTypeCatalog = reader.GetString(UserDefinedTypeCatalogOrdinal);
				}
				if (reader.IsDBNull(UserDefinedTypeSchemaOrdinal))
				{
					tmp.UserDefinedTypeSchema = String.Empty;
				}
				else
				{
					tmp.UserDefinedTypeSchema = reader.GetString(UserDefinedTypeSchemaOrdinal);
				}
				if (reader.IsDBNull(UserDefinedTypeNameOrdinal))
				{
					tmp.UserDefinedTypeName = String.Empty;
				}
				else
				{
					tmp.UserDefinedTypeName = reader.GetString(UserDefinedTypeNameOrdinal);
				}
				if (reader.IsDBNull(ScopeCatalogOrdinal))
				{
					tmp.ScopeCatalog = String.Empty;
				}
				else
				{
					tmp.ScopeCatalog = reader.GetString(ScopeCatalogOrdinal);
				}
				if (reader.IsDBNull(ScopeSchemaOrdinal))
				{
					tmp.ScopeSchema = String.Empty;
				}
				else
				{
					tmp.ScopeSchema = reader.GetString(ScopeSchemaOrdinal);
				}
				if (reader.IsDBNull(ScopeNameOrdinal))
				{
					tmp.ScopeName = String.Empty;
				}
				else
				{
					tmp.ScopeName = reader.GetString(ScopeNameOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.Parameter>();
			}
			return result;
		}
	}
}
