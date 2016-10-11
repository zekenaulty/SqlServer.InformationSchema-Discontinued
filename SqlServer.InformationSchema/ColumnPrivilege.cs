namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class ColumnPrivilege
	{
		public string Grantor {get; internal set;}
		public string Grantee {get; internal set;}
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
		public string ColumnName {get; internal set;}
		public string PrivilegeType {get; internal set;}
		public string IsGrantable {get; internal set;}
	}
}
