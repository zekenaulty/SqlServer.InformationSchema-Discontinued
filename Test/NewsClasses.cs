using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Xml;
using SqlServer.Companion;
using SqlServer.Companion.Base;


//data classes
namespace SqlDevTest.Data
{
	#region Data Class
	[System.Serializable()]
	[SqlServer.Companion.TableRow("News")]
	[System.Xml.Serialization.XmlRootAttribute(ElementName="News", Namespace="", IsNullable=false)]
	public class News : SqlServer.Companion.Base.ISqlDataObject
	{
		
		protected bool _isDirty = false;
		
		protected int _NewsID = System.Int32.MinValue;
		
		protected string _NewsCaption = System.String.Empty;
		
		protected string _NewsText = System.String.Empty;
		
		public News()
		{
		}
		
		public News(int newsID, string newsCaption, string newsText)
		{
			this._NewsID = newsID;
			this._NewsCaption = newsCaption;
			this._NewsText = newsText;
		}
		
		[System.Xml.Serialization.XmlElement("IsDirty", typeof(bool))]
		public virtual bool IsDirty
		{
			get
			{
				return this._isDirty;
			}
			set
			{
				// This set should not be here, but in order to fully serialize the object all properties that will be serialized must be read/write. I leave it to the developer to know better than to set the dirty property manualy.
				this._isDirty = value;
			}
		}
		
