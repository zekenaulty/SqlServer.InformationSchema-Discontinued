/* using System;
using System.Collections;
using System.Collections.Specialized;

using SqlServer.Companion.Base;

namespace SqlServer.Companion.Schema
{
	/// <summary>Base for the SqlSchema cache.</summary>
	internal sealed class SqlSchemaCache
	{
		Hashtable hash = new Hashtable();
		TimeSpan cache_length = new TimeSpan(0, 0, 5, 0, 0);
		public SqlSchemaCache(){}
		/// <summary>
		/// Add an ISqlSchemaObject to a collection of cached objects of the same type.
		/// </summary>
		/// <param name="cached_type">The type being cached.</param>
		/// <param name="key">Key used to access the cached item.</param>
		/// <param name="value">The ISqlSchemaObject to cached.</param>
		public void Add(Type cached_type, object key, ISqlSchemaObject value)
		{
			if(hash.Contains(cached_type))
			{
				((Hashtable)hash[cached_type]).Add(key, value);
			}
			else
			{
				Hashtable sub_hash = new Hashtable();
				sub_hash.Add(key, value);
				hash.Add(cached_type, sub_hash);
			}
		}
		/// <summary>
		/// Remove an ISqlSchemaObject from the cache.
		/// </summary>
		/// <param name="cached_type">The type being cached.</param>
		/// <param name="key">Key used to access the cached item.</param>
		public void Remove(Type cached_type, ISqlSchemaObject key)
		{
			if(hash.Contains(cached_type))
				((Hashtable)hash[cached_type]).Remove(key);
		}
		/// <summary>
		/// Provides access to an ISqlSchemaObject from the cache.
		/// </summary>
		/// <param name="cached_type">The type being cached.</param>
		/// <param name="key">Key used to access the cached item.</param>
		/// <returns>An IsqlSchemaObject to be cast as needed.</returns>
		public ISqlSchemaObject Items(Type cached_type, object key)
		{
			if(hash.Contains(cached_type))
			{
				((ISqlSchemaObject)((Hashtable)hash[cached_type])[key]).InstanceAccessed = DateTime.Now;
				return (ISqlSchemaObject)((ISqlSchemaObject)((Hashtable)hash[cached_type])[key]).Clone();
			}
			else
			{
				throw new Exception("Cache item not found.");
			}
		}
		/// <summary>
		/// Completely clear the cache.
		/// </summary>
		public void Clear(){hash.Clear();}
		/// <summary>
		/// Clear a cache level by type.
		/// </summary>
		/// <param name="cached_type"></param>
		public void Clear(Type cached_type)
		{
			if(hash.Contains(cached_type))
				((Hashtable)hash[cached_type]).Clear();
		}
		/// <summary>
		/// Gets/Sets the TimeSpan that an Item can remain in the cache.
		/// </summary>
		public TimeSpan CacheLength
		{
			get{return cache_length;}
			set{cache_length = value;}
		}
		/// <summary>
		/// Remove all stale cache items.
		/// </summary>
		public void PurgeStale()
		{
			IEnumerator k = hash.Keys.GetEnumerator();
			while(k.MoveNext())
				PurgeStale((Type)k.Current);
		}
		/// <summary>
		/// Rmoe all stale cache itms from a cache type.
		/// </summary>
		/// <param name="cached_type"></param>
		public void PurgeStale(Type cached_type)
		{
			if(hash.Contains(cached_type))
			{
				Hashtable h = hash[cached_type] as Hashtable;
				IEnumerator k = h.GetEnumerator();
				for(int i = 0; i < h.Count; i++)
				{
					k.MoveNext();
					ISqlSchemaObject o = h[i] as ISqlSchemaObject;
					DateTime dif = o.InstanceCreated.Add(cache_length);

					if(dif > o.InstanceAccessed)
						h.Remove(k.Current);
				}
			}
		}
	}
}
*/