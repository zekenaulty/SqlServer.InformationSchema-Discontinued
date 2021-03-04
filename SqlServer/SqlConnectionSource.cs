using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;

namespace SqlServer.Companion
{
	#region SqlConnectionSource
	/// <summary>Represent a SqlSerer Connection String and provides common/special case execution methods.</summary>
	/// <remarks>
	/// The SqlServer.Companion.SqlConnectionSource provides a higher level of control over an SqlServer ConnectionString. 
	/// As well as encapsulating a set of Stored Proedure and SQL execution blocks simular to SqlHelper and 
	/// the Microsoft SqlServer Application Block. The SQL execution blocks are losely based on a set of VB.Net Modules
	/// I created in 2002. The genral intention of this object and it's child objects is to provide an Object Orianted Modal 
	/// for reusable SqlServer Database access and manipulation.
	/// 
	/// All exceptions are allowed to pass up unless the error is expected and will not effet user excecution.
	/// </remarks>
	/// <ToDo>Add persistant connection without transaction use...save resources for complex multi 
	/// method call database quries.Can be used in conjunction with transaction as well...
	/// </ToDo>
	[Serializable]
	public sealed class SqlConnectionSource : ICloneable
	{
		#region Nested Class: TransactionBundle
		/// <summary>
		/// Provides a persistant connection for ease of connection management. Also provides a Transaction ready for use.
		/// </summary>
		public sealed class TransactionBundle
		{
			#region Construction
			SqlConnectionSource c = null;
			/// <summary>
			/// Internal only. Creates a new ready to use TransactionBundle.
			/// </summary>
			/// <param name="conn">A SqlConnection.</param>
			/// <param name="iso">The IsolationLevel of the initial transaction.</param>
			internal TransactionBundle(SqlConnectionSource conn, IsolationLevel iso)
			{
				connection = conn.CreateConnection();
				trans = connection.BeginTransaction(iso);
			}
			#endregion
			#region Connection
			SqlConnection connection = null;
			/// <summary>
			/// SqlConnection.
			/// </summary>
			public SqlConnection Connection
			{
				get
				{
					return connection;
				}
			}
			#endregion
			#region Transaction
			SqlTransaction trans = null;
			/// <summary>
			/// SqlTransaction.
			/// </summary>
			public SqlTransaction Transaction
			{
				get{return trans;}
			}
			#endregion
			#region RenewTransaction
			/// <summary>
			/// Begins initializes Transaction to a fresh transaction. If the current Transaction is in use(!null) it will be cleaned by calling SqlConnectionSource.CleanTransaction(tran.Transaction).
			/// </summary>
			/// <param name="trans">The Transaction bundle that will be renewed.</param>
			/// <param name="iso">The IsolationLevel.</param>
			public void RenewTransaction(IsolationLevel iso)
			{
				if(trans != null)
					c.CleanTransaction(trans);

				trans = Connection.BeginTransaction(iso);
			}
			#endregion
		}
		#endregion
		#region Nested Class: SqlAccesor
		/// <summary> Provide a higher level execution of sql routines than SqlProcedure or SqlText alone.</summary>
		[Serializable]
		public sealed class SqlAccessor
		{
			#region Nested Class: SqlProcedure
			/// <summary>Provides methods to access and manipulate a SqlServer Database through Stored Procedures.</summary>
			[Serializable]
			public sealed class SqlProcedure
			{
				#region Construction
				SqlConnectionSource conn = null;
				public SqlProcedure(SqlConnectionSource connection){this.conn = connection;}
				#endregion
				#region Throw Error
				void ThorwGenProcErr(string procedure, System.Exception ex){Err.ThrowErr("An error occuried in SqlServer.SqlProc when trying to execute the following stored procedure: " + procedure + ". More detailed information has been stored in this System.Exceptions InnerException.", ex); }
				#endregion
				#region SqlHelper DiscoverParameters
				/*
				/// <summary>
				/// Resolve at run time the appropriate set of SqlParameters for a stored procedure
				/// </summary>
				/// <param name="connection">A valid SqlConnection object</param>
				/// <param name="spName">The name of the stored procedure</param>
				/// <param name="includeReturnValueParameter">Whether or not to include their return value parameter</param>
				/// <returns>The parameter array discovered.</returns>
				internal static SqlParameter[] DiscoverSpParameterSet(SqlConnection connection, string spName, bool includeReturnValueParameter)
				{
					if( connection == null ) throw new ArgumentNullException( "connection" );
					if( spName == null || spName.Length == 0 ) throw new ArgumentNullException( "spName" );
					SqlCommand cmd = new SqlCommand(spName, connection);
					cmd.CommandType = CommandType.StoredProcedure;

					connection.Open();
					SqlCommandBuilder.DeriveParameters(cmd);
					connection.Close();

					if (!includeReturnValueParameter) 
						cmd.Parameters.RemoveAt(0);

					SqlParameter[] discoveredParameters = new SqlParameter[cmd.Parameters.Count];
					cmd.Parameters.CopyTo(discoveredParameters, 0); 

					// Init the parameters with a DBNull value
					foreach (SqlParameter discoveredParameter in discoveredParameters)
						discoveredParameter.Value = DBNull.Value;

					return discoveredParameters;
				}*/
				#endregion
				#region ExecuteReader
				public SqlDataReader ExecuteReader(string procedure)
				{
					if(null == conn) return null;
					SqlDataReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, CommandType.StoredProcedure);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteReader(CommandBehavior.CloseConnection);
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}

