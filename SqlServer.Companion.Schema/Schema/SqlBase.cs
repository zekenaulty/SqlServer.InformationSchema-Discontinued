using System;

namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	public abstract class SqlBase : ISqlSchemaObject, ICloneable
	{
		protected SqlConnectionSource __conn = null;
		public virtual object Clone(){return null;}

		DateTime _dc = DateTime.Now;
		public DateTime InstanceCreated
		{
			get{return _dc;}
		}

		DateTime _da = DateTime.Now;
		public DateTime InstanceAccessed
		{
			get{return _da;}
			set{_da = value;}
		}
	}
}