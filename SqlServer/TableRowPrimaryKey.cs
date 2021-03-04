using System;

namespace SqlServer.Companion
{
	/// <summary></summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field ,AllowMultiple = false, Inherited = true)]
	public class TableRowPrimaryKey : Attribute
	{
		public TableRowPrimaryKey(){}

		//add GetAttributes
	}
}