		public virtual bool IsNew
		{
			get
			{
				if ((this._NewsID == System.Int32.MinValue))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		
		// ----------------------------------------------------------
		// 	PRIMARY KEY
		// 	Catalog: SqlDevTest, Table: News
		// 	Field: NewsID, DataType: int
		/// Gets/Sets the value of NewsID.
		// 	Precision: 10
		/// This property can be set but it should never be set. It is allowed to be set to support XmlSerialization and only for that reason.
		// ----------------------------------------------------------
		[SqlServer.Companion.TableRowField("NewsID", "int", false, 10, 1)]
		[SqlServer.Companion.TableRowPrimaryKey()]
		[System.Xml.Serialization.XmlElement("NewsID", typeof(int))]
		public virtual int NewsID
		{
			get
			{
				return this._NewsID;
			}
			set
			{
				// This set should not be here, but in order to fully serialize the object all properties that will be serialized must be read/write. I leave it to the developer to know better than to set the PK.
				this._NewsID = value;
			}
		}
		
		// ----------------------------------------------------------
		// 	Catalog: SqlDevTest, Table: News
		// 	Field: NewsCaption, DataType: varchar
		/// Gets/Sets the value of NewsCaption.
		// 	Length: 255
		// ----------------------------------------------------------
		[SqlServer.Companion.TableRowField("NewsCaption", "varchar", false, 255, 2)]
		[System.Xml.Serialization.XmlElement("NewsCaption", typeof(string))]
		public virtual string NewsCaption
		{
			get
			{
				return this._NewsCaption;
			}
			set
			{
				this._NewsCaption = value;
				this.MarkDirty();
			}
		}
		
		// ----------------------------------------------------------
		// 	Catalog: SqlDevTest, Table: News
		// 	Field: NewsText, DataType: text
		/// Gets/Sets the value of NewsText.
		// 	Length: 2147483647
		// ----------------------------------------------------------
		[SqlServer.Companion.TableRowField("NewsText", "text", false, 2147483647, 3)]
		[System.Xml.Serialization.XmlElement("NewsText", typeof(string))]
		public virtual string NewsText
		{
			get
			{
				return this._NewsText;
			}
			set
			{
				this._NewsText = value;
				this.MarkDirty();
			}
		}
		
		protected virtual void MarkDirty()
		{
			this._isDirty = true;
		}
		
		protected static SqlDevTest.Data.News Build(System.Data.SqlClient.SqlDataReader r)
		{
			SqlDevTest.Data.News ret = new SqlDevTest.Data.News();
			

			if (r.IsDBNull(0))
			{
				ret._NewsID = System.Int32.MinValue;
			}
			else
			{
				ret._NewsID = r.GetInt32(0);
			}
			

			if (r.IsDBNull(1))
			{
				ret._NewsCaption = System.String.Empty;
			}
			else
			{
				ret._NewsCaption = r.GetString(1);
			}
			

			if (r.IsDBNull(2))
			{
				ret._NewsText = System.String.Empty;
			}
			else
			{
				ret._NewsText = r.GetString(2);
			}
			

			return ret;
		}
		
		// Build an ArrayList from an inpassed SqlDataReader.
		protected static System.Collections.ArrayList BuildArray(System.Data.SqlClient.SqlDataReader r)
		{
			System.Collections.ArrayList ret = null;
			

			// very poorly generated while loop... good job Microsoft.. NOT
			for (
				; r.Read(); 
				)
			{
				if ((ret == null))
				{
					ret = new System.Collections.ArrayList();
				}
				ret.Add(SqlDevTest.Data.News.Build(r));
			}
			return ret;
		}
		
		public virtual void WriteXML(string filepath)
		{
			System.Xml.Serialization.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(this.GetType());
			System.IO.TextWriter write = new System.IO.StreamWriter(filepath);
			serial.Serialize(write, this);
			write.Close();
		}
		
		public static SqlDevTest.Data.News LoadXML(string filepath)
		{
			System.Xml.Serialization.XmlSerializer serial = new System.Xml.Serialization.XmlSerializer(typeof(SqlDevTest.Data.News));
			System.IO.TextReader read = new System.IO.StreamReader(filepath);
			SqlDevTest.Data.News ret = ((SqlDevTest.Data.News)(serial.Deserialize(read)));
			read.Close();
			return ret;
		}
		
		public static SqlDevTest.Data.News News__FindByNewsID(SqlServer.Companion.SqlConnectionSource conn, int newsID)
		{
			SqlDevTest.Data.News ret = null;
			System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@NewsID", newsID);
			System.Data.SqlClient.SqlDataReader r = null;
			r = conn.DbAccessor.ExecuteProcedure.ExecuteReader("News__FindByNewsID", param);
			if ((null == r))
			{
			}
			else
			{
				ret = SqlDevTest.Data.News.Build(r);
				r.Close();
				r = null;
			}
			return ret;
		}
		
		public virtual void Delete(SqlServer.Companion.SqlConnectionSource conn)
		{
			if ((this.IsNew == false))
			{
				System.Data.SqlClient.SqlParameter pk = new System.Data.SqlClient.SqlParameter("@NewsID", this._NewsID);
				conn.DbAccessor.ExecuteProcedure.ExecuteNonQuery("News__DELETE_ROW", pk);
			}
		}
		
		public virtual void Delete(SqlServer.Companion.SqlConnectionSource conn, System.Data.SqlClient.SqlTransaction tran)
		{
			if ((this.IsNew == false))
			{
				System.Data.SqlClient.SqlParameter pk = new System.Data.SqlClient.SqlParameter("@NewsID", this._NewsID);
				conn.DbAccessor.ExecuteProcedure.ExecuteNonQuery("News__DELETE_ROW", pk, tran);
			}
		}
		
		public virtual void Insert(SqlServer.Companion.SqlConnectionSource conn)
		{
			if ((this.IsNew == true))
			{
				System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[] {
																										  new System.Data.SqlClient.SqlParameter("@NewsCaption", this._NewsCaption),
																										  new System.Data.SqlClient.SqlParameter("@NewsText", this._NewsText),
																										  new System.Data.SqlClient.SqlParameter("@NewsID", this._NewsID)};
				param[2].Direction = System.Data.ParameterDirection.Output;
				conn.DbAccessor.ExecuteProcedure.ExecuteNonQuery("News__INSERT_ROW", param);
				this._NewsID = ((int)(param[2].Value));
				this._isDirty = false;
			}
		}
		
		public virtual void Insert(SqlServer.Companion.SqlConnectionSource conn, System.Data.SqlClient.SqlTransaction tran)
		{
			if ((this.IsNew == true))
			{
				System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[] {
																										  new System.Data.SqlClient.SqlParameter("@NewsCaption", this._NewsCaption),
																										  new System.Data.SqlClient.SqlParameter("@NewsText", this._NewsText),
																										  new System.Data.SqlClient.SqlParameter("@NewsID", this._NewsID)};
				param[2].Direction = System.Data.ParameterDirection.Output;
				conn.DbAccessor.ExecuteProcedure.ExecuteNonQuery("News__INSERT_ROW", param, tran);
				this._NewsID = ((int)(param[2].Value));
				this._isDirty = false;
			}
		}
		
		public virtual void Update(SqlServer.Companion.SqlConnectionSource conn)
		{
			if ((this.IsNew == false))
			{
				System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[] {
																										  new System.Data.SqlClient.SqlParameter("@NewsCaption", this._NewsCaption),
																										  new System.Data.SqlClient.SqlParameter("@NewsText", this._NewsText),
																										  new System.Data.SqlClient.SqlParameter("@NewsID", this._NewsID)};
				conn.DbAccessor.ExecuteProcedure.ExecuteNonQuery("News__UPDATE_ROW", param);
				this._isDirty = false;
			}
		}
		
		public virtual void Update(SqlServer.Companion.SqlConnectionSource conn, System.Data.SqlClient.SqlTransaction tran)
		{
			if ((this.IsNew == false))
			{
				System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[] {
																										  new System.Data.SqlClient.SqlParameter("@NewsCaption", this._NewsCaption),
																										  new System.Data.SqlClient.SqlParameter("@NewsText", this._NewsText),
																										  new System.Data.SqlClient.SqlParameter("@NewsID", this._NewsID)};
				conn.DbAccessor.ExecuteProcedure.ExecuteNonQuery("News__UPDATE_ROW", param, tran);
				this._isDirty = false;
			}
		}
		
		public static System.Collections.ArrayList News__FindAll(SqlServer.Companion.SqlConnectionSource conn)
		{
			System.Collections.ArrayList ret = null;
			System.Data.SqlClient.SqlDataReader r = null;
			r = conn.DbAccessor.ExecuteProcedure.ExecuteReader("News__FindAll");
			if ((null == r))
			{
			}
			else
			{
				ret = SqlDevTest.Data.News.BuildArray(r);
				r.Close();
				r = null;
			}
			return ret;
		}
		
		public static System.Collections.ArrayList News__FindAllPaged(SqlServer.Companion.SqlConnectionSource conn, int pageIndex, int numResults)
		{
			System.Collections.ArrayList ret = null;
			System.Data.SqlClient.SqlParameter[] param = new System.Data.SqlClient.SqlParameter[] {
																									  new System.Data.SqlClient.SqlParameter("@PageIndex", pageIndex),
																									  new System.Data.SqlClient.SqlParameter("@NumResults", numResults)};
			System.Data.SqlClient.SqlDataReader r = null;
			r = conn.DbAccessor.ExecuteProcedure.ExecuteReader("News__FindAllPaged", param);
			if ((null == r))
			{
			}
			else
			{
				ret = SqlDevTest.Data.News.BuildArray(r);
				r.Close();
				r = null;
			}
			return ret;
		}
		
		public static int News__FindAllCount(SqlServer.Companion.SqlConnectionSource conn)
		{
			int ret = 0;
			ret = ((int)(conn.DbAccessor.ExecuteProcedure.ExecuteScalar("News__FindAllCount")));
			return ret;
		}
		
		public static SqlDevTest.Data.News News__FindByCaption(SqlServer.Companion.SqlConnectionSource conn, string newsCaption)
		{
			SqlDevTest.Data.News ret = null;
			System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@NewsCaption", newsCaption);
			System.Data.SqlClient.SqlDataReader r = null;
			r = conn.DbAccessor.ExecuteProcedure.ExecuteReader("News__FindByCaption", param);
			if ((null == r))
			{
			}
			else
			{
				ret = SqlDevTest.Data.News.Build(r);
				r.Close();
				r = null;
			}
			return ret;
		}
	}

	#endregion
}

//business classes
namespace SqlDevTest.Business
{
	
