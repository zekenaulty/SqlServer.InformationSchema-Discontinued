using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;

using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;

using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;

using System.Windows.Forms;
using System.Xml;

using Microsoft.CSharp;
using Microsoft.VisualBasic;

using SqlServer.Companion;
using SqlServer.Companion.Schema;


namespace SqlServer.Companion.CodeDom
{
	/// <summary>Class providing methods to create sql stored procedures.</summary>
	/// <remarks>If the tables used with this class do not have an int or uniqueidentifier Primary Key generated code could be invalid.</remarks>
	public class SqlStoredProcedureGenerator
	{
		#region Helpers
		private static void AddStr(StringBuilder sb, string str){sb.Append(str);}
		private static void AddCrLf(StringBuilder sb){sb.Append("\r\n");}
		private static void AddParamName(StringBuilder sb, SqlDbTableField field){sb.Append("@" + field.ColumnName);}
		#endregion
		#region CRUD (INSERT, UPDATE, and DELETE)
		/// <summary>
		/// Returns the SQL that will create a stored procedure to delete data from the in passed table by primary key.
		/// </summary>
		/// <param name="conn">SqlConnectionSource used to run the SQL if run is set to true.</param>
		/// <param name="tbl">The SqlDbTable used to create the SQL.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		public static string CreateDelete(
			SqlConnectionSource conn, 
			SqlDbTable tbl,
			bool drop,
			bool run)
		{
			string ret = "";
            string proc_name = tbl.TableName.Replace(" ", string.Empty) + "__DELETE_ROW";
			StringBuilder sb = new StringBuilder();


			if(drop == true && run == true)
			{
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + proc_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + proc_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			AddStr(sb,"CREATE PROCEDURE " + proc_name);
			AddCrLf(sb);
			
			AddStr(sb, "(");

			if(null != tbl.Columns && tbl.Columns.Count >= 1)
			{
				string where = "";

				if(null != tbl.Constraints)
				{
					foreach(SqlDbTableConstraint tc in tbl.Constraints)
					{
						if(tc.IsPrimaryKey)
						{
							if(null != tbl.Columns)
							{
								foreach(SqlDbTableField fld in tbl.Columns)
								{
									if(fld.ColumnName == tc.ConstraintColumnName)
									{
										AddStr(sb, "\r\n@" + fld.ColumnName + " " + fld.DataType);
										break;
									}
								}
							}
							where = "WHERE\r\n\t" + tc.ConstraintColumnName + " = @" + tc.ConstraintColumnName;
							break;
						}
					}
				}

				AddCrLf(sb);

				AddStr(sb, ")");
				AddCrLf(sb);

				AddStr(sb, "AS");
				AddCrLf(sb);

				AddStr(sb, "DELETE FROM [" + tbl.TableName + "] ");
				AddCrLf(sb);

				AddStr(sb, where);

				AddCrLf(sb);
				AddCrLf(sb);
				
				AddStr(sb, "RETURN");
			}

			ret = sb.ToString();
			if(run == true)
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);

			return ret;
		}


