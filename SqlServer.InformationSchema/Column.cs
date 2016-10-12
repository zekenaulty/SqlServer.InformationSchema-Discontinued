namespace SqlServer.InformationSchema
{
	using System;
	using System.Collections.Generic;
	using System.Data;
	using System.Data.SqlClient;
	using System.Linq;
	
    /// <summary>
    /// Describes a table column.
    /// </summary>
	public class Column
	{
        /// <summary>
        /// Table qualifier.
        /// </summary>
		public string TableCatalog {get; internal set;}

        /// <summary>
        /// Name of schema that contains the table. ** Important** Do not use INFORMATION_SCHEMA views to determine the schema of an object. The only reliable way to find the schema of a object is to query the sys.objects catalog view.
        /// </summary>
		public string TableSchema {get; internal set; }

        /// <summary>
        /// Table name.
        /// </summary>
		public string TableName {get; internal set;}

        /// <summary>
        /// Column name.
        /// </summary>
		public string ColumnName {get; internal set;}

        /// <summary>
        /// Column identification number.
        /// </summary>
		public int OrdinalPosition {get; internal set;}

        /// <summary>
        /// Default value of the column.
        /// </summary>
		public string ColumnDefault {get; internal set;}

        /// <summary>
        /// Nullability of the column. If this column allows for NULL, this column returns YES. Otherwise, NO is returned.
        /// </summary>
		public string IsNullable {get; internal set;}

        /// <summary>
        /// System-supplied data type.
        /// </summary>
		public string DataType {get; internal set;}

        /// <summary>
        /// Maximum length, in characters, for binary data, character data, or text and image data. -1 for xml and large-value type data.Otherwise, NULL is returned.
        /// </summary>
		public int CharacterMaximumLength {get; internal set; }

        /// <summary>
        /// Maximum length, in bytes, for binary data, character data, or text and image data. -1 for xml and large-value type data.Otherwise, NULL is returned.
        /// </summary>
		public int CharacterOctetLength {get; internal set; }

        /// <summary>
        /// Precision of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        /// </summary>
		public byte NumericPrecision {get; internal set;}

        /// <summary>
        /// Precision radix of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        /// </summary>
		public short NumericPrecisionRadix {get; internal set;}

        /// <summary>
        /// Scale of approximate numeric data, exact numeric data, integer data, or monetary data. Otherwise, NULL is returned.
        /// </summary>
		public int NumericScale {get; internal set;}

        /// <summary>
        /// Subtype code for datetime and ISO interval data types. For other data types, NULL is returned.
        /// </summary>
		public short DatetimePrecision {get; internal set;}

        /// <summary>
        /// Returns master. This indicates the database in which the character set is located, if the column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
		public string CharacterSetCatalog {get; internal set;}

        /// <summary>
        /// Always returns NULL.
        /// </summary>
		public string CharacterSetSchema {get; internal set;}

        /// <summary>
        /// Returns the unique name for the character set if this column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
		public string CharacterSetName {get; internal set;}

        /// <summary>
        /// Always returns NULL.
        /// </summary>
		public string CollationCatalog {get; internal set;}

        /// <summary>
        /// Always returns NULL.
        /// </summary>
		public string CollationSchema {get; internal set;}

        /// <summary>
        /// Returns the unique name for the collation if the column is character data or text data type. Otherwise, NULL is returned.
        /// </summary>
		public string CollationName {get; internal set;}

        /// <summary>
        /// If the column is an alias data type, this column is the database name in which the user-defined data type was created. Otherwise, NULL is returned.
        /// </summary>
		public string DomainCatalog {get; internal set; }

        /// <summary>
        /// If the column is a user-defined data type, this column returns the name of the schema of the user-defined data type. Otherwise, NULL is returned. ** Important** Do not use INFORMATION_SCHEMA views to determine the schema of a data type.The only reliable way to find the schema of a type is to use the TYPEPROPERTY function.
        /// </summary>
		public string DomainSchema {get; internal set;}

        /// <summary>
        /// If the column is a user-defined data type, this column is the name of the user-defined data type. Otherwise, NULL is returned.
        /// </summary>
		public string DomainName {get; internal set;}
	}
}
