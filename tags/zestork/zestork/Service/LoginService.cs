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

        public LogOnModel Login(string returnUrl,string code,string id)
        {
            var model = new LogOnModel();
            if (id == "facebook")
            {
                FacebookService FacebookService = new FacebookService();
                model = FacebookService.Login(returnUrl, code);
            }            
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