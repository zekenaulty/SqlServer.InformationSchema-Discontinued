namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class Parameter
	{
		public string SpecificCatalog {get; internal set;}
		public string SpecificSchema {get; internal set;}
		public string SpecificName {get; internal set;}
		public int OrdinalPosition {get; internal set;}
		public string ParameterMode {get; internal set;}
		public string IsResult {get; internal set;}
		public string AsLocator {get; internal set;}
		public string ParameterName {get; internal set;}
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
		public string IntervalType {get; internal set;}
		public short IntervalPrecision {get; internal set;}
		public string UserDefinedTypeCatalog {get; internal set;}
		public string UserDefinedTypeSchema {get; internal set;}
		public string UserDefinedTypeName {get; internal set;}
		public string ScopeCatalog {get; internal set;}
		public string ScopeSchema {get; internal set;}
		public string ScopeName {get; internal set;}
	}
}
