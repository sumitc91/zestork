using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataWrapper
{
    public class imgurUploadImageResponse
    {
        public data data { get; set; }
    }
    public class data
    {
        public string deletehash { get; set; }
        public string link { get; set; }
    }
}