	#region News Business Class
	[System.Xml.Serialization.XmlRootAttribute(ElementName="News", Namespace="", IsNullable=false)]
	public class News : SqlDevTest.Data.News
	{
		
		public static SqlDevTest.Business.News FromBase(SqlDevTest.Data.News data)
		{
			SqlDevTest.Business.News ret = new SqlDevTest.Business.News();
			ret._NewsID = data.NewsID;
			ret._NewsCaption = data.NewsCaption;
			ret._NewsText = data.NewsText;
			return ret;
		}
	}

	#endregion

	#region NewsCollection class
	[System.Xml.Serialization.XmlRootAttribute(ElementName="NewsCollection", Namespace="", IsNullable=false)]
	public class NewsCollection : SqlServer.Companion.Base.SqlDataObjectCollectionBase
	{
		
		new public SqlDevTest.Business.News this[int index]
		{
			get
			{
				return ((SqlDevTest.Business.News)(base[index]));
			}
		}
		
		public virtual int Add(SqlDevTest.Business.News value)
		{
			return base.Add(value);
		}
		
		public virtual int IndexOf(SqlDevTest.Business.News value)
		{
			return base.IndexOf(value);
		}
		
		public virtual void Insert(int index, SqlDevTest.Business.News value)
		{
			base.Insert(index, value);
		}
		
		public virtual void Remove(SqlDevTest.Business.News value)
		{
			base.Remove(value);
		}
		
		public static SqlDevTest.Business.NewsCollection FromBaseArray(System.Collections.ArrayList a)
		{
			SqlDevTest.Business.NewsCollection ret = null;
			for (int i = 0; (i < a.Count); i++
				)
			{
				if ((ret == null))
				{
					ret = new SqlDevTest.Business.NewsCollection();
				}
				ret.Add(SqlDevTest.Business.News.FromBase(((SqlDevTest.Data.News)(a[i]))));
			}
			return ret;
		}
	}

	#endregion


}



