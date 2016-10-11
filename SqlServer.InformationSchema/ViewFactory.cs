namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ViewFactory
	{
		public static List<SqlServer.InformationSchema.View> FindAll(System.Data.SqlClient.SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.VIEWS", false);
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
		public static List<SqlServer.InformationSchema.View> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.View> result = null;
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			int ViewDefinitionOrdinal = reader.GetOrdinal("VIEW_DEFINITION");
			int CheckOptionOrdinal = reader.GetOrdinal("CHECK_OPTION");
			int IsUpdatableOrdinal = reader.GetOrdinal("IS_UPDATABLE");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.View>();
				}
				SqlServer.InformationSchema.View tmp = new SqlServer.InformationSchema.View();
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
				if (reader.IsDBNull(ViewDefinitionOrdinal))
				{
					tmp.ViewDefinition = String.Empty;
				}
				else
				{
					tmp.ViewDefinition = reader.GetString(ViewDefinitionOrdinal);
				}
				if (reader.IsDBNull(CheckOptionOrdinal))
				{
					tmp.CheckOption = String.Empty;
				}
				else
				{
					tmp.CheckOption = reader.GetString(CheckOptionOrdinal);
				}
				if (reader.IsDBNull(IsUpdatableOrdinal))
				{
					tmp.IsUpdatable = String.Empty;
				}
				else
				{
					tmp.IsUpdatable = reader.GetString(IsUpdatableOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.View>();
			}
			return result;
		}
	}
}
