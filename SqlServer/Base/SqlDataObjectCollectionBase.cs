using System;

namespace SqlServer.Companion.Base
{
	#region Event Helpers (should move to independent files.)
	#region Added Event/Args
	public delegate void CollectionItemAddedEvent(object sender, CollectionItemAddedEventArgs e);
	public class CollectionItemAddedEventArgs : EventArgs
	{
		internal CollectionItemAddedEventArgs(ISqlDataObject item):base()
		{
			o = item;
		}
		ISqlDataObject o = null;
		public virtual object Item{get{return o;}}
		bool allow = true;
		public bool AllowAction
		{
			get{return allow;}
			set{if(value != allow) allow = value;}
		}
	}
	#endregion
	#region Removed Event/Args
	public delegate void CollectionItemRemovedEvent(object sender, CollectionItemRemovedEventArgs e);
	public class CollectionItemRemovedEventArgs : EventArgs
	{
		public CollectionItemRemovedEventArgs(ISqlDataObject item):base()
		{
			o = item;
		}
		ISqlDataObject o = null;
		public virtual object Item{get{return o;}}
		bool allow = true;
		public bool AllowAction
		{
			get{return allow;}
			set{if(value != allow) allow = value;}
		}
	}
	#endregion
	#region Saved Event/Args
	public delegate void SqlDataObjectSavedEvent(object sender, SqlDataObjectSavedEventArgs e);
	public sealed class SqlDataObjectSavedEventArgs : EventArgs
	{
		ISqlDataObject src = null;
		public SqlDataObjectSavedEventArgs(ISqlDataObject source)
		{
			src = source;
		}
		public ISqlDataObject Item
		{
			get
			{
				return src;
			}
		}
	}
	#endregion
	#region  Deleted Event/Args
	public delegate void SqlDataObjectDeletedEvent(object sender, SqlDataObjectDeletedEventArgs e);
	public sealed class SqlDataObjectDeletedEventArgs : EventArgs
	{
		ISqlDataObject src = null;
		public SqlDataObjectDeletedEventArgs(ISqlDataObject source)
		{
			src = source;
		}
		public ISqlDataObject Item
		{
			get
			{
				return src;
			}
		}
	}

	#endregion
	#endregion
	/// <summary>
	/// Provides a basic collection class geared to creating strongly typed collection 
	/// that also support collection events. I.E. adding an item will raise the item 
	/// added event and allow the chance to prevent this action or modify 
	/// the item being added...
	/// </summary>
	public abstract class SqlDataObjectCollectionBase : System.Collections.IEnumerable, System.IDisposable, System.Collections.IList
	{
		public SqlDataObjectCollectionBase(){}
		public SqlDataObjectCollectionBase(System.Collections.ArrayList array)
		{
			foreach(ISqlDataObject iSql in array)
				this._values.Add(iSql);
		}

		#region Walk like a Collection. Talk like a Collection.
		private System.Collections.ArrayList _values = new System.Collections.ArrayList();
		/// <summary>
		/// 
		/// </summary>
		public int Count{get{return _values.Count;}}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		protected int Add(ISqlDataObject value)
		{
			CollectionItemAddedEventArgs e = new CollectionItemAddedEventArgs(value);

			OnItemAdded(e);
			#region Attempt to hook deleted event
			if(e.AllowAction)
			{
				Type t = null;
				t = value.GetType();

				if(null != t)
				{	//t.GetEvents();//would have to walk
					System.Reflection.EventInfo ei = null;
					ei = t.GetEvent("SqlDataObjectDeletedEvent");
					
					SqlDataObjectDeletedEvent de = null;
					de = new SqlDataObjectDeletedEvent(this.Deleted);
					if(null != ei && null != de )
						ei.AddEventHandler(value, de);

					ei = null;
				}
				t = null;
			}
			#endregion
			if(e.AllowAction)
				return _values.Add(value);
			else
				return -1;
		}
		void Deleted(object sender, SqlDataObjectDeletedEventArgs e){this.RemoveDel(e.Item);}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		protected int IndexOf(ISqlDataObject value)
		{
			return _values.IndexOf(value);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		/// <param name="value"></param>
		protected void Insert(int index, ISqlDataObject value)
		{
			CollectionItemAddedEventArgs e = new CollectionItemAddedEventArgs(value);

			OnItemAdded(e);

			if(e.AllowAction)
				_values.Insert(index, value);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		protected void Remove(ISqlDataObject value)
		{
			CollectionItemRemovedEventArgs e = new CollectionItemRemovedEventArgs(value);

			OnItemRemoved(e);

			if(e.AllowAction)
				_values.Remove(value);
		}
		void RemoveDel(ISqlDataObject value)
		{
			_values.Remove(value);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="index"></param>
		protected void RemoveAt(int index)
		{
			CollectionItemRemovedEventArgs e = new CollectionItemRemovedEventArgs(this[index]);

			OnItemRemoved(e);

			if(e.AllowAction)
				_values.RemoveAt(index);
		}
		#endregion
		#region Implementation of IDisposable
		public void Dispose()
		{
			_values.Clear();
			_values = null;
		}
		#endregion
		#region Implementation of IEnumerable
		public System.Collections.IEnumerator GetEnumerator()
		{
			return _values.GetEnumerator();
		}
		public System.Collections.IEnumerator GetEnumerator(int index, int count)
		{
			return _values.GetEnumerator(index, count);
		}
		#endregion
		#region Indexer
		/// <summary>
		/// Indexer.......Item.....VB()/C#[]
		/// </summary>
		protected ISqlDataObject this[int index]
		{
			get{return (ISqlDataObject)_values[index];}
			set{_values[index] = value;}
		}
		#endregion
		#region Events
		public CollectionItemAddedEvent ItemAdded;
		void OnItemAdded(CollectionItemAddedEventArgs e)
		{
			if(null != ItemAdded)
				ItemAdded(this, e);
		}
		public CollectionItemRemovedEvent ItemRemoved;
		void OnItemRemoved(CollectionItemRemovedEventArgs e)
		{
			if(null != ItemRemoved)
				ItemRemoved(this, e);
		}
		#endregion

		#region IList Members

		public bool IsReadOnly
		{
			get
			{
				return this._values.IsReadOnly;
			}
		}

		object System.Collections.IList.this[int index]
		{
			get
			{
				return this._values[index];
			}
			set
			{
				if(value is ISqlDataObject && value.GetType() == this._values[index].GetType())
					this._values[index] = value;
			}
		}

		void System.Collections.IList.RemoveAt(int index){this._values.RemoveAt(index);}
		void System.Collections.IList.Insert(int index, object value)
		{
			if(value is ISqlDataObject)
				this.Insert(index, (ISqlDataObject)value);
		}

		void System.Collections.IList.Remove(object value)
		{
			if(value is ISqlDataObject)
				this.Remove((ISqlDataObject)value);
		}

		public bool Contains(object value)
		{
			return this._values.Contains(value);;
		}

		public void Clear()
		{
			this._values.Clear();
		}

		int System.Collections.IList.IndexOf(object value)
		{
			return this._values.IndexOf(value);
		}

		int System.Collections.IList.Add(object value)
		{
			if(value is ISqlDataObject)
				return this._values.Add(value);
			else
				return -1;

		}

		public bool IsFixedSize
		{
			get
			{
				return this._values.IsFixedSize;
			}
		}

		#endregion

		#region ICollection Members
		public bool IsSynchronized{get{return this._values.IsSynchronized;}}
		public void CopyTo(Array array, int index){this._values.CopyTo(array, index);}
		public object SyncRoot{get{return this._values.SyncRoot;}}
		#endregion
	}
}