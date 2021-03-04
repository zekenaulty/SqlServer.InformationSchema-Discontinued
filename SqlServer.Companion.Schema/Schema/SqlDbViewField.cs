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
	public sealed class SqlDbViewField : SqlDbFieldBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbViewField(){}
		#endregion
		#region VIEW_CATALOG
		string view_catalog = string.Empty;
		public string ViewCatalog
		{
			get{return view_catalog;}
			//set{if(value != view_catalog) view_catalog = value;}
		}
		#endregion
		#region VIEW_SCHEMA
		string view_schema = string.Empty;
		public string ViewSchema
		{
			get{return view_schema;}
			//set{if(value != view_schema) view_schema = value;}
		}
		#endregion
		#region VIEW_NAME
		string view_name = string.Empty;
		public string ViewName
		{
			get{return view_name;}
			//set{if(value != view_name) view_name = value;}
		}
		#endregion
		#region +Build DataReader
		internal static SqlDbViewField Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbViewField vf = new SqlDbViewField();
			
			vf.__conn = connection;

			vf.view_catalog		= (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			vf.view_schema		= (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			vf.view_name		= (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			vf.table_catalog	= (r.IsDBNull(3)) ? string.Empty : r.GetString(3);
			vf.table_schema		= (r.IsDBNull(4)) ? string.Empty : r.GetString(4);
			vf.table_name		= (r.IsDBNull(5)) ? string.Empty : r.GetString(5);
			vf.column_name		= (r.IsDBNull(6)) ? string.Empty : r.GetString(6);
			vf.__conn = connection;
			return vf;
		}
		#endregion
		#region +BuildCollection DataReader
		internal static SqlDbViewFieldCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbViewFieldCollection vfc = new SqlDbViewFieldCollection();

			vfc.Add(Build(r, connection));
			while(r.Read())
				vfc.Add(Build(r, connection));

			return vfc;
		}
		#endregion
		new public object Clone()
		{
			SqlDbViewField ret = new SqlDbViewField();
			return base.Clone();
		}
	}
}