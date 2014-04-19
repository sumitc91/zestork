using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zestork.Models;

namespace zestork.CommonMethods
{
    public class AccountControllerMethods
    {
        public static string getUserType(string username)
        {           
            var _db = new ZestorkContainer();
            Users user = _db.Users.SingleOrDefault(x => x.Username == username);
            if (user != null)
            {
                return user.Type;
            }
            else
            {
                return "user";
            }
        }
    }
}