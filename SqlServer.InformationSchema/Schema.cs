namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class Schema
	{
		public string CatalogName {get; internal set;}
		public string SchemaName {get; internal set;}
		public string SchemaOwner {get; internal set;}
		public string DefaultCharacterSetCatalog {get; internal set;}
		public string DefaultCharacterSetSchema {get; internal set;}
		public string DefaultCharacterSetName {get; internal set;}
	}
}
