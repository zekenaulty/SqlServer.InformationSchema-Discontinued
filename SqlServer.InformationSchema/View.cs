namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class View
	{
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
		public string ViewDefinition {get; internal set;}
		public string CheckOption {get; internal set;}
		public string IsUpdatable {get; internal set;}
	}
}
