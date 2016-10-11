namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
	public class Routine
	{
		public string SpecificCatalog {get; internal set;}
		public string SpecificSchema {get; internal set;}
		public string SpecificName {get; internal set;}
		public string RoutineCatalog {get; internal set;}
		public string RoutineSchema {get; internal set;}
		public string RoutineName {get; internal set;}
		public string RoutineType {get; internal set;}
		public string ModuleCatalog {get; internal set;}
		public string ModuleSchema {get; internal set;}
		public string ModuleName {get; internal set;}
		public string UdtCatalog {get; internal set;}
		public string UdtSchema {get; internal set;}
		public string UdtName {get; internal set;}
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
		public string TypeUdtCatalog {get; internal set;}
		public string TypeUdtSchema {get; internal set;}
		public string TypeUdtName {get; internal set;}
		public string ScopeCatalog {get; internal set;}
		public string ScopeSchema {get; internal set;}
		public string ScopeName {get; internal set;}
		public long MaximumCardinality {get; internal set;}
		public string DtdIdentifier {get; internal set;}
		public string RoutineBody {get; internal set;}
		public string RoutineDefinition {get; internal set;}
		public string ExternalName {get; internal set;}
		public string ExternalLanguage {get; internal set;}
		public string ParameterStyle {get; internal set;}
		public string IsDeterministic {get; internal set;}
		public string SqlDataAccess {get; internal set;}
		public string IsNullCall {get; internal set;}
		public string SqlPath {get; internal set;}
		public string SchemaLevelRoutine {get; internal set;}
		public short MaxDynamicResultSets {get; internal set;}
		public string IsUserDefinedCast {get; internal set;}
		public string IsImplicitlyInvocable {get; internal set;}
		public DateTime Created {get; internal set;}
		public DateTime LastAltered {get; internal set;}
	}
}
