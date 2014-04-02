using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Net;
using System.IO;
using Google.Apis.Auth.OAuth2;
using System.Text;
using Newtonsoft.Json;
using zestork.Models.DataWrapper;
using zestork.Models;
using System.Data.Entity.Validation;
using zestork.CommonMethods;
using System.Configuration;

namespace zestork.Service
{
    public class googleService
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public LogOnModel Login(string returnUrl, string code)
        {

            var _db = new ZestorkContainer();
            var userData = new LogOnModel();
            string app_id = "";
            string app_secret = "";
            app_id = ConfigurationManager.AppSettings["googleAppID"].ToString();
            app_secret = ConfigurationManager.AppSettings["googleAppSecret"].ToString();
            //if (returnUrl.Contains("zestork.pcongo"))
            //{
            //    app_id = ConfigurationManager.AppSettings["googleAppIDZestork"].ToString();
            //    app_secret = ConfigurationManager.AppSettings["googleAppSecretZestork"].ToString();
            //}
            //else
            //{
            //    app_id = ConfigurationManager.AppSettings["googleAppID"].ToString();
            //    app_secret = ConfigurationManager.AppSettings["googleAppSecret"].ToString();
            //}
           
            string scope = "email%20profile";            
            
            if (code == null)
            {
                userData.ReturnUrl = (string.Format(
                    "https://accounts.google.com/o/oauth2/auth?scope={0}&state=%2Fprofile&redirect_uri={1}&response_type=code&client_id={2}&approval_prompt=force",
                    scope, returnUrl, app_id));
                logger.Info(userData.ReturnUrl);
                return userData;
            }
            else
            {
                string access_token = getGoogleAuthToken(returnUrl, scope, code, app_id, app_secret);
                String URI = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;
                logger.Info(URI);
                WebClient webClient = new WebClient();
                Stream stream = webClient.OpenRead(URI);
                string googleUserDetailString;

                /*I have not used any JSON parser because I do not want to use any extra dll/3rd party dll*/
                using (StreamReader br = new StreamReader(stream))
                {
                    googleUserDetailString = br.ReadToEnd();
                }
                var googleUserDetails = JsonConvert.DeserializeObject<googleUserDetails>(Convert.ToString(googleUserDetailString));
                userData.User = new User();
                if (_db.Users.Any(x => x.Username == googleUserDetails.email))
                {
                    Users user = _db.Users.SingleOrDefault(x => x.Username == googleUserDetails.email);
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

                    userData.User.FirstName = googleUserDetails.given_name;
                    userData.User.LastName = googleUserDetails.family_name;
                    userData.User.Username = googleUserDetails.email;
                    try
                    {
                        userData.User.Email = googleUserDetails.email;
                    }
                    catch (Exception)
                    {

                        userData.User.Email = "NA";
                    }

                    userData.User.Gender = googleUserDetails.gender;
                    userData.User.ImageUrl = googleUserDetails.picture;
                    var user = new Users
                    {
                        Username = googleUserDetails.email,
                        Password = Guid.NewGuid().ToString(),
                        Source = "google",
                        isActive = "true",
                        Type = "user",
                        guid = Guid.NewGuid().ToString(),
                        FirstName = googleUserDetails.given_name,
                        LastName = googleUserDetails.family_name,
                        gender = googleUserDetails.gender,
                        ImageUrl = googleUserDetails.picture
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
            return userData;
        }

        private string getGoogleAuthToken(string returnUrl, string scope, string code, string app_id, string app_secret)
        {
            byte[] buffer = Encoding.ASCII.GetBytes("code=" + code + "&client_id=" + app_id + "&client_secret=" + app_secret + "&redirect_uri=" + returnUrl + "&grant_type=authorization_code");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://accounts.google.com/o/oauth2/token");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = buffer.Length;

            Stream strm = request.GetRequestStream();
            strm.Write(buffer, 0, buffer.Length);
            strm.Close();

            HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
            Stream responseStream = webResponse.GetResponseStream();
            StreamReader responseStreamReader = new StreamReader(responseStream);
            string result = responseStreamReader.ReadToEnd();//parse token from result
            var googleAccessTokenResponse = JsonConvert.DeserializeObject<googleAccessTokenWrapper>(Convert.ToString(result));
            return googleAccessTokenResponse.access_token;
        }
    }
}