using System;


using SqlServer.Companion.Base;
namespace SqlServer.Companion.Schema
{
	/// <summary></summary>
	public interface ISqlSchemaObject:ISqlDataObject
	{
		/// <summary>
		/// The DateTime the instance was created/cloned.
		/// </summary>
		DateTime InstanceCreated{get;}
		/// <summary>
		/// The DateTime when the item was last accessed.
		/// </summary>
		DateTime InstanceAccessed{get; set;}
	}
}