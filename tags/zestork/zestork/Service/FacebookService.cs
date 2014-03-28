using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using System.Net;
using System.IO;
using Facebook;

namespace zestork.Service
{
    public class FacebookService
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public LogOnModel Login(string returnUrl,string code)
        {
            var userData = new LogOnModel();
            userData = checkAuthorization(returnUrl, code);
            return userData;
        }

        private LogOnModel checkAuthorization(string returnUrl, string code)
        {
            
            string app_id = "226108297510981";            
            string app_secret = "8e01ee04ed1f1372310b76e6ad54e9eb";
            var userData = new LogOnModel();
            string scope = "";
            if (code == null)
            {
                userData.ReturnUrl = (string.Format(
                    "https://graph.facebook.com/oauth/authorize?client_id={0}&redirect_uri={1}&scope={2}",
                    app_id, returnUrl, scope));

                return userData;
            }
            else
            {
                Dictionary<string, string> tokens = new Dictionary<string, string>();                
                string url = string.Format(
                    "https://graph.facebook.com/oauth/access_token?client_id={0}&redirect_uri={1}&scope={2}&code={3}&client_secret={4}",
                    app_id, returnUrl, scope, code, app_secret);

                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {

                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    string vals = reader.ReadToEnd();

                    foreach (string token in vals.Split('&'))
                    {
                        tokens.Add(token.Substring(0, token.IndexOf("=")),
                            token.Substring(token.IndexOf("=") + 1, token.Length - token.IndexOf("=") - 1));
                    }

                }

                string access_token = tokens["access_token"];

                var client = new FacebookClient(access_token);

                //client.Post("/me/feed", new { message = "this is just a sample message2" });

                dynamic me = client.Get("me");
                string firstName = me.first_name;
                string lastName = me.last_name;
                string username = me.username;
                string gender = me.gender;
            }

            return userData;
        }

    }
}