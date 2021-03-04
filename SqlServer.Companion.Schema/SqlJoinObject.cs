
//using System;
//using System.Data;
//using System.Data.Common;
//using System.Data.SqlClient;
//using System.Data.SqlTypes;
//using System.Xml;

//using SqlServer.Companion.Base;
//using SqlServer.Companion.Resx;
//using SqlServer.Companion.Schema;

//namespace SqlServer.Companion
//{
//    [Serializable]
//    public sealed class SqlJoinObjectCollection : SqlDataObjectCollectionBase
//    {
//        #region Construction
//        public SqlJoinObjectCollection() { }
//        #endregion
//        #region Add
//        public int Add(SqlJoinObject value)
//        {
//            return base.Add(value);
//        }
//        #endregion
//        #region IndexOf
//        public int IndexOf(SqlJoinObject value)
//        {
//            return base.IndexOf(value);
//        }
//        #endregion
//        #region Insert
//        public void Insert(int index, SqlJoinObject value)
//        {
//            base.Insert(index, value);
//        }
//        #endregion
//        #region Remove
//        public void Remove(SqlJoinObject value)
//        {
//            base.Remove(value);
//        }
//        #endregion
//        #region Indexer
//        new public SqlJoinObject this[int index]
//        {
//            get { return (SqlJoinObject)base[index]; }
//        }
//        #endregion
//    }
//    public class SqlJoinObject : ISqlDataObject
//    {
//        #region Tables
//        SqlDbTableCollection tables = null;
//        public SqlDbTableCollection Tables
//        {
//            get
//            {
//                if (null == tables)
//                    tables = SqlSchema.FindTables(__conn, LoadType.Lazy);

//                return tables;
//            }
//        }
//        #endregion
//        #region Columns
//        SqlDbTableFieldCollection col = null;
//        public SqlDbTableFieldCollection Columns
//        {
//            get
//            {
//                if (null == col)
//                    col = SqlSchema.FindTableFields(__conn, this.TableName);

//                return col;
//            }
//        }
//        #endregion

//        public void AddTable(SqlDbTable tbl)
//        {
//            this.Tables.Add(tbl);
//        }

//        public void AddColumn(SqlDbTableFieldBase col)
//        {
//            this.Columns.Add(col);
//        }

//        public string GetSelect() { return ""; }
//        public string GetInsert() { return ""; }
//        public string GetUpdate() { return ""; }
//        public string GetDelete() { return ""; }

//    }
//}
