namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ColumnDomainUsageFactory
	{
		public static List<SqlServer.InformationSchema.ColumnDomainUsage> FindAll(SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.COLUMN_DOMAIN_USAGE", false);
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
		public static List<SqlServer.InformationSchema.ColumnDomainUsage> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.ColumnDomainUsage> result = null;
			int DomainCatalogOrdinal = reader.GetOrdinal("DOMAIN_CATALOG");
			int DomainSchemaOrdinal = reader.GetOrdinal("DOMAIN_SCHEMA");
			int DomainNameOrdinal = reader.GetOrdinal("DOMAIN_NAME");
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			int ColumnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.ColumnDomainUsage>();
				}
				SqlServer.InformationSchema.ColumnDomainUsage tmp = new SqlServer.InformationSchema.ColumnDomainUsage();
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
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.ColumnDomainUsage>();
			}
			return result;
		}
	}
}
