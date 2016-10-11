namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class DomainConstraint
	{
		public string ConstraintCatalog {get; internal set;}
		public string ConstraintSchema {get; internal set;}
		public string ConstraintName {get; internal set;}
		public string DomainCatalog {get; internal set;}
		public string DomainSchema {get; internal set;}
		public string DomainName {get; internal set;}
		public string IsDeferrable {get; internal set;}
		public string InitiallyDeferred {get; internal set;}
	}
}
