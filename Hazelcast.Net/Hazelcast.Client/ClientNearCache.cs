using System;
using System.Collections.Concurrent;
using Hazelcast.Client.Spi;
using Hazelcast.Config;
using Hazelcast.IO.Serialization;
using Hazelcast.Net.Ext;
using Hazelcast.Util;

namespace Hazelcast.Client
{
    public class ClientNearCache
    {
        internal const int evictionPercentage = 20;

        internal const int cleanupInterval = 5000;
        public static readonly object NullObject = new object();
        internal readonly ConcurrentDictionary<Data, CacheRecord> cache;
        internal readonly AtomicBoolean canCleanUp;

        internal readonly AtomicBoolean canEvict;
        internal readonly ClientContext context;

        internal readonly EvictionPolicy evictionPolicy;

        internal readonly InMemoryFormat inMemoryFormat;
        internal readonly bool invalidateOnChange;

        internal readonly string mapName;
        internal readonly long maxIdleMillis;
        internal readonly int maxSize;
        internal readonly long timeToLiveMillis;
        internal long lastCleanup;

        public ClientNearCache(string mapName, ClientContext context, NearCacheConfig nearCacheConfig)
        {
            this.mapName = mapName;
            this.context = context;
            maxSize = nearCacheConfig.GetMaxSize();
            maxIdleMillis = nearCacheConfig.GetMaxIdleSeconds()*1000;
            inMemoryFormat = nearCacheConfig.GetInMemoryFormat();
            timeToLiveMillis = nearCacheConfig.GetTimeToLiveSeconds()*1000;
            invalidateOnChange = nearCacheConfig.IsInvalidateOnChange();
            evictionPolicy = (EvictionPolicy) Enum.Parse(typeof (EvictionPolicy), nearCacheConfig.GetEvictionPolicy());
            cache = new ConcurrentDictionary<Data, CacheRecord>();
            canCleanUp = new AtomicBoolean(true);
            canEvict = new AtomicBoolean(true);
            lastCleanup = Clock.CurrentTimeMillis();
        }

        public virtual void Put(Data key, object @object)
        {
            FireTtlCleanup();
            if (evictionPolicy == EvictionPolicy.None && cache.Count >= maxSize)
            {
                return;
            }
            if (evictionPolicy != EvictionPolicy.None && cache.Count >= maxSize)
            {
                FireEvictCache();
            }
            object value;
            if (@object == null)
            {
                value = NullObject;
            }
            else
            {
                value = inMemoryFormat.Equals(InMemoryFormat.Binary)
                    ? context.GetSerializationService().ToData(@object)
                    : @object;
            }
            cache.TryAdd(key, new CacheRecord(this, key, value));
        }

        //TODO FIXME
        private void FireEvictCache()
        {
            if (canEvict.CompareAndSet(true, false))
            {
                //try
                //{
                //    //this.context.GetExecutionService().Execute(new _Runnable_94(this));
                //}
                //catch (RejectedExecutionException)
                //{
                //    canEvict.Set(true);
                //}
                //catch (Exception e)
                //{
                //    throw ExceptionUtil.Rethrow(e);
                //}
            }
        }

        //TODO FIXME
        private void FireTtlCleanup()
        {
            //if (Clock.CurrentTimeMillis() < (lastCleanup + cleanupInterval))
            //{
            //    return;
            //}
            //if (canCleanUp.CompareAndSet(true, false))
            //{
            //    try
            //    {
            //        context.GetExecutionService().Execute(new _Runnable_124(this));
            //    }
            //    catch (RejectedExecutionException)
            //    {
            //        canCleanUp.Set(true);
            //    }
            //    catch (Exception e)
            //    {
            //        throw ExceptionUtil.Rethrow(e);
            //    }
            //}
        }

        public virtual object Get(Data key)
        {
            FireTtlCleanup();
            CacheRecord record = null;
            cache.TryGetValue(key, out record);
            if (record != null)
            {
                record.Access();
                if (record.Expired())
                {
                    Invalidate(key);
                    return null;
                }
                if (record.value.Equals(NullObject))
                {
                    return NullObject;
                }
                return inMemoryFormat.Equals(InMemoryFormat.Binary)
                    ? context.GetSerializationService().ToObject((Data) record.value)
                    : record.value;
            }
            return null;
        }

        public virtual void Invalidate(Data key)
        {
            CacheRecord record = null;
            try
            {
                cache.TryRemove(key, out record);
            }
            catch (ArgumentNullException e)
            {
            }
        }

        public virtual void Clear()
        {
            cache.Clear();
        }

        internal class CacheRecord : IComparable<CacheRecord>
        {
            private readonly ClientNearCache _enclosing;
            internal readonly long creationTime;

            internal readonly AtomicInteger hit;
            internal readonly Data key;

            internal readonly object value;

            //TODO volatile
            internal long lastAccessTime;

            internal CacheRecord(ClientNearCache _enclosing, Data key, object value)
            {
                this._enclosing = _enclosing;
                this.key = key;
                this.value = value;
                long time = Clock.CurrentTimeMillis();
                lastAccessTime = time;
                creationTime = time;
                hit = new AtomicInteger(0);
            }

            public virtual int CompareTo(CacheRecord o)
            {
                if (EvictionPolicy.Lru.Equals(_enclosing.evictionPolicy))
                {
                    return lastAccessTime.CompareTo((o.lastAccessTime));
                }
                if (EvictionPolicy.Lfu.Equals(_enclosing.evictionPolicy))
                {
                    return hit.Get().CompareTo((o.hit.Get()));
                }
                return 0;
            }

            internal virtual void Access()
            {
                hit.IncrementAndGet();
                lastAccessTime = Clock.CurrentTimeMillis();
            }

            internal virtual bool Expired()
            {
                long time = Clock.CurrentTimeMillis();
                return (_enclosing.maxIdleMillis > 0 && time > lastAccessTime + _enclosing.maxIdleMillis) ||
                       (_enclosing.timeToLiveMillis > 0 && time > creationTime + _enclosing.timeToLiveMillis);
            }
        }

        internal enum EvictionPolicy
        {
            None,
            Lru,
            Lfu
        }
    }
}