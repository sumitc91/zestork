using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using SunPower.Common.Infrastructure.Logger;
using zestork.Models.DataContract;
using ASPSnippets.LinkedInAPI;
using System.Data;
using zestork.Models;
using System.Data.Entity.Validation;
using zestork.CommonMethods;
using System.Configuration;

namespace zestork.Service
{
    public class linkedinService
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public LogOnModel Login(string returnUrl, string AbsoluteUri)
        {
            var _db = new ZestorkContainer();
            var userData = new LogOnModel();

            string app_id = string.Empty;
            string app_secret = string.Empty;
            if (returnUrl.Contains("zestork.pcongo"))
            {
                app_id = ConfigurationManager.AppSettings["linkedinAppIDZestork"].ToString();
                app_secret = ConfigurationManager.AppSettings["linkedinAppSecretZestork"].ToString();
            }
            else
            {
                app_id = ConfigurationManager.AppSettings["linkedinAppID"].ToString();
                app_secret = ConfigurationManager.AppSettings["linkedinAppSecret"].ToString();
            }

            LinkedInConnect.APIKey = app_id;
            LinkedInConnect.APISecret = app_secret;
            LinkedInConnect.RedirectUrl = AbsoluteUri.Split('?')[0];
            
            if (LinkedInConnect.IsAuthorized)
            {

                DataSet ds = LinkedInConnect.Fetch();
                String firstName = ds.Tables["person"].Rows[0][1].ToString();
                String lastName = ds.Tables["person"].Rows[0][2].ToString();
                String email = ds.Tables["person"].Rows[0][3].ToString();
                //String course = ds.Tables["person"].Rows[0][4].ToString();
                //String designation = ds.Tables["person"].Rows[0][5].ToString();
                //String profileLink = ds.Tables["person"].Rows[0][7].ToString();                

                userData.User = new User();
                if (_db.Users.Any(x => x.Username == email))
                {
                    Users user = _db.Users.SingleOrDefault(x => x.Username == email);
                    //user is already registered
                    userData.User.FirstName = user.FirstName;
                    userData.User.LastName = user.LastName;
                    userData.User.Username = user.Username;
                    userData.User.Gender = user.gender;
                    userData.User.ImageUrl = user.ImageUrl;
                    userData.statusCode = "200";
                }
                else
                {
                    // add user in database
                    String ID = Guid.NewGuid().ToString();
                    userData.User.FirstName = firstName;
                    userData.User.LastName = lastName;
                    userData.User.Username = email;
                    try
                    {
                        userData.User.Email = email;
                    }
                    catch (Exception)
                    {

                        userData.User.Email = "NA";
                    }

                    userData.User.Gender = "NA";
                    userData.User.ImageUrl = "NA";
                    var user = new Users
                    {
                        Username = email,
                        Password = Guid.NewGuid().ToString(),
                        Source = "linkedin",
                        isActive = "true",
                        Type = "user",
                        guid = Guid.NewGuid().ToString(),
                        FirstName = firstName,
                        LastName = lastName,
                        gender = "NA",
                        ImageUrl = "NA"
                    };

                    _db.Users.Add(user);

                    try
                    {
                        _db.SaveChanges();
                        userData.statusCode = "200";
                    }
                    catch (DbEntityValidationException e)
                    {
                        dbContextException dbContextException = new CommonMethods.dbContextException();
                        dbContextException.logDbContextException(e);
                        throw;
                    }
                }
            }
            else
            {
                LinkedInConnect.Authorize();
            }
            return userData;
        }
    }
}