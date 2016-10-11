namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class RoutineColumn
	{
		public string TableCatalog {get; internal set;}
		public string TableSchema {get; internal set;}
		public string TableName {get; internal set;}
		public string ColumnName {get; internal set;}
		public int OrdinalPosition {get; internal set;}
		public string ColumnDefault {get; internal set;}
		public string IsNullable {get; internal set;}
		public string DataType {get; internal set;}
		public int CharacterMaximumLength {get; internal set;}
		public int CharacterOctetLength {get; internal set;}
		public byte NumericPrecision {get; internal set;}
		public short NumericPrecisionRadix {get; internal set;}
		public int NumericScale {get; internal set;}
		public short DatetimePrecision {get; internal set;}
		public string CharacterSetCatalog {get; internal set;}
		public string CharacterSetSchema {get; internal set;}
		public string CharacterSetName {get; internal set;}
		public string CollationCatalog {get; internal set;}
		public string CollationSchema {get; internal set;}
		public string CollationName {get; internal set;}
		public string DomainCatalog {get; internal set;}
		public string DomainSchema {get; internal set;}
		public string DomainName {get; internal set;}
	}
}
