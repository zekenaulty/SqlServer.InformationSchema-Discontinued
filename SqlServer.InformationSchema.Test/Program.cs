using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlServer.InformationSchema.Test
{
    /// <summary>
    /// Because an actual database is needed to test these classes I have chosen to use a simple console application instead of using Unit tests.
    /// These tests will be based on the AdventureWorks sample database: https://www.microsoft.com/en-us/download/details.aspx?id=49502
    /// </summary>
    class Program
    {
        /// <summary>
        /// Program entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            Console.BufferWidth = 3000;
            Console.BufferHeight = short.MaxValue-1;
            Console.WindowWidth = Console.LargestWindowWidth - 100;
            Console.WindowHeight = Console.LargestWindowHeight - 40;

            Console.WriteLine("SqlServer.InformationSchema Test(s)");

            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["test"].ConnectionString))
            {
                #region CHECK_CONSTRAINTS
                if(RunTestSection("CHECK_CONSTRANTS"))
                {
                    TestCheckConstraints.FindByName(connection, "Production", "CK_Product_ProductLine");
                    EnterForNext();

                    TestCheckConstraints.FindBySchema(connection, "Purchasing");
                    EnterForNext();

                    if(RunTestSection("CHECK_CONSTRAINT FindAll"))
                    {
                        TestCheckConstraints.FindAll(connection);
                        EnterForNext();
                    }
                }
                #endregion
                #region COLUMNS
                if(RunTestSection("COLUMNS"))
                {
                    TestColumns.FindByTable(connection, "HumanResources", "Department");
                    EnterForNext();

                    TestColumns.FindByColumn(connection, "Production", "ProductCategory", "Name");
                    EnterForNext();

                    if(RunTestSection("Columns FindAll"))
                    {
                        TestColumns.FindAll(connection);
                        EnterForNext();
                    }
                }
                #endregion
            }

            Console.Write("Press ENTER to exit.");
            WaitForEnter();
        }

        /// <summary>
        /// Show a message and wait for enter.
        /// </summary>
        static void EnterForNext()
        {
            Console.Write("Press ENTER to run next test.");
            WaitForEnter();
            Console.Clear();
        }

        /// <summary>
        /// A key press of Y returns true and N return false; all other key strokes are ignored.
        /// </summary>
        /// <param name="section"></param>
        /// <returns></returns>
        static bool RunTestSection(string section)
        {
            Console.Clear();
            Console.Write($"Press Y to run {section} tests N to skip.");

            ConsoleKey key = Console.ReadKey(true).Key;
            while(key != ConsoleKey.Y && key != ConsoleKey.N)
            {
                key = Console.ReadKey(true).Key;
            }

            Console.WriteLine();

            if (key == ConsoleKey.Y) return true;

            return false;
        }

        /// <summary>
        /// Wait for the user to press ENTER and supress all other key strokes.
        /// </summary>
        static void WaitForEnter()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey(true).Key;
            }

            Console.WriteLine();
        }
    }
}
