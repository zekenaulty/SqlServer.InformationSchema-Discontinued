namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ViewTableUsage
	{
		public string ViewCatalog {get; internal set;}
		public string ViewSchema {get; internal set;}
		public string ViewName {get; internal set;}
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
	}
}
