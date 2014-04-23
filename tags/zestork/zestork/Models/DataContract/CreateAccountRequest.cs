using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataContract
{
    public class CreateAccountRequest
    {
        public string userName { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string isActive { get; set; }
        public string source { get; set; }
        public string uid { get; set; }
        public string type { get; set; }
        public string CompanyName { get; set; }
        public string referral { get; set; }
    }
}