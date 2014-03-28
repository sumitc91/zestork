using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace zestork.Models.DataContract
{
    public class User
    {
        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string FacebookId { get; set; }
        public string FacebookAccessToken { get; set; }
        public string GoogleId { get; set; }
        public string GoogleAccessToken { get; set; }
        public string ImageUrl { get; set; }
        public System.DateTime LastLogin { get; set; }
        public System.DateTime Created { get; set; }
        public Nullable<System.DateTime> Changed { get; set; }
    }
}