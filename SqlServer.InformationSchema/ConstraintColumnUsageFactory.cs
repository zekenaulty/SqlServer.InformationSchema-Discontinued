namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ConstraintColumnUsageFactory
	{
		public static List<SqlServer.InformationSchema.ConstraintColumnUsage> FindAll(SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE", false);
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
		public static List<SqlServer.InformationSchema.ConstraintColumnUsage> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.ConstraintColumnUsage> result = null;
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			int ColumnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
			int ConstraintCatalogOrdinal = reader.GetOrdinal("CONSTRAINT_CATALOG");
			int ConstraintSchemaOrdinal = reader.GetOrdinal("CONSTRAINT_SCHEMA");
			int ConstraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.ConstraintColumnUsage>();
				}
				SqlServer.InformationSchema.ConstraintColumnUsage tmp = new SqlServer.InformationSchema.ConstraintColumnUsage();
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
				if (reader.IsDBNull(ConstraintCatalogOrdinal))
				{
					tmp.ConstraintCatalog = string.Empty;
				}
				else
				{
					tmp.ConstraintCatalog = reader.GetString(ConstraintCatalogOrdinal);
				}
				if (reader.IsDBNull(ConstraintSchemaOrdinal))
				{
					tmp.ConstraintSchema = string.Empty;
				}
				else
				{
					tmp.ConstraintSchema = reader.GetString(ConstraintSchemaOrdinal);
				}
				if (reader.IsDBNull(ConstraintNameOrdinal))
				{
					tmp.ConstraintName = string.Empty;
				}
				else
				{
					tmp.ConstraintName = reader.GetString(ConstraintNameOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.ConstraintColumnUsage>();
			}
			return result;
		}
	}
}
