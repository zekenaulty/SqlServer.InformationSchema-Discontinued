namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class RoutineFactory
	{
		public static List<SqlServer.InformationSchema.Routine> FindAll(SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.ROUTINES", false);
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
		public static List<SqlServer.InformationSchema.Routine> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.Routine> result = null;
			int SpecificCatalogOrdinal = reader.GetOrdinal("SPECIFIC_CATALOG");
			int SpecificSchemaOrdinal = reader.GetOrdinal("SPECIFIC_SCHEMA");
			int SpecificNameOrdinal = reader.GetOrdinal("SPECIFIC_NAME");
			int RoutineCatalogOrdinal = reader.GetOrdinal("ROUTINE_CATALOG");
			int RoutineSchemaOrdinal = reader.GetOrdinal("ROUTINE_SCHEMA");
			int RoutineNameOrdinal = reader.GetOrdinal("ROUTINE_NAME");
			int RoutineTypeOrdinal = reader.GetOrdinal("ROUTINE_TYPE");
			int ModuleCatalogOrdinal = reader.GetOrdinal("MODULE_CATALOG");
			int ModuleSchemaOrdinal = reader.GetOrdinal("MODULE_SCHEMA");
			int ModuleNameOrdinal = reader.GetOrdinal("MODULE_NAME");
			int UdtCatalogOrdinal = reader.GetOrdinal("UDT_CATALOG");
			int UdtSchemaOrdinal = reader.GetOrdinal("UDT_SCHEMA");
			int UdtNameOrdinal = reader.GetOrdinal("UDT_NAME");
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
			int TypeUdtCatalogOrdinal = reader.GetOrdinal("TYPE_UDT_CATALOG");
			int TypeUdtSchemaOrdinal = reader.GetOrdinal("TYPE_UDT_SCHEMA");
			int TypeUdtNameOrdinal = reader.GetOrdinal("TYPE_UDT_NAME");
			int ScopeCatalogOrdinal = reader.GetOrdinal("SCOPE_CATALOG");
			int ScopeSchemaOrdinal = reader.GetOrdinal("SCOPE_SCHEMA");
			int ScopeNameOrdinal = reader.GetOrdinal("SCOPE_NAME");
			int MaximumCardinalityOrdinal = reader.GetOrdinal("MAXIMUM_CARDINALITY");
			int DtdIdentifierOrdinal = reader.GetOrdinal("DTD_IDENTIFIER");
			int RoutineBodyOrdinal = reader.GetOrdinal("ROUTINE_BODY");
			int RoutineDefinitionOrdinal = reader.GetOrdinal("ROUTINE_DEFINITION");
			int ExternalNameOrdinal = reader.GetOrdinal("EXTERNAL_NAME");
			int ExternalLanguageOrdinal = reader.GetOrdinal("EXTERNAL_LANGUAGE");
			int ParameterStyleOrdinal = reader.GetOrdinal("PARAMETER_STYLE");
			int IsDeterministicOrdinal = reader.GetOrdinal("IS_DETERMINISTIC");
			int SqlDataAccessOrdinal = reader.GetOrdinal("SQL_DATA_ACCESS");
			int IsNullCallOrdinal = reader.GetOrdinal("IS_NULL_CALL");
			int SqlPathOrdinal = reader.GetOrdinal("SQL_PATH");
			int SchemaLevelRoutineOrdinal = reader.GetOrdinal("SCHEMA_LEVEL_ROUTINE");
			int MaxDynamicResultSetsOrdinal = reader.GetOrdinal("MAX_DYNAMIC_RESULT_SETS");
			int IsUserDefinedCastOrdinal = reader.GetOrdinal("IS_USER_DEFINED_CAST");
			int IsImplicitlyInvocableOrdinal = reader.GetOrdinal("IS_IMPLICITLY_INVOCABLE");
			int CreatedOrdinal = reader.GetOrdinal("CREATED");
			int LastAlteredOrdinal = reader.GetOrdinal("LAST_ALTERED");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.Routine>();
				}
				SqlServer.InformationSchema.Routine tmp = new SqlServer.InformationSchema.Routine();
				if (reader.IsDBNull(SpecificCatalogOrdinal))
				{
					tmp.SpecificCatalog = string.Empty;
				}
				else
				{
					tmp.SpecificCatalog = reader.GetString(SpecificCatalogOrdinal);
				}
				if (reader.IsDBNull(SpecificSchemaOrdinal))
				{
					tmp.SpecificSchema = string.Empty;
				}
				else
				{
					tmp.SpecificSchema = reader.GetString(SpecificSchemaOrdinal);
				}
				if (reader.IsDBNull(SpecificNameOrdinal))
				{
					tmp.SpecificName = string.Empty;
				}
				else
				{
					tmp.SpecificName = reader.GetString(SpecificNameOrdinal);
				}
				if (reader.IsDBNull(RoutineCatalogOrdinal))
				{
					tmp.RoutineCatalog = string.Empty;
				}
				else
				{
					tmp.RoutineCatalog = reader.GetString(RoutineCatalogOrdinal);
				}
				if (reader.IsDBNull(RoutineSchemaOrdinal))
				{
					tmp.RoutineSchema = string.Empty;
				}
				else
				{
					tmp.RoutineSchema = reader.GetString(RoutineSchemaOrdinal);
				}
				if (reader.IsDBNull(RoutineNameOrdinal))
				{
					tmp.RoutineName = string.Empty;
				}
				else
				{
					tmp.RoutineName = reader.GetString(RoutineNameOrdinal);
				}
				if (reader.IsDBNull(RoutineTypeOrdinal))
				{
					tmp.RoutineType = string.Empty;
				}
				else
				{
					tmp.RoutineType = reader.GetString(RoutineTypeOrdinal);
				}
				if (reader.IsDBNull(ModuleCatalogOrdinal))
				{
					tmp.ModuleCatalog = string.Empty;
				}
				else
				{
					tmp.ModuleCatalog = reader.GetString(ModuleCatalogOrdinal);
				}
				if (reader.IsDBNull(ModuleSchemaOrdinal))
				{
					tmp.ModuleSchema = string.Empty;
				}
				else
				{
					tmp.ModuleSchema = reader.GetString(ModuleSchemaOrdinal);
				}
				if (reader.IsDBNull(ModuleNameOrdinal))
				{
					tmp.ModuleName = string.Empty;
				}
				else
				{
					tmp.ModuleName = reader.GetString(ModuleNameOrdinal);
				}
				if (reader.IsDBNull(UdtCatalogOrdinal))
				{
					tmp.UdtCatalog = string.Empty;
				}
				else
				{
					tmp.UdtCatalog = reader.GetString(UdtCatalogOrdinal);
				}
				if (reader.IsDBNull(UdtSchemaOrdinal))
				{
					tmp.UdtSchema = string.Empty;
				}
				else
				{
					tmp.UdtSchema = reader.GetString(UdtSchemaOrdinal);
				}
				if (reader.IsDBNull(UdtNameOrdinal))
				{
					tmp.UdtName = string.Empty;
				}
				else
				{
					tmp.UdtName = reader.GetString(UdtNameOrdinal);
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
					tmp.IntervalType = string.Empty;
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
				if (reader.IsDBNull(TypeUdtCatalogOrdinal))
				{
					tmp.TypeUdtCatalog = string.Empty;
				}
				else
				{
					tmp.TypeUdtCatalog = reader.GetString(TypeUdtCatalogOrdinal);
				}
				if (reader.IsDBNull(TypeUdtSchemaOrdinal))
				{
					tmp.TypeUdtSchema = string.Empty;
				}
				else
				{
					tmp.TypeUdtSchema = reader.GetString(TypeUdtSchemaOrdinal);
				}
				if (reader.IsDBNull(TypeUdtNameOrdinal))
				{
					tmp.TypeUdtName = string.Empty;
				}
				else
				{
					tmp.TypeUdtName = reader.GetString(TypeUdtNameOrdinal);
				}
				if (reader.IsDBNull(ScopeCatalogOrdinal))
				{
					tmp.ScopeCatalog = string.Empty;
				}
				else
				{
					tmp.ScopeCatalog = reader.GetString(ScopeCatalogOrdinal);
				}
				if (reader.IsDBNull(ScopeSchemaOrdinal))
				{
					tmp.ScopeSchema = string.Empty;
				}
				else
				{
					tmp.ScopeSchema = reader.GetString(ScopeSchemaOrdinal);
				}
				if (reader.IsDBNull(ScopeNameOrdinal))
				{
					tmp.ScopeName = string.Empty;
				}
				else
				{
					tmp.ScopeName = reader.GetString(ScopeNameOrdinal);
				}
				if (reader.IsDBNull(MaximumCardinalityOrdinal))
				{
					tmp.MaximumCardinality = long.MinValue;
				}
				else
				{
					tmp.MaximumCardinality = reader.GetInt64(MaximumCardinalityOrdinal);
				}
				if (reader.IsDBNull(DtdIdentifierOrdinal))
				{
					tmp.DtdIdentifier = string.Empty;
				}
				else
				{
					tmp.DtdIdentifier = reader.GetString(DtdIdentifierOrdinal);
				}
				if (reader.IsDBNull(RoutineBodyOrdinal))
				{
					tmp.RoutineBody = string.Empty;
				}
				else
				{
					tmp.RoutineBody = reader.GetString(RoutineBodyOrdinal);
				}
				if (reader.IsDBNull(RoutineDefinitionOrdinal))
				{
					tmp.RoutineDefinition = string.Empty;
				}
				else
				{
					tmp.RoutineDefinition = reader.GetString(RoutineDefinitionOrdinal);
				}
				if (reader.IsDBNull(ExternalNameOrdinal))
				{
					tmp.ExternalName = string.Empty;
				}
				else
				{
					tmp.ExternalName = reader.GetString(ExternalNameOrdinal);
				}
				if (reader.IsDBNull(ExternalLanguageOrdinal))
				{
					tmp.ExternalLanguage = string.Empty;
				}
				else
				{
					tmp.ExternalLanguage = reader.GetString(ExternalLanguageOrdinal);
				}
				if (reader.IsDBNull(ParameterStyleOrdinal))
				{
					tmp.ParameterStyle = string.Empty;
				}
				else
				{
					tmp.ParameterStyle = reader.GetString(ParameterStyleOrdinal);
				}
				if (reader.IsDBNull(IsDeterministicOrdinal))
				{
					tmp.IsDeterministic = string.Empty;
				}
				else
				{
					tmp.IsDeterministic = reader.GetString(IsDeterministicOrdinal);
				}
				if (reader.IsDBNull(SqlDataAccessOrdinal))
				{
					tmp.SqlDataAccess = string.Empty;
				}
				else
				{
					tmp.SqlDataAccess = reader.GetString(SqlDataAccessOrdinal);
				}
				if (reader.IsDBNull(IsNullCallOrdinal))
				{
					tmp.IsNullCall = string.Empty;
				}
				else
				{
					tmp.IsNullCall = reader.GetString(IsNullCallOrdinal);
				}
				if (reader.IsDBNull(SqlPathOrdinal))
				{
					tmp.SqlPath = string.Empty;
				}
				else
				{
					tmp.SqlPath = reader.GetString(SqlPathOrdinal);
				}
				if (reader.IsDBNull(SchemaLevelRoutineOrdinal))
				{
					tmp.SchemaLevelRoutine = string.Empty;
				}
				else
				{
					tmp.SchemaLevelRoutine = reader.GetString(SchemaLevelRoutineOrdinal);
				}
				if (reader.IsDBNull(MaxDynamicResultSetsOrdinal))
				{
					tmp.MaxDynamicResultSets = short.MinValue;
				}
				else
				{
					tmp.MaxDynamicResultSets = reader.GetInt16(MaxDynamicResultSetsOrdinal);
				}
				if (reader.IsDBNull(IsUserDefinedCastOrdinal))
				{
					tmp.IsUserDefinedCast = string.Empty;
				}
				else
				{
					tmp.IsUserDefinedCast = reader.GetString(IsUserDefinedCastOrdinal);
				}
				if (reader.IsDBNull(IsImplicitlyInvocableOrdinal))
				{
					tmp.IsImplicitlyInvocable = string.Empty;
				}
				else
				{
					tmp.IsImplicitlyInvocable = reader.GetString(IsImplicitlyInvocableOrdinal);
				}
				if (reader.IsDBNull(CreatedOrdinal))
				{
					tmp.Created = DateTime.MinValue;
				}
				else
				{
					tmp.Created = reader.GetDateTime(CreatedOrdinal);
				}
				if (reader.IsDBNull(LastAlteredOrdinal))
				{
					tmp.LastAltered = DateTime.MinValue;
				}
				else
				{
					tmp.LastAltered = reader.GetDateTime(LastAlteredOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.Routine>();
			}
			return result;
		}
	}
}
