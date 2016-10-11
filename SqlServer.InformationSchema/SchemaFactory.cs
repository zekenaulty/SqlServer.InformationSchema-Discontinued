namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class SchemaFactory
	{
		public static List<SqlServer.InformationSchema.Schema> FindAll(System.Data.SqlClient.SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.SCHEMATA", false);
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
		public static List<SqlServer.InformationSchema.Schema> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.Schema> result = null;
			int CatalogNameOrdinal = reader.GetOrdinal("CATALOG_NAME");
			int SchemaNameOrdinal = reader.GetOrdinal("SCHEMA_NAME");
			int SchemaOwnerOrdinal = reader.GetOrdinal("SCHEMA_OWNER");
			int DefaultCharacterSetCatalogOrdinal = reader.GetOrdinal("DEFAULT_CHARACTER_SET_CATALOG");
			int DefaultCharacterSetSchemaOrdinal = reader.GetOrdinal("DEFAULT_CHARACTER_SET_SCHEMA");
			int DefaultCharacterSetNameOrdinal = reader.GetOrdinal("DEFAULT_CHARACTER_SET_NAME");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.Schema>();
				}
				SqlServer.InformationSchema.Schema tmp = new SqlServer.InformationSchema.Schema();
				if (reader.IsDBNull(CatalogNameOrdinal))
				{
					tmp.CatalogName = String.Empty;
				}
				else
				{
					tmp.CatalogName = reader.GetString(CatalogNameOrdinal);
				}
				if (reader.IsDBNull(SchemaNameOrdinal))
				{
					tmp.SchemaName = String.Empty;
				}
				else
				{
					tmp.SchemaName = reader.GetString(SchemaNameOrdinal);
				}
				if (reader.IsDBNull(SchemaOwnerOrdinal))
				{
					tmp.SchemaOwner = String.Empty;
				}
				else
				{
					tmp.SchemaOwner = reader.GetString(SchemaOwnerOrdinal);
				}
				if (reader.IsDBNull(DefaultCharacterSetCatalogOrdinal))
				{
					tmp.DefaultCharacterSetCatalog = String.Empty;
				}
				else
				{
					tmp.DefaultCharacterSetCatalog = reader.GetString(DefaultCharacterSetCatalogOrdinal);
				}
				if (reader.IsDBNull(DefaultCharacterSetSchemaOrdinal))
				{
					tmp.DefaultCharacterSetSchema = String.Empty;
				}
				else
				{
					tmp.DefaultCharacterSetSchema = reader.GetString(DefaultCharacterSetSchemaOrdinal);
				}
				if (reader.IsDBNull(DefaultCharacterSetNameOrdinal))
				{
					tmp.DefaultCharacterSetName = String.Empty;
				}
				else
				{
					tmp.DefaultCharacterSetName = reader.GetString(DefaultCharacterSetNameOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.Schema>();
			}
			return result;
		}
	}
}
