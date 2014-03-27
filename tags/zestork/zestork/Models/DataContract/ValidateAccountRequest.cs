using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataContract
{
    public class ValidateAccountRequest
    {
        public string userName { get; set; }
        public string guid { get; set; }
    }
}