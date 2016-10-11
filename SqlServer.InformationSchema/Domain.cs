namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class Domain
	{
		public string DomainCatalog {get; internal set;}
		public string DomainSchema {get; internal set;}
		public string DomainName {get; internal set;}
		public string DataType {get; internal set;}
		public int CharacterMaximumLength {get; internal set;}
		public int CharacterOctetLength {get; internal set;}
		public string CollationCatalog {get; internal set;}
		public string CollationSchema {get; internal set;}
		public string CollationName {get; internal set;}
		public string CharacterSetCatalog {get; internal set;}
		public string CharacterSetSchema {get; internal set;}
		public string CharacterSetName {get; internal set;}
		public byte NumericPrecision {get; internal set;}
		public short NumericPrecisionRadix {get; internal set;}
		public int NumericScale {get; internal set;}
		public short DatetimePrecision {get; internal set;}
		public string DomainDefault {get; internal set;}
	}
}
