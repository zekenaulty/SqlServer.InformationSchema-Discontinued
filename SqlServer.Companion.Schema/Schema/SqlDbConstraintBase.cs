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
	public abstract class SqlDbConstraintBase : SqlBase, ISqlDataObject
	{
		#region CONSTRAINT_CATALOG
		protected string constraint_catalog = string.Empty;
		public string ConstraintCatalog
		{
			get{return constraint_catalog;}
			//set{if(value != constraint_catalog) constraint_catalog = value;}
		}
		#endregion
		#region CONSTRAINT_SCHEMA
		protected string constraint_schema = string.Empty;
		public string ConstraintSchema
		{
			get{return constraint_schema;}
			//set{if(value != constraint_schema) constraint_schema = value;}
		}
		#endregion
		#region CONSTRAINT_NAME
		protected string constraint_name = string.Empty;
		public string ConstraintName
		{
			get{return constraint_name;}
			//set{if(value != constraint_name) constraint_name = value;}
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}