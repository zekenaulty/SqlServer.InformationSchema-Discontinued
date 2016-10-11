namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ConstraintTableUsage
	{
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
		public string ConstraintCatalog {get; internal set;}
		public string ConstraintSchema {get; internal set;}
		public string ConstraintName {get; internal set;}
	}
}
