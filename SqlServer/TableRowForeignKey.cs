using System;

namespace SqlServer.Companion
{
	/// <summary></summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field ,AllowMultiple = false, Inherited = true)]
	public class TableRowForeignKey : Attribute
	{
		public TableRowForeignKey(string joinTable, string joinOnColumn)
		{
			_table = joinTable;
			_column = joinOnColumn;
		}
		private string _table = string.Empty;
		public string JoinTable{get{return _table;}}

		private string _column = string.Empty;
		public string JoinOnColumn{get{return _column;}}
		
		//add GetAttributes
	}
}
