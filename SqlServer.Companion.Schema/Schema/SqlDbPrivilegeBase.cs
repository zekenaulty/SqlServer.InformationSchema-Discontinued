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
	public abstract class SqlDbPrivilegeBase : SqlDbTableBase, ISqlDataObject
	{
		#region GRANTOR
		protected string grantor = string.Empty;
		public string Grantor
		{
			get{return grantor;}
			//set{if(value != grantor) grantor = value;}
		}
		#endregion
		#region GRANTEE
		protected string grantee = string.Empty;
		public string Grantee
		{
			get{return grantee;}
			//set{if(value != grantee) grantee = value;}
		}
		#endregion
		#region PRIVILEGE_TYPE
		protected string privilege_type = string.Empty;
		public string PrivilegeType
		{
			get{return privilege_type;}
			//set{if(value != privilege_type) privilege_type = value;}
		}
		#endregion
		#region IS_GRANTABLE
		protected string is_grantable = string.Empty;
		public string IsGrantable
		{
			get{return is_grantable;}
			//set{if(value != is_grantable) is_grantable = value;}
		}
		#endregion
		new public object Clone()
		{
			return base.Clone();
		}
	}
}
