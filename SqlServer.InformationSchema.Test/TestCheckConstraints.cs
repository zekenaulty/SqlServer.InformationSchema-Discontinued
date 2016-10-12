using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer.InformationSchema.Test
{
    class TestCheckConstraints
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="constraints"></param>
        private static void EmitCheckConstrants(List<CheckConstraint> constraints)
        {
            for (int i = 0; i < constraints.Count; i++)
                Utility.EmitProperties(constraints[i]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection">SqlConnection</param>
        internal static void FindAll(SqlConnection connection)
        {
            Console.WriteLine("CheckConstraintFactory.FindAll");
            EmitCheckConstrants(CheckConstraintFactory.FindAll(connection));         
        }

        internal static void FindBySchema(SqlConnection connection, string schema)
        {
            Console.WriteLine($"CheckConstraintFactory.FindSchema - {schema}");
            EmitCheckConstrants(CheckConstraintFactory.FindBySchema(connection, schema));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="schema"></param>
        /// <param name="name"></param>
        internal static void FindByName(SqlConnection connection, string schema, string name)
        {
            Console.WriteLine($"CheckConstraintFactory.FindSchema - {schema}.{name}");
            EmitCheckConstrants(CheckConstraintFactory.FindByName(connection, schema, name));
        }

    }
}
