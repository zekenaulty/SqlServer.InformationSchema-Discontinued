namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ReferentialConstraint
	{
		public string ConstraintCatalog {get; internal set;}
		public string ConstraintSchema {get; internal set;}
		public string ConstraintName {get; internal set;}
		public string UniqueConstraintCatalog {get; internal set;}
		public string UniqueConstraintSchema {get; internal set;}
		public string UniqueConstraintName {get; internal set;}
		public string MatchOption {get; internal set;}
		public string UpdateRule {get; internal set;}
		public string DeleteRule {get; internal set;}
	}
}
