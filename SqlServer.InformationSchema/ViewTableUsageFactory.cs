namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ViewTableUsageFactory
	{
		public static List<SqlServer.InformationSchema.ViewTableUsage> FindAll(System.Data.SqlClient.SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.VIEW_TABLE_USAGE", false);
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
		public static List<SqlServer.InformationSchema.ViewTableUsage> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.ViewTableUsage> result = null;
			int ViewCatalogOrdinal = reader.GetOrdinal("VIEW_CATALOG");
			int ViewSchemaOrdinal = reader.GetOrdinal("VIEW_SCHEMA");
			int ViewNameOrdinal = reader.GetOrdinal("VIEW_NAME");
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.ViewTableUsage>();
				}
				SqlServer.InformationSchema.ViewTableUsage tmp = new SqlServer.InformationSchema.ViewTableUsage();
				if (reader.IsDBNull(ViewCatalogOrdinal))
				{
					tmp.ViewCatalog = String.Empty;
				}
				else
				{
					tmp.ViewCatalog = reader.GetString(ViewCatalogOrdinal);
				}
				if (reader.IsDBNull(ViewSchemaOrdinal))
				{
					tmp.ViewSchema = String.Empty;
				}
				else
				{
					tmp.ViewSchema = reader.GetString(ViewSchemaOrdinal);
				}
				if (reader.IsDBNull(ViewNameOrdinal))
				{
					tmp.ViewName = String.Empty;
				}
				else
				{
					tmp.ViewName = reader.GetString(ViewNameOrdinal);
				}
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
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.ViewTableUsage>();
			}
			return result;
		}
	}
}
