namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class CheckConstraint
	{
		public string ConstraintCatalog {get; internal set;}
		public string ConstraintSchema {get; internal set;}
		public string ConstraintName {get; internal set;}
		public string CheckClause {get; internal set;}
	}
}
