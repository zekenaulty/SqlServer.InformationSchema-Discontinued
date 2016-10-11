namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class Table
	{
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
		public string TableType {get; internal set;}
	}
}
