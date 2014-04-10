using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataContract
{
    public class detailsEditUserPage
    {
        public int Id { get; set; }
        public string Username { get; set; }
        //public string Password { get; set; }
        public string isActive { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public string guid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
        public string gender { get; set; }
        public bool Locked { get; set; }
        public string PageThemeColor { get; set; }
        public List<string> skillTags { get; set; }
    }
}