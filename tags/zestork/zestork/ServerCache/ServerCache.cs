﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections;

namespace SunPower.Common.Infrastructure.ServerCache
{
    public class ServerCache
    {
        private static ServerCache _current = new ServerCache();

        private ReaderWriterLockSlim _itemsLock = new ReaderWriterLockSlim();
        private static Hashtable _items = new Hashtable();

        private object _cacheRefreshFrequencyLock = new object();
        private TimeSpan _cacheRefreshFrequency = new TimeSpan(0, 0, 15);

        private Timer _timer = null;

        public TimeSpan CacheRefreshFrequency
        {
            get
            {
                TimeSpan res;

                lock (_cacheRefreshFrequencyLock)
                {
                    res = _cacheRefreshFrequency;
                }

                return res;
            }
            set
            {
                lock (_cacheRefreshFrequencyLock)
                {
                    _cacheRefreshFrequency = value;
                }

                int refreshFrequencyMilliseconds = (int)CacheRefreshFrequency.TotalMilliseconds;
                this._timer.Change(refreshFrequencyMilliseconds, refreshFrequencyMilliseconds);
            }
        }

        public int CachedItemsNumber
        {
            get
            {
                _itemsLock.EnterReadLock();
                try
                {
                    return _items.Count;
                }
                finally
                {
                    _itemsLock.ExitReadLock();
                }
            }
        }

        private ServerCache()
        {
            int refreshFrequencyMilliseconds = (int)CacheRefreshFrequency.TotalMilliseconds;
            this._timer = new System.Threading.Timer(new
                       TimerCallback(CacherefreshTimerCallback),
                       null, refreshFrequencyMilliseconds, refreshFrequencyMilliseconds);
        }

        public static ServerCache Current
        {
            get
            {
                return _current;
            }
        }

        public object this[object key]
        {
            get
            {
                _itemsLock.EnterUpgradeableReadLock();
                try
                {
                    ServerCacheItem res = (ServerCacheItem)_items[key];
                    if (res != null)
                    {
                        if (res.SlidingExpirationTime.TotalMilliseconds > 0)
                        {
                            _itemsLock.EnterWriteLock();
                            try
                            {
                                res.LastAccessTime = DateTime.Now;
                            }
                            finally
                            {
                                _itemsLock.ExitWriteLock();
                            }
                        }
                        return res.ItemValue;
                    }
                    else
                    {
                        return null;
                    }
                }
                finally
                {
                    _itemsLock.ExitUpgradeableReadLock();
                }
            }
            set
            {
                _itemsLock.EnterWriteLock();
                try
                {
                    _items[key] = new ServerCacheItem(value);
                }
                finally
                {
                    _itemsLock.ExitWriteLock();
                }
            }
        }

        public void Insert(object key, object value)
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items[key] = new ServerCacheItem(value);
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        public void Insert(object key, object value, DateTime expirationDate)
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items[key] = new ServerCacheItem(value, expirationDate);
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        public void Insert(object key, object value, TimeSpan expirationTime)
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items[key] = new ServerCacheItem(value, expirationTime);
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        public void Insert(object key, object value, TimeSpan expirationTime, bool slidingExpiration)
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items[key] = new ServerCacheItem(value, expirationTime, slidingExpiration);
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        public void Insert(object key, object value, DateTime expirationDate, TimeSpan slidingExpirationTime)
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items[key] = new ServerCacheItem(value, expirationDate, slidingExpirationTime);
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        public void Remove(object key)
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items.Remove(key);
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        public void RemoveAll()
        {
            _itemsLock.EnterWriteLock();
            try
            {
                _items.Clear();
            }
            finally
            {
                _itemsLock.ExitWriteLock();
            }
        }

        private void CacherefreshTimerCallback(object state)
        {
            _itemsLock.EnterUpgradeableReadLock();
            try
            {
                Dictionary<object, ServerCacheItem> delItems = new Dictionary<object, ServerCacheItem>();
                DateTime dtNow = DateTime.Now;
                foreach (DictionaryEntry de in _items)
                {
                    ServerCacheItem ci = (ServerCacheItem)de.Value;
                    if (ci.ExpirationDate < dtNow)
                    {
                        delItems.Add(de.Key, ci);
                    }
                    else
                    {
                        if (ci.SlidingExpirationTime.TotalMilliseconds > 0)
                        {
                            if (dtNow.Subtract(ci.LastAccessTime).TotalMilliseconds > ci.SlidingExpirationTime.TotalMilliseconds)
                            {
                                delItems.Add(de.Key, ci);
                            }
                        }
                    }
                }

                if (delItems.Count > 0)
                {
                    _itemsLock.EnterWriteLock();
                    try
                    {
                        foreach (KeyValuePair<object, ServerCacheItem> kvp in delItems)
                        {
                            if (_items.ContainsKey(kvp.Key))
                            {
                                _items.Remove(kvp.Key);
                            }
                        }
                    }
                    finally
                    {
                        _itemsLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                _itemsLock.ExitUpgradeableReadLock();
            }
        }
    }
}
