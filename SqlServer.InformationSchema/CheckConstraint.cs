namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;

    /// <summary>
    /// A check constraint in SQL Server allows you to specify a condition on each row in a table.
    /// </summary>
    public class CheckConstraint
	{
        /// <summary>
        /// Constraint qualifier.
        /// </summary>
		public string ConstraintCatalog {get; internal set;}

        /// <summary>
        /// Name of the schema to which the constraint belongs. ** Important ** Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.
        /// </summary>
		public string ConstraintSchema {get; internal set;}

        /// <summary>
        /// Constraint name.
        /// </summary>
		public string ConstraintName {get; internal set;}

        /// <summary>
        /// Actual text of the Transact-SQL definition statement.
        /// </summary>
		public string CheckClause {get; internal set;}
	}
}