		/// <summary>
		/// Returns the SQL that will create a stored procedure to insert a row of data into the in passed table.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		public static string CreateInsert(
			SqlConnectionSource conn, 
			SqlDbTable tbl,
			bool drop,
			bool run)
		{
			string ret = "";
			string proc_name = tbl.TableName.Replace(" ", string.Empty) + "__INSERT_ROW";
			StringBuilder sb = new StringBuilder();

			Type pk_type = null;

			if(drop == true && run == true)
			{
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + proc_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + proc_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			AddStr(sb,"CREATE PROCEDURE " + proc_name);
			AddCrLf(sb);
			
			AddStr(sb, "(");

			if(null != tbl.Columns && tbl.Columns.Count >= 1)
			{
				string pk = "";
				string col = "(\r\n";
				string val = "VALUES\r\n(\r\n";
				if(null != tbl.Constraints)
				{
					foreach(SqlDbTableConstraint tc in tbl.Constraints)
					{
						if(tc.IsPrimaryKey)
						{
							pk = tc.ConstraintColumnName;
							
							break;
						}
					}
				}

				AddCrLf(sb);

				int atFld = 1;
				foreach(SqlDbTableField fld in tbl.Columns)
				{
					if(fld.ColumnName == pk && (fld.ValueType == typeof(System.Int32) || fld.ValueType == typeof(System.Guid)))
					{
						pk_type = fld.ValueType;

						AddStr(sb, "\t");
						AddParamName(sb, fld);
						AddStr(sb, " " + fld.DataType + " OUTPUT");

						if(pk_type != null && pk_type == typeof(System.Guid))
						{
							if(atFld < tbl.Columns.Count)
							{
								col += "\t" + fld.ColumnName + ", \r\n";
								val += "\t@New" + fld.ColumnName + ", \r\n";
							}
							else
							{
								col += "\t" + fld.ColumnName + "\r\n)\r\n";
								val += "\t@New" + fld.ColumnName + "\r\n)\r\n";
							}
						}

						if(atFld < (tbl.Columns.Count - 1))
							AddStr(sb, ", \r\n");
						else if((atFld <= (tbl.Columns.Count - 1)) && tbl.Columns.Count == 2)
							AddStr(sb, ", \r\n");


					}
					else
					{
						string dt = " " + fld.DataType;

                        if (fld.DataType == "varchar" || fld.DataType == "nvarchar")
							dt += "(" + fld.CharacterMaximumLength.ToString() + ")";

						AddStr(sb, "\t");
						AddParamName(sb, fld);
					

						col += "\t" + fld.ColumnName;//build col and values statements
						val += "\t@" + fld.ColumnName;

						if(atFld < tbl.Columns.Count)
						{
							dt += ", \r\n";
							col += ", \r\n";
							val += ", \r\n";
						}
						else
						{
							dt += "\r\n";
							col += "\r\n)\r\n";
							val += "\r\n)\r\n";
						}

						AddStr(sb, dt);
					}
					atFld++;
				} //end foreach

				AddStr(sb, ")");
				AddCrLf(sb);
				
				AddStr(sb, "AS");
				AddCrLf(sb);

				if(null != pk_type && pk_type == typeof(System.Guid))
				{
					AddStr(sb, "DECLARE @New" + pk + " UNIQUEIDENTIFIER\r\n");
					AddStr(sb, "SET @New" + pk + " = NEWID()\r\n");
					AddCrLf(sb);
				}

				AddStr(sb, "INSERT INTO [" + tbl.TableName + "] ");
				AddCrLf(sb);

				AddStr(sb, col);
				AddStr(sb, val);

				AddCrLf(sb);
				
				if(null != pk_type && pk_type == typeof(System.Int32))
					AddStr(sb, "\r\nSET @" + pk + " = @@IDENTITY\r\n\r\n");
				else if(null != pk_type && pk_type == typeof(System.Guid))
					AddStr(sb, "\r\nSET @" + pk + " = @New" + pk + "\r\n\r\n");

				AddStr(sb, "RETURN");
			}

			ret = sb.ToString();
			if(run == true)
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);

			return ret;
		}


		/// <summary>
		/// Returns the SQL that will create a stored procedure used to update a row of information in the in passed table.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		public static string CreateUpdate(
			SqlConnectionSource conn, 
			SqlDbTable tbl,
			bool drop,
			bool run)
		{
			string ret = "";
            string proc_name = tbl.TableName.Replace(" ", string.Empty) + "__UPDATE_ROW";
			StringBuilder sb = new StringBuilder();


			if(drop == true && run == true)
			{
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + proc_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + proc_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			AddStr(sb,"CREATE PROCEDURE " + proc_name);
			AddCrLf(sb);
			
			AddStr(sb, "(");

			string pk = "";
			if(null != tbl.Columns && tbl.Columns.Count >= 1)
			{
				string col = "SET\r\n";
				string where = "";

				if(null != tbl.Constraints)
				{
					foreach(SqlDbTableConstraint tc in tbl.Constraints)
					{
						if(tc.IsPrimaryKey)
						{
							pk = tc.ConstraintColumnName;
							where = "WHERE\r\n\t" + tc.ConstraintColumnName + " = @" + tc.ConstraintColumnName;
							break;
						}
					}
				}

				AddCrLf(sb);

				int atFld = 1;
				foreach(SqlDbTableField fld in tbl.Columns)
				{
					string dt = " ";
					AddStr(sb, "\t");
					AddParamName(sb, fld);
					
					if(pk != fld.ColumnName)
						col += "\t" + fld.ColumnName + " = @" + fld.ColumnName;

					if(atFld < tbl.Columns.Count)
					{
                        if (fld.DataType == "varchar" || fld.DataType == "nvarchar")
							dt += fld.DataType + "(" + fld.CharacterMaximumLength.ToString() + "),\r\n";
						else
							dt += fld.DataType + ",\r\n";
						
						if(pk != fld.ColumnName && atFld < (tbl.Columns.Count) && tbl.Columns.Count >= 3)
							col += ", \r\n";
						else if(pk != fld.ColumnName && (atFld <= (tbl.Columns.Count)) && tbl.Columns.Count == 3)
							col += ", \r\n";
						
					}
					else
					{
                        if (fld.DataType == "varchar" || fld.DataType == "nvarchar")
							dt += fld.DataType + "(" + fld.CharacterMaximumLength.ToString() + ")\r\n";
						else
							dt += fld.DataType + "\r\n";
						
						if(pk != fld.ColumnName)
							col += "\r\n";
					}

					AddStr(sb, dt);
					atFld++;
				}

				AddStr(sb, ")");
				AddCrLf(sb);
				
				AddStr(sb, "AS");
				AddCrLf(sb);

				AddStr(sb, "UPDATE [" + tbl.TableName + "] ");
				AddCrLf(sb);

				AddStr(sb, col);
				AddStr(sb, where);

				AddCrLf(sb);
				AddCrLf(sb);
				
				AddStr(sb, "RETURN");
			}

			ret = sb.ToString();
			if(run == true)
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);

			return ret;
		}

		#endregion
		#region Standard Queries
		/// <summary>
		/// Returns the SQL used to create a stored procedure for retriving data based up in passed table and filters.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="filters">An ArrayList of SqlDbTableFields to be used to determine the parametrs of this query.</param>
		/// <param name="query_name">The name to be used for this stored procedure.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		/// <remarks>This version of the method use equal comparisons on filters.</remarks>
		public static string CreateQuery(
			SqlConnectionSource conn,
			SqlDbTable tbl,
			ArrayList filters,
			string query_name,
			bool drop,
			bool run)
		{
			StringBuilder sb = new StringBuilder();
			string ret = "";
			string where = "WHERE\r\n";
			int atFld = 1;

			if(drop == true && run == true)
			{	//add drop sql if needed
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + query_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + query_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}
			
			//we are going to create a stored procedure here
			AddStr(sb,"CREATE PROCEDURE " + query_name);
			AddCrLf(sb);
			if(null != filters && filters.Count > 0)
			{	//if there are filters open the parameter list
				AddStr(sb, "(");
				AddCrLf(sb);
			}

			foreach(SqlDbTableField fld in filters)
			{	//loop through filters
				AddStr(sb, "\t");
				AddParamName(sb, fld); //add the parameter name string
				AddStr(sb, " " + fld.DataType); //add the parameter datatype

                if (fld.DataType == "varchar" || fld.DataType == "nvarchar")//add size for varchar
					AddStr(sb, "(" + fld.CharacterMaximumLength.ToString() + ")");


				//format
				if(atFld <= (filters.Count - 1))
					AddStr(sb, ", \r\n");
				else if(atFld == 1 && filters.Count == 2)
					AddStr(sb, ", \r\n");
				else
					AddStr(sb, "\r\n");

				//build where clause string
				where += "\t@" + fld.ColumnName + " = " + fld.ColumnName;

				//check to see what we need to do to the where clause
				//and apply that format
				if(atFld <= (filters.Count - 1))
					where += "\r\nAND";
				else if(atFld == 1 && filters.Count == 2)
					where += "\r\nAND";
				else
					where += "\r\n";

				atFld++; //count that we have moved on
			}

			if(null != filters && filters.Count > 0)
			{	//if the parameter section was opend
				AddStr(sb, ")"); //close it
				AddCrLf(sb);
			}

			AddStr(sb, "AS");
			AddCrLf(sb); //begin query

			AddStr(sb, "SELECT");
			AddCrLf(sb);

			if(null != tbl.Columns)
			{	//loop through and add a request for each column
				atFld = 1; // start at 1
				foreach(SqlDbTableField f in tbl.Columns)
				{
					AddStr(sb, "\t" + f.ColumnName);	//add the column

					if(atFld < (tbl.Columns.Count))		//not the end
						AddStr(sb, ",\r\n");			// add comma
					else
						AddStr(sb, "\r\n");				// move on

					atFld++;	//track field #
				}
			}
			else
			{
				AddStr(sb, "\t*\r\n");	//if things are weird just catch it all
			}

			AddStr(sb, "FROM");	//add From clause
			AddCrLf(sb);

			AddStr(sb, "\t[" + tbl.TableName + "]");
			AddCrLf(sb);	//from this table

			if(filters.Count > 0)
				AddStr(sb, where);	//if we have a filter list add it

			AddStr(sb, "\r\n\r\nRETURN"); //return

			ret = sb.ToString();

			if(run == true)	//execute if needed
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);
            
			return ret;
		}


		/// <summary>
		/// Returns the SQL used to create a stored procedure for retriving data based up in passed table and filters.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="filters">A QueryFilterList of QueryFilters to be used to determine the parametrs of this query.</param>
		/// <param name="query_name">The name to be used for this stored procedure.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		public static string CreateQuery(
			SqlConnectionSource conn,
			SqlDbTable tbl,
			QueryFilterList filters,
			string query_name,
			bool drop,
			bool run)
		{
			StringBuilder sb = new StringBuilder();
			string ret = "";
			string where = "WHERE\r\n";
			int atFld = 1;

			if(drop == true && run == true)
			{	//add drop sql if needed
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + query_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + query_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}
			
			//we are going to create a stored procedure here
			AddStr(sb,"CREATE PROCEDURE " + query_name);
			AddCrLf(sb);
			if(null != filters && filters.Count > 0)
			{	//if there are filters open the parameter list
				AddStr(sb, "(");
				AddCrLf(sb);
			}

			foreach(QueryFilter f in filters)
			{
				//set quick field ref
				SqlDbTableField fld = f.Field;

				AddStr(sb, "\t");
				AddParamName(sb, fld); //add the parameter name string
				AddStr(sb, " " + fld.DataType); //add the parameter datatype

                if (fld.DataType == "varchar" || fld.DataType == "nvarchar")//add size for varchar
					AddStr(sb, "(" + fld.CharacterMaximumLength.ToString() + ")");


				//format
				if(atFld <= (filters.Count - 1))
					AddStr(sb, ", \r\n");
				else if(atFld == 1 && filters.Count == 2)
					AddStr(sb, ", \r\n");
				else
					AddStr(sb, "\r\n");

				//build where clause string
				where += "\t@" + fld.ColumnName + " = " + fld.ColumnName;

				//check to see what we need to do to the where clause
				//and apply that format
				if(atFld <= (filters.Count - 1))
					where += "\r\nAND";
				else if(atFld == 1 && filters.Count == 2)
					where += "\r\nAND";
				else
					where += "\r\n";

				atFld++; //count that we have moved on
			}

			if(null != filters && filters.Count > 0)
			{	//if the parameter section was opend
				AddStr(sb, ")"); //close it
				AddCrLf(sb);
			}

			AddStr(sb, "AS");
			AddCrLf(sb); //begin query

			AddStr(sb, "SELECT");
			AddCrLf(sb); //add select clause

			if(null != tbl.Columns)
			{	//loop through and add a request for each column
				atFld = 1; // start at 1
				foreach(SqlDbTableField f in tbl.Columns)
				{
					AddStr(sb, "\t" + f.ColumnName);	//add the column

					if(atFld < (tbl.Columns.Count))		//not the end
						AddStr(sb, ",\r\n");			// add comma
					else
						AddStr(sb, "\r\n");				// move on

					atFld++;	//track field #
				}
			}
			else
			{
				AddStr(sb, "\t*\r\n");	//if things are weird just catch it all
			}

			AddStr(sb, "FROM");	//add From clause
			AddCrLf(sb);

			AddStr(sb, "\t[" + tbl.TableName + "]");
			AddCrLf(sb);	//from this table

			if(filters.Count > 0)
				AddStr(sb, where);	//if we have a filter list add it

			AddStr(sb, "\r\n\r\nRETURN"); //return

			ret = sb.ToString();

			if(run == true)	//execute if needed
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);
            
			return ret;
		}
		#endregion
		#region Count Queries
		/// <summary>
		/// Returns the SQL used to create a stored procedure for retriving the number of records that can be retrived based up in passed table and filters.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="filters">An ArrayList of SqlDbTableFields to be used to determine the parametrs of this query.</param>
		/// <param name="query_name">The name to be used for this stored procedure.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		/// <remarks>This version of the method use equal comparisons on filters.</remarks>
		public static string CreateCountQuery(
			SqlConnectionSource conn,
			SqlDbTable tbl,
			ArrayList filters,
			string query_name,
			bool drop,
			bool run)
		{
			StringBuilder sb = new StringBuilder();
			string ret = "";
			string where = "WHERE\r\n";
			int atFld = 1;

			if(drop == true && run == true)
			{	//add drop if needed
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + query_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + query_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			//add create procedure
			AddStr(sb,"CREATE PROCEDURE " + query_name);
			AddCrLf(sb);
			if(filters.Count > 0)
			{	//open parameter list
				AddStr(sb, "(");
				AddCrLf(sb);
			}

			foreach(SqlDbTableField fld in filters)
			{
				AddStr(sb, "\t");
				AddParamName(sb, fld); // param name
				AddStr(sb, " " + fld.DataType); //add param type

                if (fld.DataType == "varchar" || fld.DataType == "nvarchar")//add size for varchar
					AddStr(sb, "(" + fld.CharacterMaximumLength.ToString() + ")");

				if(atFld < (filters.Count))
					AddStr(sb, ", \r\n");	//format for next param decleration
				else
					AddStr(sb, "\r\n");		//format as last param decleration

				where += "\t@" + fld.ColumnName + " = " + fld.ColumnName; //add where

				if(atFld <= (filters.Count - 1))
					where += "\r\nAND";		//format for next where
				else
					where += "\r\n";		//format for end of where

				atFld++;
			}
			if(filters.Count > 0)
			{	//close parameter list
				AddStr(sb, ")");
				AddCrLf(sb);
			}

			AddStr(sb, "AS");
			AddCrLf(sb); //begin query

			AddStr(sb, "SELECT");
			AddCrLf(sb); //add select clause

			AddStr(sb, "\tCOUNT(*)\r\n"); //add count clause

			AddStr(sb, "FROM");
			AddCrLf(sb); //add from clause

			AddStr(sb, "\t[" + tbl.TableName + "]");
			AddCrLf(sb); //add table name

			if(filters.Count > 0)
				AddStr(sb, where); //if there is a where add it

			AddStr(sb, "\r\n\r\nRETURN");

			ret = sb.ToString();

			if(run == true) //execute if needed
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);
            
			return ret;
		}


		/// <summary>
		/// Returns the SQL used to create a stored procedure for retriving the number of records that can be retrived based up in passed table and filters.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="filters">An QueryFilterList of QueryFilters to be used to determine the parametrs of this query.</param>
		/// <param name="query_name">The name to be used for this stored procedure.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		/// <remarks>This version of the method use equal comparisons on filters.</remarks>
		public static string CreateCountQuery(
			SqlConnectionSource conn,
			SqlDbTable tbl,
			QueryFilterList filters,
			string query_name,
			bool drop,
			bool run)
		{
			StringBuilder sb = new StringBuilder();
			string ret = "";
			string where = "WHERE\r\n";
			int atFld = 1;

			if(drop == true && run == true)
			{	//add drop if needed
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + query_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + query_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			//add create procedure
			AddStr(sb,"CREATE PROCEDURE " + query_name);
			AddCrLf(sb);
			if(filters.Count > 0)
			{	//open parameter list
				AddStr(sb, "(");
				AddCrLf(sb);
			}

			foreach(QueryFilter f in filters)
			{

				SqlDbTableField fld = f.Field;

				AddStr(sb, "\t");
				AddParamName(sb, fld); // param name
				AddStr(sb, " " + fld.DataType); //add param type

                if (fld.DataType == "varchar" || fld.DataType == "nvarchar")//add size for varchar
					AddStr(sb, "(" + fld.CharacterMaximumLength.ToString() + ")");

				if(atFld < (filters.Count))
					AddStr(sb, ", \r\n");	//format for next param decleration
				else
					AddStr(sb, "\r\n");		//format as last param decleration

				where += "\t@" + f.FilterString(); //add where

				if(atFld <= (filters.Count - 1))
					where += "\r\nAND";		//format for next where
				else
					where += "\r\n";		//format for end of where

				atFld++;
			}
			if(filters.Count > 0)
			{	//close parameter list
				AddStr(sb, ")");
				AddCrLf(sb);
			}

			AddStr(sb, "AS");
			AddCrLf(sb); //begin query

			AddStr(sb, "SELECT");
			AddCrLf(sb); //add select clause

			AddStr(sb, "\tCOUNT(*)\r\n"); //add count clause

			AddStr(sb, "FROM");
			AddCrLf(sb); //add from clause

			AddStr(sb, "\t[" + tbl.TableName + "]");
			AddCrLf(sb); //add table name

			if(filters.Count > 0)
				AddStr(sb, where); //if there is a where add it

			AddStr(sb, "\r\n\r\nRETURN");

			ret = sb.ToString();

			if(run == true) //execute if needed
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);
            
			return ret;
		}


		#endregion
		#region Paged Queries
		/// <summary>
		/// Returns the SQL used to create a stored procedure for retriving a certian number of records from a total set of records a certian depth in the records.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="filters">An ArrayList of SqlDbTableFields to be used to determine the parametrs of this query.</param>
		/// <param name="query_name">The name to be used for this stored procedure.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		/// <remarks>This version of the method use equal comparisons on filters.</remarks>
		public static string CreatePagedQuery(
			SqlConnectionSource conn,
			SqlDbTable tbl,
			ArrayList filters,
			string query_name,
			bool drop,
			bool run)
		{
			StringBuilder sb = new StringBuilder();
			string ret = "";
			string where = "WHERE\r\n";
			int atFld = 1;

			if(drop == true && run == true)
			{	//add drop if needed
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + query_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + query_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			//add create procedure
			AddStr(sb,"CREATE PROCEDURE " + query_name);
			AddCrLf(sb);
			
			//open param list
			AddStr(sb, "(");
			AddCrLf(sb);
			

			foreach(SqlDbTableField fld in filters)
			{
				AddStr(sb, "\t");
				AddParamName(sb, fld); //add parameter name 
				AddStr(sb, " " + fld.DataType); // add parameter type

                if (fld.DataType == "varchar" || fld.DataType == "nvarchar")
					AddStr(sb, "(" + fld.CharacterMaximumLength.ToString() + ")");

				AddStr(sb, ", \r\n");

				//build where clause
				where += "\t@" + fld.ColumnName + " = " + fld.ColumnName;

				if(atFld <= (filters.Count - 1))
					where += "\r\nAND";		//format for next where addition
				else
					where += "\r\n";		//format end of where clause

				atFld++;
			}

			//we need to these variables to page data
			AddStr(sb, "\t@PageIndex int,\r\n\t@NumResults int\r\n");

			//close the parameter list
			AddStr(sb, ")");
			AddCrLf(sb);

			AddStr(sb, "AS");
			AddCrLf(sb); //begin procedure

			//create a temp table to store data for paging 
			AddStr(sb, "DECLARE @RETSET TABLE\r\n(\r\n");
			AddStr(sb, "\tRecCountID int IDENTITY,\r\n");//count the records
			
			string col_list = ""; //store a list of columns for use in multi places
			if(null != tbl.Columns)
			{
				atFld = 1; //start from 1
				foreach(SqlDbTableField f in tbl.Columns)
				{
					AddStr(sb, "\t"); //format
					AddStr(sb, f.ColumnName + " " + f.DataType); //add column to table var

                    if (f.DataType == "varchar" || f.DataType == "nvarchar")
						AddStr(sb, "(" + f.CharacterMaximumLength.ToString() + ")");

					col_list += "\t" + f.ColumnName; //add column to column select list

					if(atFld < (tbl.Columns.Count))
					{
						col_list += ",\r\n";	//apply format for on going list
						AddStr(sb, ",\r\n");	//..
					}
					else
					{
						col_list += "\r\n";		//end of list format
						AddStr(sb, "\r\n");		//..
					}
					atFld++; //keep count of field
				}
			}

			//fill the table variable with data
			AddStr(sb, ")\r\nINSERT INTO @RETSET\r\n");
			AddStr(sb, "(\r\n" + col_list + ")\r\n"); //use our cached column list
			AddStr(sb, "SELECT");
			AddCrLf(sb);
		
			AddStr(sb, col_list); //use our cached column list
			AddStr(sb, "FROM");	//where is the data at again
			AddCrLf(sb);

			AddStr(sb, "\t" + tbl.TableName); //oh in the table
			AddCrLf(sb);

			if(filters.Count > 0) //if there is a where add it
				AddStr(sb, where);

			AddCrLf(sb);
			AddCrLf(sb);

			//return the requested page of data from the table variable
			AddStr(sb, "SELECT\r\n" + col_list + "FROM\r\n\t@RETSET AS tblRet\r\nWHERE \r\n\ttblRet.RecCountID >= (((@PageIndex * @NumResults) - @NumResults) + 1) \r\nAND \r\n\ttblRet.RecCountID <= (@PageIndex * @NumResults)");

			AddStr(sb, "\r\n\r\nRETURN");	//return

			ret = sb.ToString();

			if(run == true) //execute if needed
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);
            
			return ret;
		}


		/// <summary>
		/// Returns the SQL used to create a stored procedure for retriving a certian number of records from a total set of records a certian depth in the records.
		/// </summary>
		/// <param name="conn">The SqlConnectionSource that will be used to run the SQL if run is set true.</param>
		/// <param name="tbl">The SqlDbTable that the procedure will be bassed on.</param>
		/// <param name="filters">An QueryFilterList of QueryFilters to be used to determine the parametrs of this query.</param>
		/// <param name="query_name">The name to be used for this stored procedure.</param>
		/// <param name="drop">Should the procedure be dropped? true/false ? Should always be true for best practice.</param>
		/// <param name="run">Should this SQL be run now.</param>
		/// <returns>SQL string.</returns>
		/// <remarks>This version of the method use equal comparisons on filters.</remarks>
		public static string CreatePagedQuery(
			SqlConnectionSource conn,
			SqlDbTable tbl,
			QueryFilterList filters,
			string query_name,
			bool drop,
			bool run)
		{
			StringBuilder sb = new StringBuilder();
			string ret = "";
			string where = "WHERE\r\n";
			int atFld = 1;

			if(drop == true && run == true)
			{	//add drop if needed
				string drop_sql = "IF((SELECT COUNT(*) FROM INFORMATION_SCHEMA.ROUTINES WHERE INFORMATION_SCHEMA.ROUTINES.ROUTINE_NAME = '" + query_name + "') > 0)\r\n";
				drop_sql += "\tDROP PROCEDURE " + query_name + "\r\n\r\n\r\n";
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(drop_sql);

			}

			//add create procedure
			AddStr(sb,"CREATE PROCEDURE " + query_name);
			AddCrLf(sb);
			
			//open param list
			AddStr(sb, "(");
			AddCrLf(sb);
			

			foreach(QueryFilter f  in filters)
			{
				SqlDbTableField fld = f.Field;

				AddStr(sb, "\t");
				AddParamName(sb, fld); //add parameter name 

                if (fld.DataType == "varchar" || fld.DataType == "nvarchar")
					AddStr(sb, "(" + fld.CharacterMaximumLength.ToString() + ")");

				AddStr(sb, " " + fld.DataType); // add parameter type

				AddStr(sb, ", \r\n");

				//build where clause
				where += "\t" + f.FilterString();

				if(atFld <= (filters.Count - 1))
					where += "\r\nAND";		//format for next where addition
				else
					where += "\r\n";		//format end of where clause

				atFld++;
			}

			//we need to these variables to page data
			AddStr(sb, "\t@PageIndex int,\r\n\t@NumResults int\r\n");

			//close the parameter list
			AddStr(sb, ")");
			AddCrLf(sb);

			AddStr(sb, "AS");
			AddCrLf(sb); //begin procedure

			//create a temp table to store data for paging 
			AddStr(sb, "DECLARE @RETSET TABLE\r\n(\r\n");
			AddStr(sb, "\tRecCountID int IDENTITY,\r\n");//count the records
			
			string col_list = ""; //store a list of columns for use in multi places
			if(null != tbl.Columns)
			{
				atFld = 1; //start from 1
				foreach(SqlDbTableField f in tbl.Columns)
				{
					AddStr(sb, "\t"); //format
					AddStr(sb, f.ColumnName + " " + f.DataType); //add column to table var

                    if (f.DataType == "varchar" || f.DataType == "nvarchar")
						AddStr(sb, "(" + f.CharacterMaximumLength.ToString() + ")");
					
					col_list += "\t" + f.ColumnName; //add column to column select list

					if(atFld < (tbl.Columns.Count))
					{
						col_list += ",\r\n";	//apply format for on going list
						AddStr(sb, ",\r\n");	//..
					}
					else
					{
						col_list += "\r\n";		//end of list format
						AddStr(sb, "\r\n");		//..
					}
					atFld++; //keep count of field
				}
			}

			//fill the table variable with data
			AddStr(sb, ")\r\nINSERT INTO @RETSET\r\n");
			AddStr(sb, "(\r\n" + col_list + ")\r\n"); //use our cached column list
			AddStr(sb, "SELECT");
			AddCrLf(sb);
		
			AddStr(sb, col_list); //use our cached column list
			AddStr(sb, "FROM");	//where is the data at again
			AddCrLf(sb);

			AddStr(sb, "\t" + tbl.TableName); //oh in the table
			AddCrLf(sb);

			if(filters.Count > 0) //if there is a where add it
				AddStr(sb, where);

			AddCrLf(sb);
			AddCrLf(sb);

			//return the requested page of data from the table variable
			AddStr(sb, "SELECT\r\n" + col_list + "FROM\r\n\t@RETSET AS tblRet\r\nWHERE \r\n\ttblRet.RecCountID >= (((@PageIndex * @NumResults) - @NumResults) + 1) \r\nAND \r\n\ttblRet.RecCountID <= (@PageIndex * @NumResults)");

			AddStr(sb, "\r\n\r\nRETURN");	//return

			ret = sb.ToString();

			if(run == true) //execute if needed
				conn.DbAccessor.ExecuteSql.ExecuteNonQuery(ret);
            
			return ret;
		}

		#endregion

	}
}
