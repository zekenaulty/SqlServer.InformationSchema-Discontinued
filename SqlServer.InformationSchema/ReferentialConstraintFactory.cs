namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ReferentialConstraintFactory
	{
		public static List<SqlServer.InformationSchema.ReferentialConstraint> FindAll(SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.REFERENTIAL_CONSTRAINTS", false);
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
		public static List<SqlServer.InformationSchema.ReferentialConstraint> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.ReferentialConstraint> result = null;
			int ConstraintCatalogOrdinal = reader.GetOrdinal("CONSTRAINT_CATALOG");
			int ConstraintSchemaOrdinal = reader.GetOrdinal("CONSTRAINT_SCHEMA");
			int ConstraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
			int UniqueConstraintCatalogOrdinal = reader.GetOrdinal("UNIQUE_CONSTRAINT_CATALOG");
			int UniqueConstraintSchemaOrdinal = reader.GetOrdinal("UNIQUE_CONSTRAINT_SCHEMA");
			int UniqueConstraintNameOrdinal = reader.GetOrdinal("UNIQUE_CONSTRAINT_NAME");
			int MatchOptionOrdinal = reader.GetOrdinal("MATCH_OPTION");
			int UpdateRuleOrdinal = reader.GetOrdinal("UPDATE_RULE");
			int DeleteRuleOrdinal = reader.GetOrdinal("DELETE_RULE");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.ReferentialConstraint>();
				}
				SqlServer.InformationSchema.ReferentialConstraint tmp = new SqlServer.InformationSchema.ReferentialConstraint();
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
				if (reader.IsDBNull(UniqueConstraintCatalogOrdinal))
				{
					tmp.UniqueConstraintCatalog = string.Empty;
				}
				else
				{
					tmp.UniqueConstraintCatalog = reader.GetString(UniqueConstraintCatalogOrdinal);
				}
				if (reader.IsDBNull(UniqueConstraintSchemaOrdinal))
				{
					tmp.UniqueConstraintSchema = string.Empty;
				}
				else
				{
					tmp.UniqueConstraintSchema = reader.GetString(UniqueConstraintSchemaOrdinal);
				}
				if (reader.IsDBNull(UniqueConstraintNameOrdinal))
				{
					tmp.UniqueConstraintName = string.Empty;
				}
				else
				{
					tmp.UniqueConstraintName = reader.GetString(UniqueConstraintNameOrdinal);
				}
				if (reader.IsDBNull(MatchOptionOrdinal))
				{
					tmp.MatchOption = string.Empty;
				}
				else
				{
					tmp.MatchOption = reader.GetString(MatchOptionOrdinal);
				}
				if (reader.IsDBNull(UpdateRuleOrdinal))
				{
					tmp.UpdateRule = string.Empty;
				}
				else
				{
					tmp.UpdateRule = reader.GetString(UpdateRuleOrdinal);
				}
				if (reader.IsDBNull(DeleteRuleOrdinal))
				{
					tmp.DeleteRule = string.Empty;
				}
				else
				{
					tmp.DeleteRule = reader.GetString(DeleteRuleOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.ReferentialConstraint>();
			}
			return result;
		}
	}
}
