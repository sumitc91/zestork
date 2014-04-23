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
using zestork.Service.linkedin;
using Newtonsoft.Json;
using System.Xml;
using zestork.Models.DataWrapper;

namespace zestork.Service
{
    public class linkedinService
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private oAuthLinkedIn _oauth = new oAuthLinkedIn();

        public LogOnModel Login(string returnUrl, string AbsoluteUri, string oauth_token, string oauth_verifier, string referral)
        {
            var userData = new LogOnModel();
            var _db = new ZestorkContainer();

            string authLink = string.Empty;
            if (oauth_token != null && oauth_verifier != null)
            {
                var linkedInApiDataResponse = _db.LinkedInAuthApiDatas.SingleOrDefault(x => x.oauth_Token == oauth_token);
                if (linkedInApiDataResponse != null)
                {
                    GetAccessToken(oauth_token, linkedInApiDataResponse.oauth_TokenSecret, oauth_verifier);
                    String UserDetailString = RequestProfile(_oauth.Token, _oauth.TokenSecret, oauth_verifier);
                    var UserDetails = JsonConvert.DeserializeObject<linkedinUserDataWrapper>(Convert.ToString(UserDetailString));                    
                    _db.LinkedInAuthApiDatas.Attach(linkedInApiDataResponse);
                    _db.LinkedInAuthApiDatas.Remove(linkedInApiDataResponse);
                    //_db.SaveChanges();

                    userData.User = new User();
                    if (_db.Users.Any(x => x.Username == UserDetails.emailAddress))
                    {
                        Users user = _db.Users.SingleOrDefault(x => x.Username == UserDetails.emailAddress);
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
                        userData.User.FirstName = UserDetails.firstName;
                        userData.User.LastName = UserDetails.lastName;
                        userData.User.Username = UserDetails.emailAddress;
                        try
                        {
                            userData.User.Email = UserDetails.emailAddress;
                        }
                        catch (Exception)
                        {

                            userData.User.Email = "NA";
                        }

                        userData.User.Gender = "NA";
                        userData.User.ImageUrl = "NA";
                        userData.User.Username = UserDetails.emailAddress;
                        var user = new Users
                        {
                            Username = UserDetails.emailAddress,
                            Password = Guid.NewGuid().ToString(),
                            Source = "linkedin",
                            isActive = "true",
                            Type = "NA",
                            guid = Guid.NewGuid().ToString(),
                            FirstName = UserDetails.firstName,
                            LastName = UserDetails.lastName,
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
            }
            else
            {
                authLink = CreateAuthorization();
                var linkedInApiData = new LinkedInAuthApiData
                {
                    oauth_Token = _oauth.Token,
                    oauth_TokenSecret = _oauth.TokenSecret,
                    oauth_verifier = ""
                };
                _db.LinkedInAuthApiDatas.Add(linkedInApiData);
                try
                {
                    _db.SaveChanges();                    
                }
                catch (DbEntityValidationException e)
                {
                    dbContextException dbContextException = new CommonMethods.dbContextException();
                    dbContextException.logDbContextException(e);
                    throw;
                }
                userData.ReturnUrl = authLink;
            }
            return userData;
        }

        protected string CreateAuthorization()
        {            
            return _oauth.AuthorizationLinkGet();
        }

        protected void GetAccessToken(string Auth_token,string TokenSecret,string Auth_verifier)
        {
            _oauth.Token = Auth_token;
            _oauth.TokenSecret = TokenSecret;
            _oauth.Verifier = Auth_verifier;
            _oauth.AccessTokenGet(Auth_token);            
        }

        protected void SendStatusUpdate(string AccessToken,string AccessTokenSecret,string Auth_verifier)
        {
            _oauth.Token = AccessToken;
            _oauth.TokenSecret = AccessTokenSecret;
            _oauth.Verifier = Auth_verifier;

            string xml = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            xml += "<current-status>It's really working .</current-status>";

            string response = _oauth.APIWebRequest("PUT", "http://api.linkedin.com/v1/people/~/current-status", xml);
            //if (response == "")
            //    txtApiResponse.Text = "Your new status updated";
            
        }

        protected string RequestProfile(string AccessToken,string AccessTokenSecret,string Auth_verifier)
        {
            _oauth.Token = AccessToken;
            _oauth.TokenSecret = AccessTokenSecret;
            _oauth.Verifier = Auth_verifier;
            return _oauth.APIWebRequest("GET", "https://api.linkedin.com/v1/people/~:(id,first-name,last-name,industry,email-address,picture-url)?format=json", null);            
        }

        protected string RequestProfileImage(string AccessToken, string AccessTokenSecret, string Auth_verifier)
        {
            _oauth.Token = AccessToken;
            _oauth.TokenSecret = AccessTokenSecret;
            _oauth.Verifier = Auth_verifier;            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_oauth.APIWebRequest("GET", "http://api.linkedin.com/v1/people/~/picture-urls::(original)", null));
            return JsonConvert.SerializeXmlNode(doc).Replace(@"@", @"").Remove(1, 44);
        }

        //public LogOnModel Login(string returnUrl, string AbsoluteUri)
        //{
        //    var _db = new ZestorkContainer();
        //    var userData = new LogOnModel();
            
        //    string app_id = string.Empty;
        //    string app_secret = string.Empty;
        //    if (returnUrl.Contains("zestork.pcongo"))
        //    {
        //        app_id = ConfigurationManager.AppSettings["linkedinAppIDZestork"].ToString();
        //        app_secret = ConfigurationManager.AppSettings["linkedinAppSecretZestork"].ToString();
        //    }
        //    else
        //    {
        //        app_id = ConfigurationManager.AppSettings["linkedinAppID"].ToString();
        //        app_secret = ConfigurationManager.AppSettings["linkedinAppSecret"].ToString();
        //    }

        //    LinkedInConnect.APIKey = app_id;
        //    LinkedInConnect.APISecret = app_secret;
        //    LinkedInConnect.RedirectUrl = AbsoluteUri.Split('?')[0];
            
        //    if (LinkedInConnect.IsAuthorized)
        //    {

         //      DataSet ds = LinkedInConnect.Fetch();
        //        String firstName = ds.Tables["person"].Rows[0][1].ToString();
        //        String lastName = ds.Tables["person"].Rows[0][2].ToString();
        //        String email = ds.Tables["person"].Rows[0][3].ToString();
        //        //String course = ds.Tables["person"].Rows[0][4].ToString();
        //        //String designation = ds.Tables["person"].Rows[0][5].ToString();
        //        //String profileLink = ds.Tables["person"].Rows[0][7].ToString();                

        //        userData.User = new User();
        //        if (_db.Users.Any(x => x.Username == email))
        //        {
        //            Users user = _db.Users.SingleOrDefault(x => x.Username == email);
        //            //user is already registered
        //            userData.User.FirstName = user.FirstName;
        //            userData.User.LastName = user.LastName;
        //            userData.User.Username = user.Username;
        //            userData.User.Gender = user.gender;
        //            userData.User.ImageUrl = user.ImageUrl;
        //            userData.statusCode = "200";
        //        }
        //        else
        //        {
        //            // add user in database
        //            String ID = Guid.NewGuid().ToString();
        //            userData.User.FirstName = firstName;
        //            userData.User.LastName = lastName;
        //            userData.User.Username = email;
        //            try
        //            {
        //                userData.User.Email = email;
        //            }
        //            catch (Exception)
        //            {

        //                userData.User.Email = "NA";
        //            }

        //            userData.User.Gender = "NA";
        //            userData.User.ImageUrl = "NA";
        //            var user = new Users
        //            {
        //                Username = email,
        //                Password = Guid.NewGuid().ToString(),
        //                Source = "linkedin",
        //                isActive = "true",
        //                Type = "user",
        //                guid = Guid.NewGuid().ToString(),
        //                FirstName = firstName,
        //                LastName = lastName,
        //                gender = "NA",
        //                ImageUrl = "NA"
        //            };

        //            _db.Users.Add(user);

        //            try
        //            {
        //                _db.SaveChanges();
        //                userData.statusCode = "200";
        //            }
        //            catch (DbEntityValidationException e)
        //            {
        //                dbContextException dbContextException = new CommonMethods.dbContextException();
        //                dbContextException.logDbContextException(e);
        //                throw;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        LinkedInConnect.Authorize();                
        //    }
        //    return userData;
        //}
    }
}