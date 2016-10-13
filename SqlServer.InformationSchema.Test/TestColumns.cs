using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer.InformationSchema.Test
{
    class TestColumns
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="columns"></param>
        private static void EmitColumns(List<Column> columns)
        {
            for (int i = 0; i < columns.Count; i++)
                Utility.EmitProperties(columns[i]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        internal static void FindAll(SqlConnection connection)
        {
            Console.WriteLine("ColumnFactory.FindAll");
            EmitColumns(ColumnFactory.FindAll(connection));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        internal static void FindByTable(SqlConnection connection, string schemaName, string tableName)
        {
            Console.WriteLine("ColumnFactory.FindByTable");
            EmitColumns(ColumnFactory.FindByTable(connection, schemaName, tableName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="schemaName"></param>
        /// <param name="tableName"></param>
        /// <param name="columnName"></param>
        internal static void FindByColumn(SqlConnection connection, string schemaName, string tableName, string columnName)
        {
            Console.WriteLine("ColumnFactory.FindByColumn");
            EmitColumns(ColumnFactory.FindByColumn(connection, schemaName, tableName, columnName));
        }

    }
}
