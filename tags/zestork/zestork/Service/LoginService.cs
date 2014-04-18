using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using zestork.Models.DataContract;

namespace zestork.Service
{
    public class LoginService
    {
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public LogOnModel Login(string returnUrl)
        {
            var model = new LogOnModel();
            
            
                    
            return model;
        }
        public LogOnModel linkedinLogin(string returnUrl, string AbsoluteUri, string oauth_token, string oauth_verifier)
        {
            var model = new LogOnModel();
            linkedinService linkedinService = new linkedinService();
            model = linkedinService.Login(returnUrl, AbsoluteUri, oauth_token, oauth_verifier);
            return model;
        }
        public LogOnModel twitterinLogin(string returnUrl, string AbsoluteUri, string oauth_token, string oauth_verifier)
        {
            var model = new LogOnModel();
            twitterService twitterService = new twitterService();
            model = twitterService.Login(returnUrl, AbsoluteUri, oauth_token, oauth_verifier);
            return model;
        }
        public LogOnModel googleLogin(string returnUrl,string code)
        {
            var model = new LogOnModel();
            googleService googleService = new googleService();            
            model = googleService.Login(returnUrl, code);
            return model;
        }
        public LogOnModel webLogin(string userName, string passwrod, string returnUrl, string keepMeSignedIn)
        {
            var model = new LogOnModel();
            webLoginService webLoginService = new webLoginService();
            model = webLoginService.Login(userName, passwrod, returnUrl, keepMeSignedIn);
            return model;
        }
        public LogOnModel facebookLogin(string returnUrl, string code)
        {
            var model = new LogOnModel();
            
            FacebookService FacebookService = new FacebookService();
            model = FacebookService.Login(returnUrl, code);
           
            return model;
        }
        public LogOnModel LogOn(string returnUrl)
        {
            var model = new LogOnModel();
            if (!String.IsNullOrEmpty(returnUrl))
                model.ReturnUrl = returnUrl;
            else
                model.ReturnUrl = "/";

            //check if user is authenticated..
            //model.User
            return model;
        }

    }
}