using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zestork.Common.Infrastructure
{
    public class CPSession
    {
        private String ID;
        private Dictionary<string, string> attributes;        
        private DateTime lastActivityTime;
        private bool isActive;

        public CPSession()
        {
            ID = Guid.NewGuid().ToString();
            lastActivityTime = DateTime.Now;            
            isActive = true;
            attributes = new Dictionary<string, string>();
        }

        public void invalidate()
        {
            this.isActive = false;
        }

        public bool isAlive()
        {
            return this.isActive;
        }

        public void addAttribute(String key , String value)
        {
            this.attributes.Add(key,value);
        }

        public void removeAttribute(String key)
        {
            this.attributes.Remove(key);
        }

        public void removeAllAttribute()
        {
            this.attributes.Clear();
        }

        public String getAttributeValue(String key)
        {
            return this.attributes.FirstOrDefault(x => x.Key == key).Value;
        }

        //public void setAttributeValue(String key, String value)
        //{
        //    if(key == "V2Cookie")
        //        this.createdTime = DateTime.Now;
        //    else if (key == "RESTCookie")
        //        this.restCreatedTime = DateTime.Now;
        //    this.attributes[key] = value;            
        //}

        public void setAuthAttribute(String key, String value)
        {   
            this.attributes[key] = value;
        }
        
        //public void getAllKeys()
        //{
        //    return this.attributes.Keys
        //}

        public String getID()
        {
            return this.ID;
        }
    }
}
