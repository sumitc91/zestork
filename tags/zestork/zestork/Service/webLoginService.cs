using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using zestork.CommonMethods;
using zestork.Models;
using System.Data.Entity.Validation;

namespace zestork.Service
{
    public class webLoginService
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private dbContextException dbContextException = new dbContextException();

        public LogOnModel Login(string userName, string passwrod, string returnUrl, string keepMeSignedIn)
        {
            var _db = new ZestorkContainer();
            var userData = new LogOnModel();
            if (_db.Users.Any(x => x.Username == userName && x.Password == passwrod))
            {
                Users user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive=="true");
                if (user != null)
                {
                    userData.User = new User();
                    //user is already registered
                    userData.User.FirstName = user.FirstName;
                    userData.User.LastName = user.LastName;
                    userData.User.Username = user.Username;
                    userData.User.Gender = user.gender;
                    userData.User.ImageUrl = user.ImageUrl;
                    userData.User.Email = user.Username;                    
                    try
                    {
                        if (keepMeSignedIn == "true")                        
                            user.KeepMeSignedIn = "true";                        
                        else                        
                            user.KeepMeSignedIn = "false";
                        
                        _db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        dbContextException dbContextException = new CommonMethods.dbContextException();
                        dbContextException.logDbContextException(e);
                    }

                    userData.statusCode = "200";
                }
                else
                    userData.statusCode = "403";
            }
            else
                userData.statusCode = "401";
            userData.ReturnUrl = returnUrl;
            return userData;
        }
    }
}