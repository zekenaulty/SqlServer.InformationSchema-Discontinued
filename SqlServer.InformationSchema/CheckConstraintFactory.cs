namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class CheckConstraintFactory
	{
		public static List<SqlServer.InformationSchema.CheckConstraint> FindAll(System.Data.SqlClient.SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS", false);
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
		public static List<SqlServer.InformationSchema.CheckConstraint> ReadRecords(System.Data.SqlClient.SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new System.Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new System.Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.CheckConstraint> result = null;
			int ConstraintCatalogOrdinal = reader.GetOrdinal("CONSTRAINT_CATALOG");
			int ConstraintSchemaOrdinal = reader.GetOrdinal("CONSTRAINT_SCHEMA");
			int ConstraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
			int CheckClauseOrdinal = reader.GetOrdinal("CHECK_CLAUSE");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.CheckConstraint>();
				}
				SqlServer.InformationSchema.CheckConstraint tmp = new SqlServer.InformationSchema.CheckConstraint();
				if (reader.IsDBNull(ConstraintCatalogOrdinal))
				{
					tmp.ConstraintCatalog = String.Empty;
				}
				else
				{
					tmp.ConstraintCatalog = reader.GetString(ConstraintCatalogOrdinal);
				}
				if (reader.IsDBNull(ConstraintSchemaOrdinal))
				{
					tmp.ConstraintSchema = String.Empty;
				}
				else
				{
					tmp.ConstraintSchema = reader.GetString(ConstraintSchemaOrdinal);
				}
				if (reader.IsDBNull(ConstraintNameOrdinal))
				{
					tmp.ConstraintName = String.Empty;
				}
				else
				{
					tmp.ConstraintName = reader.GetString(ConstraintNameOrdinal);
				}
				if (reader.IsDBNull(CheckClauseOrdinal))
				{
					tmp.CheckClause = String.Empty;
				}
				else
				{
					tmp.CheckClause = reader.GetString(CheckClauseOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.CheckConstraint>();
			}
			return result;
		}
	}
}
