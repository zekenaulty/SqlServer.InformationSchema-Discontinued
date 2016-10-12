using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace SqlServer.InformationSchema.Test
{
    class Utility
    {
        /// <summary>
        /// PropertyInfo cache.
        /// </summary>
        static Dictionary<Type, PropertyInfo[]> Properties = new Dictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Use reflection to write the properties of an object to the console.
        /// </summary>
        /// <param name="o"></param>
        internal static void EmitProperties(object o)
        {
            Type oType = o.GetType();

            if (!Properties.ContainsKey(oType))
                Properties.Add(oType, oType.GetProperties());

            PropertyInfo[] pi = Properties[oType];

            Console.WriteLine($"\t{oType.Name}");
            for (int i = 0; i < pi.Length; i++)
                Console.WriteLine($"\t\t{pi[i].Name} - {pi[i].GetValue(o)}");

        }

    }
}
