namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
    /// <summary>
    /// Factory used to build and populate SqlServer.InformationSchema.CheckContraint(s)
    /// </summary>
	public class CheckConstraintFactory
	{

        /// <summary>
        /// Read all INFORMATION_SCHEMA.CHECK_CONSTRAINTS from the in passed SqlServerConnection.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>List<CheckConstraint></returns>
		public static List<CheckConstraint> FindAll(SqlConnection connection)
		{
			if ((connection == null))
				throw new Exception("Connection can not be null/Nothing.");

			if ((connection.State != ConnectionState.Open))
				connection.Open();

			SqlDataReader reader = null;

			try
			{
				reader = SqlDb.ExecuteReader(
                    connection, 
                    CommandType.Text, 
                    "SELECT * FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS");

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

        /// <summary>
        /// Read INFORMATION_SCHEMA.CHECK_CONSTRAINTS from the in passed SqlServerConnection for a specific schema.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>List<CheckConstraint></returns>
		public static List<CheckConstraint> FindBySchema(SqlConnection connection, string schema)
        {
            if ((connection == null))
                throw new Exception("Connection can not be null/Nothing.");

            if ((connection.State != ConnectionState.Open))
                connection.Open();

            SqlDataReader reader = null;

            try
            {
                reader = SqlDb.ExecuteReader(
                    connection, 
                    CommandType.Text, 
                    "SELECT * FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = @schema", 
                    new SqlParameter("@schema", schema));

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

        /// <summary>
        /// Read INFORMATION_SCHEMA.CHECK_CONSTRAINTS from the in passed SqlServerConnection for a specific schema and name.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>List<CheckConstraint></returns>
		public static List<CheckConstraint> FindByName(SqlConnection connection, string schema, string name)
        {
            if ((connection == null))
                throw new Exception("Connection can not be null/Nothing.");

            if ((connection.State != ConnectionState.Open))
                connection.Open();

            SqlDataReader reader = null;

            try
            {
                reader = SqlDb.ExecuteReader(
                    connection, 
                    CommandType.Text, 
                    "SELECT * FROM INFORMATION_SCHEMA.CHECK_CONSTRAINTS WHERE CONSTRAINT_SCHEMA = @schema AND CONSTRAINT_NAME = @constraintName", 
                    new SqlParameter("@schema", schema), 
                    new SqlParameter("@constraintName", name));

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

        /// <summary>
        /// Read records from an in passed SqlDataReader into a list of SqlServer.InformationSchema.CheckConstraint(s)
        /// </summary>
        /// <param name="reader">SqlDataReader</param>
        /// <returns>List<CheckConstraint></returns>
		public static List<CheckConstraint> ReadRecords(SqlDataReader reader)
		{
			if ((reader == null))
				throw new Exception("Reader can not be null/Nothing.");

			if ((reader.HasRows == false))
				throw new Exception("Reader has no rows.");

			List<CheckConstraint> result = null;

			int ConstraintCatalogOrdinal = reader.GetOrdinal("CONSTRAINT_CATALOG");
			int ConstraintSchemaOrdinal = reader.GetOrdinal("CONSTRAINT_SCHEMA");
			int ConstraintNameOrdinal = reader.GetOrdinal("CONSTRAINT_NAME");
			int CheckClauseOrdinal = reader.GetOrdinal("CHECK_CLAUSE");

			while (reader.Read())
			{
				if ((result == null))
					result = new List<CheckConstraint>();

				CheckConstraint tmp = new CheckConstraint();

				if (reader.IsDBNull(ConstraintCatalogOrdinal))
					tmp.ConstraintCatalog = string.Empty;
				else
					tmp.ConstraintCatalog = reader.GetString(ConstraintCatalogOrdinal);

				if (reader.IsDBNull(ConstraintSchemaOrdinal))
					tmp.ConstraintSchema = string.Empty;
				else
					tmp.ConstraintSchema = reader.GetString(ConstraintSchemaOrdinal);

				if (reader.IsDBNull(ConstraintNameOrdinal))
					tmp.ConstraintName = string.Empty;
				else
					tmp.ConstraintName = reader.GetString(ConstraintNameOrdinal);

				if (reader.IsDBNull(CheckClauseOrdinal))
					tmp.CheckClause = string.Empty;
				else
					tmp.CheckClause = reader.GetString(CheckClauseOrdinal);

				result.Add(tmp);
			}

			if (result == null)
				return new List<SqlServer.InformationSchema.CheckConstraint>();

			return result;
		}

	}
}
