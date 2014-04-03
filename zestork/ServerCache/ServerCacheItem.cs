using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zestork.Common.Infrastructure.ServerCache
{
    public class ServerCacheItem
    {
        private object _itemValue;
        private DateTime _createdDate = DateTime.Now;
        private DateTime _expirationDate = DateTime.MaxValue;
        private TimeSpan _slidingExpirationTime = new TimeSpan();
        private DateTime _lastAccessTime = DateTime.Now;

        public object ItemValue
        {
            get { return _itemValue; }
            set { _itemValue = value; }
        }

        public DateTime CreatedDate
        {
            get { return _createdDate; }
            set { _createdDate = value; }
        }

        public DateTime ExpirationDate
        {
            get { return _expirationDate; }
            set { _expirationDate = value; }
        }

        public TimeSpan SlidingExpirationTime
        {
            get { return _slidingExpirationTime; }
            set { _slidingExpirationTime = value; }
        }

        public DateTime LastAccessTime
        {
            get { return _lastAccessTime; }
            set { _lastAccessTime = value; }
        }

        public ServerCacheItem(object itemValue)
        {
            this._itemValue = itemValue;
        }

        public ServerCacheItem(object itemValue, DateTime expirationDate)
            : this(itemValue)
        {
            this._expirationDate = expirationDate;
        }

        public ServerCacheItem(object itemValue, TimeSpan expirationTime)
        {
            this._itemValue = itemValue;
            this._expirationDate = this._createdDate.Add(expirationTime);
        }

        public ServerCacheItem(object itemValue, TimeSpan expirationTime, bool slidingExpiration)
        {
            this._itemValue = itemValue;
            if (slidingExpiration)
            {
                this._slidingExpirationTime = expirationTime;
            }
            else
            {
                this._expirationDate = this._createdDate.Add(expirationTime);
            }
        }

        public ServerCacheItem(object itemValue, DateTime expirationDate, TimeSpan slidingExpirationTime)
            : this(itemValue, expirationDate)
        {
            this._slidingExpirationTime = slidingExpirationTime;
        }
    }
}
