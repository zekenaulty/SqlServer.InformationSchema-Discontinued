namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class TableFactory
	{
		public static List<SqlServer.InformationSchema.Table> FindAll(System.Data.SqlClient.SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.TABLES", false);
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
		public static List<SqlServer.InformationSchema.Table> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.Table> result = null;
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			int TableTypeOrdinal = reader.GetOrdinal("TABLE_TYPE");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.Table>();
				}
				SqlServer.InformationSchema.Table tmp = new SqlServer.InformationSchema.Table();
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
				if (reader.IsDBNull(TableTypeOrdinal))
				{
					tmp.TableType = String.Empty;
				}
				else
				{
					tmp.TableType = reader.GetString(TableTypeOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.Table>();
			}
			return result;
		}
	}
}
