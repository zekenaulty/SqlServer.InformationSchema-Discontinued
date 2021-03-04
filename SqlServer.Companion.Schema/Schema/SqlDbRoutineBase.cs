using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	[Serializable]
	public abstract class SqlDbRoutineBase : SqlBase, ISqlDataObject
	{
		#region SPECIFIC_CATALOG
		protected string specific_catalog = string.Empty;
		public string SpecificCatalog
		{
			get{return specific_catalog;}
			//set{if(value != specific_catalog) specific_catalog = value;}
		}
		#endregion
		#region SPECIFIC_SCHEMA
		protected string specific_schema = string.Empty;
		public string SpecificSchema
		{
			get{return specific_schema;}
			//set{if(value != specific_schema) specific_schema = value;}
		}
		#endregion
		#region SPECIFIC_NAME
		protected string specific_name = string.Empty;
		public string SpecificName
		{
			get{return specific_name;}
			//set{if(value != specific_name) specific_name = value;}
		}
		#endregion
		#region CHARACTER_MAXIMUM_LENGTH
		protected int charachter_maximum_length = -1;
		public int CharacterMaxLength
		{
			get{return charachter_maximum_length;}
			//set{if(value != charachter_maximum_length) charachter_maximum_length = value;}
		}
		#endregion
		#region CHARACTER_OCTET_LENGTH
		protected int charachter_octet_length = -1;
		public int CharacterOctetLength
		{
			get{return charachter_octet_length;}
			//set{if(value != charachter_octet_length) charachter_octet_length = value;}
		}
		#endregion
		#region NUMERIC_PRECISION
		protected short numeric_precision = -1;
		public short NumericPrecision
		{
			get{return numeric_precision;}
			//set{if(value != numeric_precision) numeric_precision = value;}
		}
		#endregion
		#region NUMERIC_PRECISION_RADIX
		protected short numeric_precision_radix = -1;
		public short NumericPrecisionRadix
		{
			get{return numeric_precision_radix;}
			//set{if(value != numeric_precision_radix) numeric_precision_radix = value;}
		}
		#endregion
		#region NUMERIC_SCALE
		protected short numeric_scale = -1;
		public short NumericScale
		{
			get{return numeric_scale;}
			//set{if(value != numeric_scale) numeric_scale = value;}
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
