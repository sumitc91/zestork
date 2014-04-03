using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using zestork.Service.twitter;
using System.Xml;
using zestork.Models.DataContract;

namespace zestork.Service
{
    public class twitterService
    {
        string url = "";
        string xml = "";
        public string name = "";
        public string username = "";
        public string profileImage = "";
        public string followersCount = "";
        public string noOfTweets = "";
        public string recentTweet = "";

        public LogOnModel Login(string returnUrl, string AbsoluteUri, string oauth_token, string oauth_verifier)
        {
            var userData = new LogOnModel();
            twitterService twitterService = new Service.twitterService();
            twitterService.loginInit(oauth_token, oauth_verifier);   
            return userData;
        }


        private void GetUserDetailsFromTwitter(string oauth_token,string oauth_verifier)
        {
            if (oauth_token != null & oauth_verifier != null)
            {
                //imgTwitter.Visible = false;
                //tbleTwitInfo.Visible = true;
                var oAuth = new oAuthTwitter();
                //Get the access token and secret.
                oAuth.AccessTokenGet(oauth_token, oauth_verifier);
                if (oAuth.TokenSecret.Length > 0)
                {
                    //We now have the credentials, so make a call to the Twitter API.
                    url = "http://twitter.com/account/verify_credentials.xml";
                    xml = oAuth.oAuthWebRequest(oAuthTwitter.Method.GET, url, String.Empty);
                    XmlDocument xmldoc = new XmlDocument();
                    xmldoc.LoadXml(xml);
                    XmlNodeList xmlList = xmldoc.SelectNodes("/user");
                    foreach (XmlNode node in xmlList)
                    {
                        name = node["name"].InnerText;
                        username = node["screen_name"].InnerText;
                        profileImage = node["profile_image_url"].InnerText;
                        followersCount = node["followers_count"].InnerText;
                        noOfTweets = node["statuses_count"].InnerText;
                        recentTweet = node["status"]["text"].InnerText;
                    }
                }
            }
        }

        private void loginInit(string oauth_token, string auth_verifier)
        {

            var oAuth = new oAuthTwitter();

            if (oauth_token == null)
            {
                //Redirect the user to Twitter for authorization.
                //Using oauth_callback for local testing.
                oAuth.CallBackUrl = "http://zestork.pcongo.com/Account/Login/twitter";
                
                //Response.Redirect(oAuth.AuthorizationLinkGet());
                string responseuri = oAuth.AuthorizationLinkGet();
            }
            else
            {
                GetUserDetailsFromTwitter(oauth_token, auth_verifier);
            }
        }
    }
}