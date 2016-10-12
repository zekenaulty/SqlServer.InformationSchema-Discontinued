namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class DomainConstraintFactory
	{
		public static List<SqlServer.InformationSchema.DomainConstraint> FindAll(SqlConnection connection)
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
				reader = SqlDb.ExecuteReader(connection, CommandType.Text, "SELECT * FROM INFORMATION_SCHEMA.DOMAIN_CONSTRAINTS", false);
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
		public static List<SqlServer.InformationSchema.DomainConstraint> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
			{
				throw new Exception("Reader can not be null/Nothing.");
			}
			if ((reader.HasRows == false))
			{
				throw new Exception("Reader has no rows.");
			}
			List<SqlServer.InformationSchema.DomainConstraint> result = null;
			int ConstraintCatalogOrdinal = reader.GetOrdinal("CONSTRAINT_CATALOG");
			int ConstraintSchemaOrdinal = reader.GetOrdinal("CONSTRAINT_SCHEMA");
			int ConstraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
			int DomainCatalogOrdinal = reader.GetOrdinal("DOMAIN_CATALOG");
			int DomainSchemaOrdinal = reader.GetOrdinal("DOMAIN_SCHEMA");
			int DomainNameOrdinal = reader.GetOrdinal("DOMAIN_NAME");
			int IsDeferrableOrdinal = reader.GetOrdinal("IS_DEFERRABLE");
			int InitiallyDeferredOrdinal = reader.GetOrdinal("INITIALLY_DEFERRED");
			for (
			; reader.Read(); 
			)
			{
				if ((result == null))
				{
					result = new List<SqlServer.InformationSchema.DomainConstraint>();
				}
				SqlServer.InformationSchema.DomainConstraint tmp = new SqlServer.InformationSchema.DomainConstraint();
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
				if (reader.IsDBNull(IsDeferrableOrdinal))
				{
					tmp.IsDeferrable = string.Empty;
				}
				else
				{
					tmp.IsDeferrable = reader.GetString(IsDeferrableOrdinal);
				}
				if (reader.IsDBNull(InitiallyDeferredOrdinal))
				{
					tmp.InitiallyDeferred = string.Empty;
				}
				else
				{
					tmp.InitiallyDeferred = reader.GetString(InitiallyDeferredOrdinal);
				}
				result.Add(tmp);
			}
			if ((result == null))
			{
				return new List<SqlServer.InformationSchema.DomainConstraint>();
			}
			return result;
		}
	}
}
