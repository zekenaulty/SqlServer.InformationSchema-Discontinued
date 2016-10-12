namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ColumnPrivilegeFactory
	{
		public static List<SqlServer.InformationSchema.ColumnPrivilege> FindAll(SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.COLUMN_PRIVILEGES", false);
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
		public static List<SqlServer.InformationSchema.ColumnPrivilege> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.ColumnPrivilege> result = null;
			int GrantorOrdinal = reader.GetOrdinal("GRANTOR");
			int GranteeOrdinal = reader.GetOrdinal("GRANTEE");
			int TableCatalogOrdinal = reader.GetOrdinal("TABLE_CATALOG");
			int TableSchemaOrdinal = reader.GetOrdinal("TABLE_SCHEMA");
			int TableNameOrdinal = reader.GetOrdinal("TABLE_NAME");
			int ColumnNameOrdinal = reader.GetOrdinal("COLUMN_NAME");
			int PrivilegeTypeOrdinal = reader.GetOrdinal("PRIVILEGE_TYPE");
			int IsGrantableOrdinal = reader.GetOrdinal("IS_GRANTABLE");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.ColumnPrivilege>();
				}
				SqlServer.InformationSchema.ColumnPrivilege tmp = new SqlServer.InformationSchema.ColumnPrivilege();
				if (reader.IsDBNull(GrantorOrdinal))
				{
					tmp.Grantor = string.Empty;
				}
				else
				{
					tmp.Grantor = reader.GetString(GrantorOrdinal);
				}
				if (reader.IsDBNull(GranteeOrdinal))
				{
					tmp.Grantee = string.Empty;
				}
				else
				{
					tmp.Grantee = reader.GetString(GranteeOrdinal);
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
				if (reader.IsDBNull(PrivilegeTypeOrdinal))
				{
					tmp.PrivilegeType = string.Empty;
				}
				else
				{
					tmp.PrivilegeType = reader.GetString(PrivilegeTypeOrdinal);
				}
				if (reader.IsDBNull(IsGrantableOrdinal))
				{
					tmp.IsGrantable = string.Empty;
				}
				else
				{
					tmp.IsGrantable = reader.GetString(IsGrantableOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.ColumnPrivilege>();
			}
			return result;
		}
	}
}