					return ret;
				}

				public SqlDataReader ExecuteReader(string procedure, SqlParameter param)
				{
					if(null == conn) return null;
					SqlDataReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteReader(CommandBehavior.CloseConnection);
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}

					return ret;
				}
				public SqlDataReader ExecuteReader(string procedure, SqlParameter[] param)
				{
					if(null == conn) return null;
					SqlDataReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteReader(CommandBehavior.CloseConnection);
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}

					return ret;
				}
				#endregion
				#region ExecuteXmlReader
				public XmlReader ExecuteXmlReader(string procedure)
				{
					if(null == conn) return null;
					XmlReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, CommandType.StoredProcedure);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteXmlReader();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				public XmlReader ExecuteXmlReader(string procedure, SqlParameter param)
				{
					if(null == conn) return null;
					XmlReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteXmlReader();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}
					return ret;
				}
				public XmlReader ExecuteXmlReader(string procedure, SqlParameter[] param)
				{
					if(null == conn) return null;
					XmlReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteXmlReader();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				#endregion
				#region ExecuteScalar
				public object ExecuteScalar(string procedure)
				{
					if(null == conn) return null;
					object ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, CommandType.StoredProcedure);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteScalar();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				public object ExecuteScalar(string procedure, SqlParameter param)
				{
					if(null == conn) return null;
					object ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteScalar();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				public object ExecuteScalar(string procedure, SqlParameter[] param)
				{
					if(null == conn) return null;
					object ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteScalar();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				#endregion
				#region ExecuteNonQuery
				public void ExecuteNonQuery(string procedure)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, CommandType.StoredProcedure);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}
				public void ExecuteNonQuery(string procedure, SqlTransaction trans)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, CommandType.StoredProcedure, trans);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}
				public void ExecuteNonQuery(string procedure, SqlParameter param)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}
				public void ExecuteNonQuery(string procedure, SqlParameter param, SqlTransaction trans)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param, trans);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}
				public void ExecuteNonQuery(string procedure, SqlParameter[] param)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex)
					{
						ThorwGenProcErr(procedure, ex);
					}
					finally
					{
						if(null != cmd)
						{
							cmd.Connection.Close();
							cmd.Dispose();
							cmd = null;
						}
					}

				}
				public void ExecuteNonQuery(string procedure, SqlParameter[] param, SqlTransaction trans)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(procedure, param, trans);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}
				#endregion
				#region ExecuteDataSet
				/// <summary>
				/// Gets a DataSet Filled with the results of in passed procedure.
				/// </summary>
				/// <param name="procedure">Name of the nrocedure.</param>
				/// <returns>A DataSet or null.</returns>
				public DataSet ExecuteDataSet(string procedure)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(procedure, CommandType.StoredProcedure);

					try
					{

						if(da == null)
							return ret;

						da.Fill(ret);
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return null;
				}
				/// <summary>
				/// Gets a DataSet Filled with the results of in passed procedure.
				/// </summary>
				/// <param name="procedure">Name of the nrocedure.</param>
				/// <param name="param">Parameter to be used when execting.</param>
				/// <returns>A DataSet or null.</returns>
				public DataSet ExecuteDataSet(string procedure, SqlParameter param)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(procedure, param);

					try
					{

						if(da == null)
							return ret;

						da.Fill(ret);
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return ret;
				}
				/// <summary>
				/// Gets a DataSet Filled with the results of in passed procedure.
				/// </summary>
				/// <param name="procedure">Name of the nrocedure.</param>
				/// <param name="param">Parameters to be used when execting.</param>
				/// <returns>A DataSet or null.</returns>
				public DataSet ExecuteDataSet(string procedure, SqlParameter[] param)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(procedure, param);

					try
					{

						if(da == null)
							return ret;

						da.Fill(ret);
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return ret;
				}
				#endregion
				#region ExecuteDataTable
				/// <summary>
				/// Gets a DataTable Filled with the results of in passed procedure.
				/// </summary>
				/// <param name="procedure">Name of the nrocedure.</param>
				/// <returns>A DataTable or null.</returns>
				public DataTable ExecuteDataTable(string procedure)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(procedure, CommandType.StoredProcedure);

					try
					{

						if(da == null)
							return null;

						da.Fill(ret);
					
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;

						if(ret.Tables.Count > 0)
							return ret.Tables[0];
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return null;
				}
				/// <summary>
				/// Gets a DataTable Filled with the results of in passed procedure.
				/// </summary>
				/// <param name="procedure">Name of the nrocedure.</param>
				/// <param name="param">Parameter to be used when execting.</param>
				/// <returns>A DataTable or null.</returns>
				public DataTable ExecuteDataTable(string procedure, SqlParameter param)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(procedure, param);

					try
					{

						if(da == null)
							return null;

						da.Fill(ret);
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;

						if(ret.Tables.Count > 0)
							return ret.Tables[0];
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return null;
				}
				/// <summary>
				/// Gets a DataTable Filled with the results of in passed procedure.
				/// </summary>
				/// <param name="procedure">Name of the nrocedure.</param>
				/// <param name="param">Parameters to be used when execting.</param>
				/// <returns>A DataTable or null.</returns>
				public DataTable ExecuteDataTable(string procedure, SqlParameter[] param)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(procedure, param);

					try
					{

						if(da == null)
							return null;

						da.Fill(ret);

						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;

						if(ret.Tables.Count > 0)
							return ret.Tables[0];
					}
					catch(Exception ex){ThorwGenProcErr(procedure, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return null;
				}
				#endregion
			}
			#endregion
			#region Nested Class: SqlText
			/// <summary>Provides methods to access and manipulate a SqlServer Database through Sql text.</summary>
			[Serializable]
				public sealed class SqlText
			{
				#region Construction
				SqlConnectionSource conn = null;
				public SqlText(SqlConnectionSource connection){this.conn = connection;}
				#endregion
				#region Throw Error
				void ThorwGenProcErr(string sql, System.Exception ex){Err.ThrowErr("An error occuried in SqlServer.SqlText when trying to execute the following sql: \r\n" + sql + ". More detailed information has been stored in this System.Exceptions InnerException.", ex); }
				#endregion
				#region ExecuteReader
				public SqlDataReader ExecuteReader(string sql)
				{
					if(null == conn) return null;
					SqlDataReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(sql, CommandType.Text);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteReader(CommandBehavior.CloseConnection);
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}

					return ret;
				}
				#endregion
				#region ExecuteXmlReader
				public XmlReader ExecuteXmlReader(string sql)
				{
					if(null == conn) return null;
					XmlReader ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(sql, CommandType.Text);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteXmlReader();
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				#endregion
				#region ExecuteScalar
				public object ExecuteScalar(string sql)
				{
					if(null == conn) return null;
					object ret = null;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(sql, CommandType.Text);

						if(null == cmd)
							return null;

						ret = cmd.ExecuteScalar();
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

					return ret;
				}
				#endregion
				#region ExecuteNonQuery
				public void ExecuteNonQuery(string sql)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(sql, CommandType.Text);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}
				public void ExecuteNonQuery(string sql, SqlTransaction trans)
				{
					if(null == conn) return;
					SqlCommand cmd = null;

					try
					{
						cmd = conn.CreateCommand(sql, CommandType.Text, trans);

						if(null == cmd)
							return;

						cmd.ExecuteNonQuery();
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}
					finally
					{
						cmd.Connection.Close();
						cmd.Dispose();
						cmd = null;
					}

				}		
				#endregion
				#region ExecuteDataSet
				/// <summary>
				/// Gets a DataSet Filled with the results of in passed sql.
				/// </summary>
				/// <param name="sql">Name of the nrocedure.</param>
				/// <returns>A DataSet or null.</returns>
				public DataSet ExecuteDataSet(string sql)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(sql, CommandType.Text);

					try
					{

						if(da == null)
							return ret;

						da.Fill(ret);
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return null;
				}
				#endregion
				#region ExecuteDataTable
				/// <summary>
				/// Gets a DataTable Filled with the results of in passed sql.
				/// </summary>
				/// <param name="sql">Name of the nrocedure.</param>
				/// <returns>A DataTable or null.</returns>
				public DataTable ExecuteDataTable(string sql)
				{
					if(null == conn) return null;

					DataSet ret = null;
					SqlDataAdapter da = conn.CreateDataAdapter(sql, CommandType.Text);

					try
					{

						if(da == null)
							return null;

						da.Fill(ret);
					
						da.SelectCommand.Connection.Close();
						da.Dispose();
						da = null;

						if(ret.Tables.Count > 0)
							return ret.Tables[0];
					}
					catch(Exception ex){ThorwGenProcErr(sql, ex);}
					finally
					{
						if(null != da)
						{
							da.SelectCommand.Connection.Close();
							da.Dispose();
							da = null;
						}
					}

					return null;
				}
				#endregion
			}
			#endregion
			#region Construction
			SqlConnectionSource conn = null;
			public SqlAccessor(SqlConnectionSource connection)
			{
				conn = connection;
				proc = new SqlProcedure(conn);
				sql = new SqlText(conn);
			}
			#endregion
			#region Helper Properties
			SqlProcedure proc = null;
			public SqlProcedure ExecuteProcedure{get{return proc;}}
			SqlText sql = null;
			public SqlText ExecuteSql{get{return sql;}}
			#endregion
		}
		#endregion
		#region Construction
		#region CreateFromString
		/// <summary>
		/// Attempts to create a ConnectionSource by parsing an in passed connection string.
		/// </summary>
		/// <param name="conn_str">Connection string.</param>
		/// <returns>A ConnectionSource or null.</returns>
		public static SqlConnectionSource CreateFromString(string conn_str)
		{
			SqlConnectionSource ret = new SqlConnectionSource();

			string[] split01 = conn_str.Split(";".ToCharArray());
			foreach(string s in split01)
			{
				string[] split02 = s.Split("=".ToCharArray());
				try
				{
					switch(split02[0].ToLower())
					{
						case "data source":
						case "server":
							ret.server = split02[1];
							break;
						case "initial catalog":
						case "database":
							ret.db = split02[1];
							break;
						case "used id":
						case "uid":
							ret.user = split02[1];
							break;
						case "password":
						case "pwd":
							ret.password = split02[1];
							break;
						default:
							if(null != s && s != ";" && s != string.Empty) 
								ret.ex += s + ";";//split02[0] + "=" + split02[1] + ";";
							break;
					}
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
				}
			}
			ret.is_dirty = true;
			string ret_s = ret.ConnectionString;
			return ret;
		}
		#endregion
		public SqlConnectionSource()
		{
			is_dirty = true;
			db_hit = new SqlAccessor(this);
		}
		internal SqlConnectionSource(string server):this(){this.server = server;}
		public SqlConnectionSource(string server, string db):this(server){this.db = db;}
		public SqlConnectionSource(string server, string db, string user, string password):this(server, db)
		{
			this.user = user;
			this.password = password;
		}
		#endregion
		#region Properties
		#region Server
		string server = string.Empty;
		public string Server
		{
			get{return this.server;}
			set
			{
				if(value != this.server)
				{
					this.server = value;
					this.is_dirty = true;
				}
			}
		}
		#endregion
		#region Db
		string db = string.Empty;
		public string Db
		{
			get{return this.db;}
			set
			{
				if(value != this.db)
				{
					this.db = value;
					this.is_dirty = true;
				}
			}
		}
		#endregion
		#region User
		string user = string.Empty;
		public string User
		{
			get{return this.user;}
			set
			{
				if(value != this.user)
				{
					this.user = value;
					this.is_dirty = true;
				}
			}
		}
		#endregion
		#region Password
		string password = string.Empty;
		public string Password
		{
			get{return this.password;}
			set
			{
				if(value != this.password)
				{
					this.password = value;
					this.is_dirty = true;
				}
			}
		}
		#endregion
		#region ConnectionStringEx
		string ex = string.Empty;
		public string Extended
		{
			get{return this.ex;}
			set
			{
				if(value != this.ex)
				{
					this.ex = value;
					this.is_dirty = true;
				}
			}
		}
		#endregion
		#region ConnectionString
		bool is_dirty = false;
		string connection_string = string.Empty;
		public string ConnectionString
		{
			get
			{
				if(is_dirty || null == connection_string || connection_string == string.Empty)
				{
					StringBuilder sb = new StringBuilder();

					if(this.server != string.Empty)
						sb.Append("SERVER=" + this.server + ";");
					
					if(this.db != string.Empty)
						sb.Append("DATABASE=" + this.db + ";");

					if(this.user != string.Empty && this.password != string.Empty)
					{
						sb.Append("UID=" + this.User + ";");
						sb.Append("PWD=" + this.password + ";");
					}

					if(this.ex != string.Empty)
						sb.Append(ex);

					connection_string = sb.ToString();

					sb = null;
					this.is_dirty = false;
				}

				if(connection_string != string.Empty && user == string.Empty)
				{
					string c1 = connection_string.ToUpper();
					string c2 = "Integrated Security=SSPI;Persist Security Info=False;".ToUpper();
					if(c1.IndexOf(c2) == -1)
					{
						if(connection_string.LastIndexOf(";") != (connection_string.Length - 1))
							connection_string += ";";

						connection_string += "Integrated Security=SSPI;Persist Security Info=False;";
					}
				}

				return connection_string;
			}
		}
		#endregion
		#region IsValid
		/// <summary>
		/// Attempts to open a connection using this connection string. 
		/// Returns true after closing the connection if a connection was opened 
		/// otherwise false is returned.
		/// </summary>
		public bool IsValid
		{
			get
			{
				bool is_valid = false;
				SqlConnection conn_sql = new SqlConnection(ConnectionString);
				try
				{

					conn_sql.Open();
					conn_sql.Close();
					is_valid = true;
				}
				catch(Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					is_valid = false;
				}
				finally
				{
					conn_sql.Dispose();
					conn_sql = null;
				}
				return is_valid;
			}
		}
		#endregion
		#region Accessor
		SqlAccessor db_hit;
		/// <summary>
		/// Provides properties and methods to be used when accesing the database.
		/// </summary>
		public SqlAccessor DbAccessor{get{return db_hit;}}
		#endregion
		#endregion
		#region Methods
		#region BeginTransaction
		/// <summary>
		/// Returns a SqlTransaction ready for use.
		/// </summary>
		/// <param name="iso">Transaction IsolationLevel.</param>
		/// <returns></returns>
		public SqlTransaction BeginTransaction(IsolationLevel iso)
		{
			return CreateConnection().BeginTransaction(iso);
		}
		#endregion
		#region BeginTransactionBundled
		/// <summary>
		/// Returns a wrapped SqlConnection and Transaction for ease of connection management and transaction access.
		/// </summary>
		/// <param name="iso">Transaction IsolationLevel.</param>
		/// <returns></returns>
		public TransactionBundle BeginTransactionBundled(IsolationLevel iso)
		{
			return new TransactionBundle(this.CloneExplicit(), iso);
		}
		#endregion
		#region CreateConnection
		/// <summary>
		/// Get an open instance of an SqlConnection pointed at this ConnectionString.
		/// </summary>
		/// <returns>An Open SqlConnection or null.</returns>
		public SqlConnection CreateConnection()
		{
			SqlConnection conn_sql = null;
			try
			{	//don't waste ticks on an IsValid check
				conn_sql = new SqlConnection(ConnectionString);
				conn_sql.Open();
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);//return null if error
				conn_sql = null;
				throw ex;
			}
			return conn_sql;
		}
		#endregion
		#region CreateCommand
		/// <summary>
		/// Gets a SqlCommand object initialized to this connection.
		/// </summary>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand()
		{
			SqlCommand cmd = null;

			try
			{
				SqlConnection conn = CreateConnection();

				if(null == conn)
					return cmd;

				cmd = new SqlCommand(string.Empty, conn);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);//return null if error
				cmd = null;
			}

			return cmd;
		}
		/// <summary>
		/// Gets a SqlCommand object initialized to this connection and set to the in passed command.
		/// </summary>
		/// <param name="command_text">The sql, table name or stored procedure name to use.</param>
		/// <param name="type">The type of command text passed in.</param>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand(string command_text, CommandType type)
		{
			SqlConnection conn = CreateConnection();
			SqlCommand cmd = null;

			if(null == conn)
				return cmd;

			cmd = new SqlCommand(command_text, conn);
			cmd.CommandType = type;

			return cmd;
		}
		/// <summary>
		/// Gets a SqlCommand object initialized to this connection, set to the in passed command, and associated with a SqlTransaction.
		/// </summary>
		/// <param name="command_text">The sql, table name or stored procedure name to use.</param>
		/// <param name="type">The type of command text passed in.</param>
		/// <param name="trans">Associated SqlTransaction.</param>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand(string command_text, CommandType type, SqlTransaction trans)
		{
			SqlConnection conn = trans.Connection;
			SqlCommand cmd = null;

			if(null == conn)
				return cmd;

			cmd = new SqlCommand(command_text, conn);
			cmd.CommandType = type;
			cmd.Transaction = trans;

			return cmd;
		}
		/// <summary>
		/// Gets a SqlCommand initialized to this connection and set to use the in passed procedure.
		/// </summary>
		/// <param name="procedure_name">Stored procedure name.</param>
		/// <param name="param">Parameter to be used by the procedure.</param>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand(string procedure_name, SqlParameter param)
		{
			SqlCommand cmd = CreateCommand(procedure_name, CommandType.StoredProcedure);
			
			if(null == cmd)
				return cmd;

			if(null !=param)
				cmd.Parameters.Add(param);

			return cmd;
		}
		/// <summary>
		/// Gets a SqlCommand initialized to this connection and set to use the in passed procedure.
		/// </summary>
		/// <param name="procedure_name">Stored procedure name.</param>
		/// <param name="param">Parameter to be used by the procedure.</param>
		/// <param name="trans">Associated SqlTransaction.</param>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand(string procedure_name, SqlParameter param, SqlTransaction trans)
		{
			SqlCommand cmd = CreateCommand(procedure_name, CommandType.StoredProcedure, trans);
			
			if(null == cmd)
				return cmd;

			if(null !=param)
			{
				cmd.Parameters.Add(param);
				//cmd.Transaction = trans;
			}

			return cmd;
		}		/// <summary>
		/// Gets a SqlCommand initialized to this connection and set to use the in passed procedure.
		/// </summary>
		/// <param name="procedure_name">Stored procedure name.</param>
		/// <param name="param">Parameters to be used by the procedure.</param>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand(string procedure_name, SqlParameter[] param)
		{
			SqlCommand cmd = CreateCommand(procedure_name, CommandType.StoredProcedure);
			
			if(null == cmd)
				return cmd;

			if(param.Length > 0)
			{
				for(int i = 0; i < param.Length; i ++)
					cmd.Parameters.Add(param[i]);
			}

			return cmd;
		}
		/// <summary>
		/// Gets a SqlCommand initialized to this connection and set to use the in passed procedure, parameters, and transaction.
		/// </summary>
		/// <param name="procedure_name">Stored procedure name.</param>
		/// <param name="param">Parameters to be used by the procedure.</param>
		/// <param name="trans">Associated SqlTransaction.</param>
		/// <returns>A SqlCommand or null.</returns>
		public SqlCommand CreateCommand(string procedure_name, SqlParameter[] param, SqlTransaction trans)
		{
			SqlCommand cmd = CreateCommand(procedure_name, CommandType.StoredProcedure, trans);
			
			if(null == cmd)
				return cmd;

			if(param.Length > 0)
			{
				for(int i = 0; i < param.Length; i ++)
					cmd.Parameters.Add(param[i]);
			}

			//cmd.Transaction = trans;

			return cmd;
		}
		#endregion
		#region CreateDataAdapter
		/// <summary>
		/// Creates a SqlDatAdapter initalized to this connection and set to use the in passed info.
		/// </summary>
		/// <param name="command_text">The Sql, Table or Stored Procedure to be used.</param>
		/// <param name="type">The type of command execution.</param>
		/// <returns>A SqlDataAdapter or null.</returns>
		public SqlDataAdapter CreateDataAdapter(string command_text, CommandType type)
		{
			SqlDataAdapter ret = null;

			try
			{
				SqlCommand cmd = CreateCommand(command_text, type);

				if(null == cmd)
					return ret;

				ret = new SqlDataAdapter(cmd);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);//return null if error
				ret = null;
			}
			return ret;
		}
		/// <summary>
		/// Creates a SqlDatAdapter initalized to this connection and set to use the in passed info.
		/// </summary>
		/// <param name="procedure_name">Stored procedure name.</param>
		/// <param name="param">Parameter to be used by the procedure.</param>
		/// <returns>A SqlDataAdapter or null.</returns>
		public SqlDataAdapter CreateDataAdapter(string procedure, SqlParameter param)
		{
			SqlDataAdapter ret = null;

			try
			{
				SqlCommand cmd = CreateCommand(procedure, param);

				if(null == cmd)
					return ret;

				ret = new SqlDataAdapter(cmd);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);//return null if error
				ret = null;
			}
			return ret;
		}
		/// <summary>
		/// Creates a SqlDatAdapter initalized to this connection and set to use the in passed info.
		/// </summary>
		/// <param name="procedure_name">Stored procedure name.</param>
		/// <param name="param">Parameters to be used by the procedure.</param>
		/// <returns>A SqlDataAdapter or null.</returns>
		public SqlDataAdapter CreateDataAdapter(string procedure, SqlParameter[] param)
		{
			SqlDataAdapter ret = null;

			try
			{
				SqlCommand cmd = CreateCommand(procedure, param);

				if(null == cmd)
					return ret;

				ret = new SqlDataAdapter(cmd);
			}
			catch(Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);//return null if error
				ret = null;
			}
			return ret;
		}
		#endregion
		#region CleanConnection
		/// <summary>
		/// Close, dispose, and set an SqlConnection to null.
		/// </summary>
		/// <param name="conn"></param>
		public void CleanConnection(SqlConnection conn)
		{
			if(conn != null)
			{
				conn.Close();
				conn.Dispose();
				conn = null;
			}
		}
		#endregion
		#region CleanCommand
		/// <summary>
		/// Dispose of a SqlCommand and set it equal to null.
		/// </summary>
		/// <param name="cmd">The SqlCommand.</param>
		public void CleanCommand(SqlCommand cmd)
		{
			if(cmd != null)
			{
				cmd.Dispose();
				cmd = null;
			}
		}
		#endregion
		#region CleanDataReader
		/// <summary>
		/// Close a SqlDataReader and set it equal to null. 
		/// If the reader was created with CommandBehavior.CloseConnection the 
		/// connection associated with the reader will be closed.
		/// </summary>
		/// <param name="r">The SqlDataReader.</param>
		public void CleanDataReader(SqlDataReader r)
		{
			if(r != null)
			{
				r.Close();
				r = null;
			}
		}
		#endregion
		#region CleanDataAdapter
		/// <summary>
		/// Dispose of a SqlDataAdapter and set it to null.
		/// </summary>
		/// <param name="adapt">The SqlDataAdapter to be cleaned.</param>
		public void CleanDataAdapter(SqlDataAdapter adapt)
		{
			if(adapt != null)
			{
				adapt.Dispose();
				adapt = null;
			}
		}
		#endregion
		#region CleanTransaction
		/// <summary>
		/// Dispose of a SqlTransaction nd set it qua to null.
		/// </summary>
		/// <param name="trans"></param>
		public void CleanTransaction(SqlTransaction trans)
		{
			if(trans != null)
			{
				trans.Dispose();
				trans = null;
			}
		}
		#endregion
		#region CleanTransactionBundle
		/// <summary>
		/// Runs CleanTransaction and CleanConnection on the in passed TransactionBundle.
		/// </summary>
		/// <param name="trans"></param>
		public void CleanTransactionBundle(TransactionBundle trans)
		{
			if(trans != null)
			{
				CleanTransaction(trans.Transaction);
				CleanConnection(trans.Connection);
				trans = null;
			}
		}
		#endregion
		#region Clone
		/// <summary>
		/// returns an object with an equal value but a diffrent address space a.k.a memory allocation a.k.a. non-equal refreance.
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			SqlConnectionSource ret = new SqlConnectionSource(server, db, user, password);
			return ret;
		}
		#endregion
		#region CloneExplicit
		/// <summary>
		/// Returns a clone of the object already cast for use.
		/// </summary>
		/// <returns>A SqlConnectionSource.</returns>
		public SqlConnectionSource CloneExplicit()
		{
			return (SqlConnectionSource)Clone();
		}
		#endregion
		#endregion
	}
	#endregion
}