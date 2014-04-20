using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace zestork.Common.Infrastructure
{
    public class TokenManager
    {
        private static Dictionary<string, CPSession> storeSession = new Dictionary<string, CPSession>();

        public static void CreateSession(CPSession session, bool isPersistant)
        {
            try
            {
                //storeSession.Add(session.getID(), session);
                object key = session.getID();
                //WCFCache.WCFCache.Current.Insert(key, session, new TimeSpan(24, 0, 0), true);
                long PeristSessionSpan = Convert.ToInt32(ConfigurationManager.AppSettings["PeristSessionSpan"].ToString());
                long defaultSessionSpan = Convert.ToInt32(ConfigurationManager.AppSettings["defaultSessionSpan"].ToString());

                int Hours = 0, Minutes = 0, Seconds = 0;
                if (isPersistant)
                {
                    Seconds = Convert.ToInt32(PeristSessionSpan % 60);
                    PeristSessionSpan /= 60;
                    Minutes = Convert.ToInt32(PeristSessionSpan % 60);
                    PeristSessionSpan /= 60;
                    Hours = Convert.ToInt32(PeristSessionSpan);
                    ServerCache.ServerCache.Current.Insert(key, session, new TimeSpan(Hours, Minutes, Seconds), true);
                }
                else
                {
                    Seconds = Convert.ToInt32(defaultSessionSpan % 60);
                    defaultSessionSpan /= 60;
                    Minutes = Convert.ToInt32(defaultSessionSpan % 60);
                    defaultSessionSpan /= 60;
                    Hours = Convert.ToInt32(defaultSessionSpan);
                    ServerCache.ServerCache.Current.Insert(key, session, new TimeSpan(Hours, Minutes, Seconds), true);
                }

            }
            catch (Exception)
            {
            }
        }

        public static void removeSession(String sessionID)
        {
            //storeSession.Remove(sessionID);
            ServerCache.ServerCache.Current.Remove(sessionID);
        }

        public static bool isValidSession(string tokenID)
        {
            CPSession session = null;
            session = (CPSession)ServerCache.ServerCache.Current[tokenID];
            if (session != null)
                return session.isAlive();
            else
                return false;
            //if (storeSession.ContainsKey(tokenID))
            //{
            //    CPSession session = storeSession.FirstOrDefault(x => x.Key == tokenID).Value;
            //    return session.isAlive();
            //}
            //else
            //{
            //    return false;
            //}
        }

        public static CPSession getSessionInfo(string tokenID)
        {
            if (isValidSession(tokenID))
            {
                CPSession session = null;
                return session = (CPSession)ServerCache.ServerCache.Current[tokenID];
                //return storeSession.FirstOrDefault(x => x.Key == tokenID).Value;
            }
            else
            {
                //throw new ArgumentException("Invalid TokenID [" + tokenID + "]");
                return null;
            }

        }

        //public static Dictionary<string, CPSession> getAllSessionInfo()
        //{
        //    return storeSession;
        //}
    }
}
