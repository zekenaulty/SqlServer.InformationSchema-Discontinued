namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ColumnDomainUsage
	{
		public string DomainCatalog {get; internal set;}
		public string DomainSchema {get; internal set;}
		public string DomainName {get; internal set;}
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
		public string ColumnName {get; internal set;}
	}
}
