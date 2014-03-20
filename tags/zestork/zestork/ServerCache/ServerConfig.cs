using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace SunPower.Common.Infrastructure.ServerCache
{
    public static class ServerConfig
    {
        private static bool? _cachingEnabled;
        public static string UserName = null;
        public static string Password = null;
        public static string StrURL = null;

        public static bool CachingEnabled
        {
            get
            {
                if (_cachingEnabled == null)
                {
                    string keyStrVal = ConfigurationManager.AppSettings["CachingEnabled"] as string;
                    if (!string.IsNullOrEmpty(keyStrVal))
                    {
                        _cachingEnabled = keyStrVal.ToLower() == "true";
                        return _cachingEnabled.GetValueOrDefault();
                    }

                }
                else
                {
                    return _cachingEnabled.GetValueOrDefault();
                }

                //set to default value
                _cachingEnabled = true;
                return true;
            }
            set
            {
                _cachingEnabled = value;
            }
        }

        private static int _slidingExpirationTime = 3600;

        /// <summary>
        /// Sliding expiration time in seconds
        /// </summary>
        public static int SlidingExpirationTime
        {
            get
            {
                return _slidingExpirationTime;
            }
            set
            {
                _slidingExpirationTime = value;
            }
        }
    }
}
