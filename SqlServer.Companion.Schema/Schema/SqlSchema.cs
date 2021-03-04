using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;

using SqlServer.Companion.Base;

#region Enumerations
namespace SqlServer.Companion
{
	public enum LoadType
	{
		/// <summary>
		/// Fast, but heavy.
		/// </summary>
		Complete,
		/// <summary>
		/// Slow, but lite.
		/// </summary>
		Lazy
	}
	public enum RoutineType
	{
		/// <summary>
		/// A function.
		/// </summary>
		Function,
		/// <summary>
		/// A Stored Procedure.
		/// </summary>
		Procedure
	}
}
#endregion
#region Schema
namespace SqlServer.Companion.Schema
{
	/// <summary>Provides methods for reading the SqlServer INFORMATION_SHEMA to query database structure metadata.</summary>
	public sealed class SqlSchema
	{
		#region Cache
		//could add cache here for each type
		//cache would need public static properties
		//to control refresh timeout
		//and....
		//
		//for conviance a type accessor can be added for each type.
		/// <summary>Provide static methds to cache Schema objects.</summary>
		/* public sealed class Chache
		{
			static SqlSchemaCache cache = new SqlSchemaCache();
			/// <summary>
			/// Add a cache item.
			/// </summary>
			/// <param name="key">Cache key.</param>
			/// <param name="value">ISqlSchemaObject</param>
			internal static void Add(object key, ISqlSchemaObject value)
			{
				cache.Add(value.GetType(), key, (ISqlSchemaObject)value.Clone());
			}
			internal static ISqlSchemaObject Find(Type cached_type, object key)
			{
				return cache.Items(cached_type, key);
			}
			//implement when you can think of a good way to implement object level
			//experasion do not implement before then 
			//with out implementing a public Clear() and Clear(Type cached_type)
			//
			//also insure that clone is implemented properly on each object
			//
			//object existance length in place, without some form of threaded
			//monitior or a timer the Purge method will have to be called
			//in order to clear stale items
			//it should be implemented on a per type level to lessen
			//execution cost
		} */
		#endregion
		#region DB
		/// <summary>
		/// Establish a DB structure connection.
		/// </summary>
		/// <param name="conn">ConnectionSource to be used to access the SqlServer databse.</param>
		/// <param name="type">Complete: the entire DB structure is loaded in the initial hit. Lazy: The structure is loaded as it is used.</param>
		/// <returns>A SqlDb.</returns>
		public static SqlDb FindDb(SqlConnectionSource conn, LoadType type)
		{
			SqlDb db = SqlDb.Build(conn.Db, (SqlConnectionSource)conn.Clone());
			if(type == LoadType.Complete)
			{
				Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
				string sql = string.Empty;
				SqlDataReader r = null;
				#region DB HIT
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.Db.FindDb.sql");

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
					#region Tables
					SqlDbTableField field = null;
					Hashtable table_col = new Hashtable();
					SqlDbTableCollection tables = new SqlDbTableCollection();

					//read tables
					tables = SqlDbTable.BuildCollection(r, (SqlConnectionSource)conn.Clone());

					r.NextResult(); //move to table columns

					while(r.Read())
					{
						field = SqlDbTableField.Build(r, (SqlConnectionSource)conn.Clone());
						if(!table_col.Contains(field.TableName))//if it isn't there add the column colection
							table_col.Add(field.TableName, new SqlDbTableFieldCollection());
						
						((SqlDbTableFieldCollection)table_col[field.TableName]).Add((SqlDbTableField)field.Clone());
					}

					//next attach the columns to the tables
					for(int i = 0; i < tables.Count; i++)
					{
						if(table_col.Contains(tables[i].TableName))
						{
							SqlDbTableFieldCollection col = table_col[tables[i].TableName] as SqlDbTableFieldCollection;
							SqlDbTable.Attach(tables[i], col);
						}
					}
					#endregion
					#region Views
					SqlDbViewField vfield = null;
					Hashtable view_col = new Hashtable();
					SqlDbViewCollection views = new SqlDbViewCollection();

					r.NextResult();//move to the views

					views = SqlDbView.BuildCollection(r, (SqlConnectionSource)conn.Clone());

					r.NextResult(); //move to theview columns

					while(r.Read())
					{
						vfield = SqlDbViewField.Build(r, (SqlConnectionSource)conn.Clone());
						if(!view_col.Contains(vfield.ViewName))//if it isn't there add the column colection
							view_col.Add(vfield.ViewName, new SqlDbViewFieldCollection());
						
						((SqlDbViewFieldCollection)view_col[vfield.ViewName]).Add((SqlDbViewField)vfield.Clone());
					}

					//next attach the columns to the views
					for(int i = 0; i < views.Count; i++)
					{
						if(view_col.Contains(views[i].TableName))
						{
							SqlDbViewFieldCollection col = view_col[views[i].TableName] as SqlDbViewFieldCollection;
							SqlDbView.Attach(views[i], col);
						}
					}
					#endregion
					#region Routines
					SqlDbRoutineField rfield = null;
					SqlDbRoutineParameter rparam = null;
					Hashtable r_col = new Hashtable();
					Hashtable r_param = new Hashtable();
					SqlDbRoutineCollection routines = new SqlDbRoutineCollection();
					r.NextResult();//move to the routines

					routines = SqlDbRoutine.BuildCollection(r, (SqlConnectionSource)conn.Clone());

					r.NextResult();//move to routine parameters 

					while(r.Read())
					{
						rparam = SqlDbRoutineParameter.Build(r, (SqlConnectionSource)conn.Clone());
						if(!r_param.Contains(rparam.SpecificName))
							r_param.Add(rparam.SpecificName, new SqlDbRoutineParameterCollection());

						((SqlDbRoutineParameterCollection)r_param[rparam.SpecificName]).Add((SqlDbRoutineParameter)r_param.Clone());
					}

					//next attach the parameters
					for(int i = 0; i < routines.Count; i++)
					{
						if(r_param.Contains(routines[i].SpecificName))
						{
							SqlDbRoutineParameterCollection param = r_param[routines[i].SpecificName] as SqlDbRoutineParameterCollection;
							SqlDbRoutine.Attach(routines[i], param);
						}
					}

					r.NextResult();//move to the fields

					while(r.Read())
					{
						rfield = SqlDbRoutineField.Build(r, (SqlConnectionSource)conn.Clone());
						if(!r_col.Contains(rfield.TableName))
							r_col.Add(rfield.TableName, new SqlDbRoutineFieldCollection());

						((SqlDbRoutineFieldCollection)r_col[rfield.TableName]).Add((SqlDbRoutineField)rfield.Clone());
					}

					//next attach the columns er fields
					for(int i = 0; i < routines.Count; i++)
					{
						if(r_col.Contains(routines[i].SpecificName))
						{
							SqlDbRoutineFieldCollection col = r_col[routines[i].SpecificName] as SqlDbRoutineFieldCollection;
							SqlDbRoutine.Attach(routines[i], col);
						}
					}
					#endregion
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}
				#endregion
			}
			return db;
		}
		#endregion
		#region Check Constraint
		/// <summary>
		/// Find a single database check constraint by name.
		/// </summary>
		/// <param name="conn">ConnectionSource to be used to access the SqlServer databse.</param>
		/// <param name="constraint">The constraint name.</param>
		/// <returns>A SqlDbCheckConstraint.</returns>
		public static SqlDbCheckConstraint FindDbCheckConstraint(SqlConnectionSource conn, string constraint)
		{
			SqlDbCheckConstraint cc = null;
			
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.CONSTRAINT.FindCheckConstraint.sql");
					sql = sql.Replace("@ConstraintName", constraint);
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					cc = SqlDbCheckConstraint.Build(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}
			
			return cc;
		}
		/// <summary>
		/// Find all Check Constraints for the current database connection.
		/// </summary>
		/// <param name="conn">Datasoure to provide SqlServer conectivity.</param>
		/// <returns>A SqlDbCheckConstraintCollection.</returns>
		public static SqlDbCheckConstraintCollection FindDbCheckConstraints(SqlConnectionSource conn)
		{
			SqlDbCheckConstraintCollection ccc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.CONSTRAINT.FindCheckConstraints.sql");
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				ccc = SqlDbCheckConstraint.BuildCollection(r, (SqlConnectionSource)conn.Clone());

			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}

