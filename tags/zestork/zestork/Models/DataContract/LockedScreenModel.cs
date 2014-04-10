using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataContract
{
    public class LockedScreenModel
    {
        public string imageUrl { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string message { get; set; }
        public string guid { get; set; }
        public string postUrl { get; set; }
        public string userName { get; set; }
        public string PageThemeColor { get; set; }
    }
}