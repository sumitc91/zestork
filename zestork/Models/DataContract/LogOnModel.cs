using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataContract
{
    public class LogOnModel
    {
        public User User { get; set; }
        public string ReturnUrl { get; set; }
        public string statusCode { get; set; }
    }
}