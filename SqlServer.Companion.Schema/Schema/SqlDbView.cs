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
	public sealed class SqlDbView : SqlDbTableBase, ISqlDataObject
	{
		#region Construction
		internal SqlDbView(){}
		#endregion
		#region Columns
		SqlDbViewFieldCollection col = null;
		public SqlDbViewFieldCollection Columns
		{
			get
			{
				if(null == col)
					col = SqlSchema.FindViewFields(__conn, this.table_name);

				return col;
			}

		}
		#endregion
		#region +Build DataReader
		public static SqlDbView Build(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbView v = new SqlDbView();

			v.__conn = connection;

			v.table_catalog = (r.IsDBNull(0)) ? string.Empty : r.GetString(0);
			v.table_schema = (r.IsDBNull(1)) ? string.Empty : r.GetString(1);
			v.table_name = (r.IsDBNull(2)) ? string.Empty : r.GetString(2);
			v.__conn = connection;

			return v;
		}
		#endregion
		#region +BuildCollection DataReader
		public static SqlDbViewCollection BuildCollection(SqlDataReader r, SqlConnectionSource connection)
		{
			SqlDbViewCollection vc = new SqlDbViewCollection();

			vc.Add(Build(r, connection));
			while(r.Read())
				vc.Add(Build(r, connection));

			return vc;
		}
		#endregion
		#region +Attach
		internal static SqlDbView Attach(SqlDbView view, SqlDbViewFieldCollection fields)
		{
			view.col = fields;
			return view;
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}