			return ccc;
		}
		#endregion
		#region Referential Constraint
		/// <summary>
		/// Finds all referential constraints that matches the given critiria.
		/// </summary>
		/// <param name="conn">Datasoure to provide SqlServer conectivity.</param>
		/// <param name="constraint">The constraint name.</param>
		/// <returns>A SqlDbReferentialConstraint.</returns>
		public static SqlDbReferentialConstraint FindRefererentialConstraint(SqlConnectionSource conn, string constraint)
		{
			SqlDbReferentialConstraint rc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.CONSTRAINT.FindReferentialConstraintByConstraint.sql");
				sql = sql.Replace("@ConstraintName", constraint);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rc = SqlDbReferentialConstraint.Build(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}		
	
			return rc;
		}
		/// <summary>
		/// Finds the first referential constraint that matches the given critiria.
		/// </summary>
		/// <param name="conn">Datasoure to provide SqlServer conectivity.</param>
		/// <param name="constraint">The constraint name.</param>
		/// <param name="unique_constraint">The unique constraint name.</param>
		/// <returns>A SqlDbReferentialConstraint.</returns>
		public static SqlDbReferentialConstraint FindRefererentialConstraint(SqlConnectionSource conn, string constraint, string unique_constraint)
		{
			SqlDbReferentialConstraint rc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.CONSTRAINT.FindReferentialConstraint.sql");
				sql = sql.Replace("@ConstraintName", constraint);
				sql = sql.Replace("@UniqueConstraintName", unique_constraint);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rc = SqlDbReferentialConstraint.Build(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}		
	
			return rc;
		}
		/// <summary>
		/// Finds the referential constraints matching the in pass unigue constraint name.
		/// </summary>
		/// <param name="conn">Datasoure to provide SqlServer conectivity.</param>
		/// <param name="unique_constraint">The unique constraint name.</param>
		/// <returns>A SqlDbReferentialConstraintCollection.</returns>
		public static SqlDbReferentialConstraintCollection FindRefererentialConstraints(SqlConnectionSource conn, string unique_constraint)
		{
			SqlDbReferentialConstraintCollection rcc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.CONSTRAINT.FindReferentialConstraints.sql");
				sql = sql.Replace("@UniqueConstraintName", unique_constraint);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rcc = SqlDbReferentialConstraint.BuildCollection(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return rcc;
		}
		/// <summary>
		/// Finds All referential constraints.
		/// </summary>
		/// <param name="conn">Datasoure to provide SqlServer conectivity.</param>
		/// <returns>A SqlDbReferentialConstraintCollection.</returns>
		public static SqlDbReferentialConstraintCollection FindRefererentialConstraints(SqlConnectionSource conn)
		{
			SqlDbReferentialConstraintCollection rcc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.CONSTRAINT.FindReferentialConstraintsGlobal.sql");

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rcc = SqlDbReferentialConstraint.BuildCollection(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return rcc;
		}
		#endregion
		#region Routine
		/// <summary>
		/// Finds a specific routine.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="routine">The name of a routine</param>
		/// <param name="type">Load Complete/Lazy. </param>
		/// <returns>A SqlDbRoutine.</returns>
		public static SqlDbRoutine FindRoutine(SqlConnectionSource conn, string routine, LoadType type)
		{
			SqlDbRoutine rt = new SqlDbRoutine();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;

			if(type == LoadType.Complete)
			{
				throw new Exception("Not implemented. Use Lazy Load.");
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.FindRoutine.sql");
					sql = sql.Replace("@RoutineName", routine);

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
					if(r.Read())
						rt = SqlDbRoutine.Build(r, (SqlConnectionSource)conn.Clone());
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}			
			}

			return rt;
		}

		/// <summary>
		/// Finds a specific routine.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="routine">The name of a routine</param>
		/// <param name="routine_type">Specifies the type of routine wanted.</param>
		/// <param name="type">Load Complete/Lazy. </param>
		/// <returns>A SqlDbRoutine.</returns>
		public static SqlDbRoutine FindRoutine(SqlConnectionSource conn,string routine, RoutineType routine_type, LoadType type)
		{
			SqlDbRoutine rt = new SqlDbRoutine();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;

			if(type == LoadType.Complete)
			{
				throw new Exception("Not implemented. Use Lazy Load.");
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.FindRoutine.sql");
					sql = sql.Replace("@RoutineName", routine);

					if(routine_type == RoutineType.Function)
						sql += " AND tblRoutine.ROUTINE_TYPE = 'FUNCTION' ";
					else
						sql += " AND tblRoutine.ROUTINE_TYPE = 'PROCEDURE' ";

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
					if(r.Read())
						rt = SqlDbRoutine.Build(r, (SqlConnectionSource)conn.Clone());
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}			
			}

			return rt;
		}

		/// <summary>
		/// Find all routines of every type.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="type">Load Complete/Lazy. </param>
		/// <returns>A SqlDbRoutineCollection.</returns>
		public static SqlDbRoutineCollection FindRoutines(SqlConnectionSource conn, LoadType type)
		{
			SqlDbRoutineCollection rtc = new SqlDbRoutineCollection();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;

			if(type == LoadType.Complete)
			{
				throw new Exception("Not implemented. Use Lazy Load.");
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.FindRoutines.sql");

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
					if(r.Read())
						rtc = SqlDbRoutine.BuildCollection(r, (SqlConnectionSource)conn.Clone());
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}			
			}
			return rtc;
		}
		#region Routine Field
		/// <summary>
		/// Finds a specif routine field.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="routine">The name of the routine.</param>
		/// <param name="field">The name of the field.</param>
		/// <returns>A .SqlDbRoutineField</returns>
		public static SqlDbRoutineField FindRoutineField(SqlConnectionSource conn, string routine, string field)
		{
			SqlDbRoutineField rf = new SqlDbRoutineField();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.COLUMN.FindRoutineColumn.sql");
				sql = sql.Replace("@RoutineName", routine);
				sql = sql.Replace("@ColumnName", field);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rf = SqlDbRoutineField.Build(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return rf;
		}
		/// <summary>
		/// Finds all the fields a table valued routine returns per row.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="routine">The name of the routine.</param>
		/// <returns>A SqlDbRoutineFieldCollection.</returns>
		public static SqlDbRoutineFieldCollection FindRoutineFields(SqlConnectionSource conn, string routine)
		{
			SqlDbRoutineFieldCollection rfs = new SqlDbRoutineFieldCollection();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.COLUMN.FindRoutineColumns.sql");
				sql = sql.Replace("@RoutineName", routine);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rfs = SqlDbRoutineField.BuildCollection(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return rfs;
		}
		#endregion
		#region Routine Parameter
		/// <summary>
		/// Finds one specific routine parameter.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="routine">The name of the routine.</param>
		/// <param name="parameter">The name of the parameter.</param>
		/// <returns>A SqlDbRoutineParameter.</returns>
		public static SqlDbRoutineParameter FindRoutineParameter(SqlConnectionSource conn, string routine, string parameter)
		{
			SqlDbRoutineParameter rp = new SqlDbRoutineParameter();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.PARAMETER.FindRoutineParameter.sql");
				sql = sql.Replace("@RoutineName", routine);
				sql = sql.Replace("@ParamName", parameter);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rp = SqlDbRoutineParameter.Build(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return rp;
		}
		/// <summary>
		/// Loads a collection of parameters a routine takes in.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="routine">The name of the routine.</param>
		/// <returns>A SqlDbRoutineParameterCollection.</returns>
		public static SqlDbRoutineParameterCollection FindRoutineParameters(SqlConnectionSource conn, string routine)
		{
			SqlDbRoutineParameterCollection rps = new SqlDbRoutineParameterCollection();
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.ROUTINE.PARAMETER.FindRoutineParameter.sql");
				sql = sql.Replace("@RoutineName", routine);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					rps = SqlDbRoutineParameter.BuildCollection(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return rps;
		}
		#endregion
		#endregion
		#region Table
		/// <summary>
		/// Finds a specific table.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table/</param>
		/// <param name="type">Load all or load least.</param>
		/// <returns>A SqlDbTable.</returns>
		public static SqlDbTable FindTable(SqlConnectionSource conn, string table, LoadType type)
		{
			SqlDbTable tbl = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;
			#region Hit
			if(type == LoadType.Complete)
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.FindTable.Complete.sql");
				sql = sql.Replace("@TableName", table);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				#region Tables
				SqlDbTableFieldCollection table_col = new SqlDbTableFieldCollection();

				
				if(r.Read())
				{	//read table
					tbl = SqlDbTable.Build(r, (SqlConnectionSource)conn.Clone());
					r.NextResult(); //move to table columns and read them
					table_col = SqlDbTableField.BuildCollection(r, (SqlConnectionSource)conn.Clone());
					tbl = SqlDbTable.Attach(tbl, table_col);//attach the columns to the table
				}
				#endregion
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.FindTable.sql");
					sql = sql.Replace("@TableName", table);

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

					if(r.Read())
						tbl = SqlDbTable.Build(r, conn);
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}			
			}
			#endregion
			return tbl;
		}
		/// <summary>
		/// Find all tables.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="type">Load all or load least.</param>
		/// <returns>A SqlDbTableCollection.</returns>
		public static SqlDbTableCollection FindTables(SqlConnectionSource conn, LoadType type)
		{
			SqlDbTableCollection tbls = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;
			

			if(type == LoadType.Complete)
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.FindTables.Complete.sql");

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				#region Tables
				SqlDbTableField field = null;
				Hashtable table_col = new Hashtable();
				SqlDbTableCollection tables = new SqlDbTableCollection();

				//read tables
				tables = SqlDbTable.BuildCollection(r, (SqlConnectionSource)conn.Clone());

				r.NextResult(); //move to table columns

				while(r.Read())
				{
					field = SqlDbTableField.Build(r, (SqlConnectionSource)conn.Clone());
					if(!table_col.Contains(field.TableName))//if it isn't there add the column colection
						table_col.Add(field.TableName, new SqlDbTableFieldCollection());
						
					((SqlDbTableFieldCollection)table_col[field.TableName]).Add((SqlDbTableField)field.Clone());
				}

				//next attach the columns to the tables
				for(int i = 0; i < tables.Count; i++)
				{
					if(table_col.Contains(tables[i].TableName))
					{
						SqlDbTableFieldCollection col = table_col[tables[i].TableName] as SqlDbTableFieldCollection;
						SqlDbTable.Attach(tables[i], col);
					}
				}
				#endregion
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.FindTables.sql");

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

					if(r.Read())
						tbls = SqlDbTable.BuildCollection(r, conn);
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}			
			}
			return tbls;
		}
		#region Constraints
		/// <summary>
		/// Finds a specific table constraint.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <param name="constraint">The name of the constraint.</param>
		/// <returns>A SqlDbTableConstraint.</returns>
		public static SqlDbTableConstraint FindTableConstraint(SqlConnectionSource conn, string table, string constraint)
		{
			SqlDbTableConstraint tc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.CONSTRAINT.FindTableCheckConstraint.sql");
				sql = sql.Replace("@TableName", table);
				sql = sql.Replace("@ConstraintName", constraint);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tc = SqlDbTableConstraint.Build(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tc;
		}
		/// <summary>
		/// Find all a tables constraints
		/// </summary>
		/// <param name="conn">Connection Source.</param>
		/// <param name="table">The name of the table.</param>
		/// <returns>A SqlDbTableConstraintCollection.</returns>
		public static SqlDbTableConstraintCollection FindTableConstraints(SqlConnectionSource conn, string table)
		{
			SqlDbTableConstraintCollection tcc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.CONSTRAINT.FindTableCheckConstraints.sql");
				sql = sql.Replace("@TableName", table);
				
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tcc = SqlDbTableConstraint.BuildCollection(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tcc;
		}
		#endregion
		#region Privileges
		/// <summary>
		/// Find a collection of privileges for a table.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <returns>A SqlDbTablePrivilegeCollection.</returns>
		public static SqlDbTablePrivilegeCollection FindTablePrivileges(SqlConnectionSource conn, string table)
		{
			SqlDbTablePrivilegeCollection tpc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.PRIVILEGE.FindTablePrivileges.sql");
				sql = sql.Replace("@TableName", table);
				
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tpc = SqlDbTablePrivilege.BuildCollection(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tpc;
		}
		#endregion
		#region Table Field
		/// <summary>
		/// Find a table field.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <param name="field">The name of the field.</param>
		/// <returns>A SqlDbTableField.</returns>
		public static SqlDbTableField FindTableField(SqlConnectionSource conn, string table, string field)
		{
			SqlDbTableField fld = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;
		
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.COLUMN.FindTableColumn.sql");
				sql = sql.Replace("@TableName", table);
				sql = sql.Replace("@ColumnName", field);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					fld = SqlDbTableField.Build(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			
			return fld;
		}
		/// <summary>
		/// Find all table fields.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <returns>A SqlDbTableFieldCollection.</returns>
		public static SqlDbTableFieldCollection FindTableFields(SqlConnectionSource conn, string table)
		{
			SqlDbTableFieldCollection flds = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.COLUMN.FindTableColumns.sql");
				sql = sql.Replace("@TableName", table);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					flds = SqlDbTableField.BuildCollection(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			
			return flds;
		}
		#region Constraints
		/// <summary>
		/// Find a specific constraint.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="constraint">The name of the constraint.</param>
		/// <returns>A SqlDbTableFieldConstraint.</returns>
		public static SqlDbTableFieldConstraint FindTableFieldConstraint(SqlConnectionSource conn, string constraint)
		{
			SqlDbTableFieldConstraint tc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.COLUMN.CONSTRAINT.FindConstraintColumnUsageByConstraint.sql");
				sql = sql.Replace("@ConstraintName", constraint);
				
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tc = SqlDbTableFieldConstraint.Build(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tc;
		}
		/// <summary>
		/// Find a specific constraint.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <param name="field">The name of the field.</param>
		/// <param name="constraint">The name of the constraint.</param>
		/// <returns>A SqlDbTableFieldConstraint.</returns>
		public static SqlDbTableFieldConstraint FindTableFieldConstraint(SqlConnectionSource conn, string table, string field, string constraint)
		{
			SqlDbTableFieldConstraint tc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.COLUMN.CONSTRAINT.FindConstraintColumnUsage.sql");
				sql = sql.Replace("@TableName", table);
				sql = sql.Replace("@ColumnName", field);
				sql = sql.Replace("@ConstraintName", constraint);
				
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tc = SqlDbTableFieldConstraint.Build(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tc;
		}
		/// <summary>
		/// Find constraints.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <param name="field">The name of the field.</param>
		/// <returns>A SqlDbTableFieldConstraintCollection.</returns>
		public static SqlDbTableFieldConstraintCollection FindTableFieldConstraints(SqlConnectionSource conn, string table, string field)
		{
			SqlDbTableFieldConstraintCollection tcc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.COLUMN.CONSTRAINT.FindConstraintColumnUsages.sql");
				sql = sql.Replace("@TableName", table);
				sql = sql.Replace("@ColumnName", field);
				
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tcc = SqlDbTableFieldConstraint.BuildCollection(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tcc;
		}
		#endregion
		#region Privileges
		/// <summary>
		/// Find field level privleges.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="table">The name of the table.</param>
		/// <param name="field">The name of the field.</param>
		/// <returns>A SqlDbTableFieldPrivilegeCollection.</returns>
		public static SqlDbTableFieldPrivilegeCollection FindTableFieldPrivileges(SqlConnectionSource conn, string table, string field)
		{
			SqlDbTableFieldPrivilegeCollection tpc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			string sql = string.Empty;
			SqlDataReader r = null;

			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.TABLE.COLUMN.CONSTRAINT.FindConstraintColumnUsages.sql");
				sql = sql.Replace("@TableName", table);
				sql = sql.Replace("@ColumnName", field);
				
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				if(r.Read())
					tpc = SqlDbTableFieldPrivilege.BuildCollection(r, conn);
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}			

			return tpc;
		}
		#endregion
		#endregion
		#endregion
		#region View
		/// <summary>
		/// Find a view by name.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="view">The name of the view.</param>
		/// <param name="type">Complete or Lazy.</param>
		/// <returns>A SqlDbView.</returns>
		public static SqlDbView FindView(SqlConnectionSource conn, string view, LoadType type)
		{
			SqlDbView tbl = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			if(type == LoadType.Complete)
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.VIEW.FindView.Complete.sql");
				sql = sql.Replace("@ViewName", view);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				#region Views
				SqlDbViewFieldCollection view_col = new SqlDbViewFieldCollection();

				
				if(r.Read())
				{	//read view
					tbl = SqlDbView.Build(r, (SqlConnectionSource)conn.Clone());
					r.NextResult(); //move to view columns and read them
					view_col = SqlDbViewField.BuildCollection(r, (SqlConnectionSource)conn.Clone());
					tbl = SqlDbView.Attach(tbl, view_col);//attach the columns to the view
				}
				#endregion
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.VIEW.FindView.sql");
					sql = sql.Replace("@ViewName", view);

					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
					if(r.Read())
						tbl = SqlDbView.Build(r, (SqlConnectionSource)conn.Clone());
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}
			}
			return tbl;
		}
		/// <summary>
		/// Find all views.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="type">Complete or Lazy.</param>
		/// <returns>A SqlDbViewCollection.</returns>
		public static SqlDbViewCollection FindViews(SqlConnectionSource conn, LoadType type)
		{
			SqlDbViewCollection vc = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			if(type == LoadType.Complete)
			{
				SqlDbViewField vfield = null;
				Hashtable view_col = new Hashtable();
				SqlDbViewCollection views = new SqlDbViewCollection();

				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.VIEW.FindViews.Complete.sql");
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);

				views = SqlDbView.BuildCollection(r, (SqlConnectionSource)conn.Clone());

				r.NextResult(); //move to the view columns

				while(r.Read())
				{
					vfield = SqlDbViewField.Build(r, (SqlConnectionSource)conn.Clone());
					if(!view_col.Contains(vfield.ViewName))//if it isn't there add the column colection
						view_col.Add(vfield.ViewName, new SqlDbViewFieldCollection());
						
					((SqlDbViewFieldCollection)view_col[vfield.ViewName]).Add((SqlDbViewField)vfield.Clone());
				}

				//next attach the columns to the views
				for(int i = 0; i < views.Count; i++)
				{
					if(view_col.Contains(views[i].TableName))
					{
						SqlDbViewFieldCollection col = view_col[views[i].TableName] as SqlDbViewFieldCollection;
						SqlDbView.Attach(views[i], col);
					}
				}
			}
			else if(type == LoadType.Lazy)
			{
				try
				{
					sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.VIEW.FindViews.sql");
					r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
					if(r.Read())
						vc = SqlDbView.BuildCollection(r, (SqlConnectionSource)conn.Clone());
				}
				catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
				finally
				{
					if(null != r)
					{
						r.Close();
						r = null;
					}
				}
			
			}
			return vc;
		}
		#region View Field
		/// <summary>
		/// Find a view field.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="view">The name of the view.</param>
		/// <param name="field">The name of the field.</param>
		/// <returns>A SqlDbViewField.</returns>
		public static SqlDbViewField FindViewField(SqlConnectionSource conn, string view, string field)
		{
			SqlDbViewField fld = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.VIEW.COLUMN.FindViewField.sql");
				sql = sql.Replace("@ViewName", view);
				sql = sql.Replace("@ColumnName", field);

				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
				
				if(r.Read())
					fld = SqlDbViewField.Build(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}
			
			return fld;
		}
		/// <summary>
		/// Find all view fields.
		/// </summary>
		/// <param name="conn">Connection source.</param>
		/// <param name="view">The name of the view.</param>
		/// <returns>A SqlDbViewFieldCollection.</returns>
		public static SqlDbViewFieldCollection FindViewFields(SqlConnectionSource conn, string view)
		{
			SqlDbViewFieldCollection flds = null;
			Resx.Reader rx = new SqlServer.Companion.Resx.Reader();
			
			string sql = string.Empty;
			SqlDataReader r = null;
			
			try
			{
				sql = rx.ResourceString("SqlServer.Companion.Resx.INFORMATION_SCHEMA.Sql.DB.VIEW.COLUMN.FindViewFields.sql");
				sql = sql.Replace("@ViewName", view);
				r = conn.DbAccessor.ExecuteSql.ExecuteReader(sql);
			
				if(r.Read())
					flds = SqlDbViewField.BuildCollection(r, (SqlConnectionSource)conn.Clone());
			}
			catch(Exception ex){System.Diagnostics.Debug.WriteLine(ex.Message);}
			finally
			{
				if(null != r)
				{
					r.Close();
					r = null;
				}
			}
			


			return flds;
		}
		#endregion
		#endregion
	}
}
#endregion