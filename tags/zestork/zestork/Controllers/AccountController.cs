using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using Newtonsoft.Json;
using zestork.Models;
using System.Data.Entity.Validation;
using zestork.CommonMethods;
using zestork.Service;
using ASPSnippets.LinkedInAPI;
using System.Net;
using zestork.Common.Infrastructure;

namespace zestork.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
        private dbContextException dbContextException = new dbContextException();
       
        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Login(string id)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate
            //{ return true; };
            
            String returnUrl = "";
            var userData = new LogOnModel();
            LoginService LoginService = new LoginService();
            if (id == "facebook")
            {
                String code = Request.QueryString["code"];
                userData = LoginService.facebookLogin("http://" + Request.Url.Authority + "/Account/Login/facebook/", code);
            }
            else if (id == "web")
            {
                String userName = Request.Form["userName"];
                String password = Request.Form["password"];
                userData = LoginService.webLogin(userName,password,returnUrl);                
            }
            else if (id == "google")
            {
                if (Request.QueryString["access_token"] != null)
                {
                    string access_token = Request.QueryString["access_token"];
                }
                String code = Request.QueryString["code"];
                userData = LoginService.googleLogin("http://" + Request.Url.Authority + "/Account/Login/google", code);
            }
            else if (id == "linkedin")
            {
                String AbsoluteUri = Request.Url.AbsoluteUri;

                string oauth_token = Request.QueryString["oauth_token"];
                string oauth_verifier = Request.QueryString["oauth_verifier"];

                userData = LoginService.linkedinLogin("http://" + Request.Url.Authority + "/Account/Login/linkedin", AbsoluteUri, oauth_token, oauth_verifier);
                
            }
            else if (id == "twitter")
            {
                String AbsoluteUri = Request.Url.AbsoluteUri;

                string oauth_token = Request.QueryString["oauth_token"];
                string oauth_verifier = Request.QueryString["oauth_verifier"];

                userData = LoginService.twitterinLogin("http://" + Request.Url.Authority + "/Account/Login/twitter", AbsoluteUri, oauth_token, oauth_verifier);

            }
            //check for specific status code
            if (userData.statusCode != null)
            {
                if (userData.statusCode != "200")
                {
                    Response.Redirect("/#/login/" + userData.statusCode);
                }
                else
                {

                    #region Session                    
                    CPSession session = new CPSession();
                    session.addAttribute("userName", userData.User.Username);
                    bool isPersistent = false; // as of now we have only 1 type of login
                    TokenManager.CreateSession(session, isPersistent);
                    userData.User.guid = session.getID();                    
                    #endregion

                    Response.Redirect("/" + userData.User.FirstName +"-"+userData.User.LastName + "?guid=" + userData.User.guid + "/#/");
                }
            }
            
            return Json(userData, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public ActionResult CreateAccount(CreateAccountRequest req)
        {                       
            logger.Info("new account creation request");            
            var _db = new ZestorkContainer();
            String emailRetVal = String.Empty;
            //if user already exists
            if(_db.Users.Any(x=>x.Username==req.userName))
                return Json(new { code="402",msg="User Already Exists" });
            
            String ID = Guid.NewGuid().ToString();
            var user = new Users
            {
                Username = req.userName,
                Password = req.password,
                Source = req.source,
                isActive = "false",
                Type = req.type,
                guid = Guid.NewGuid().ToString(),
                FirstName = req.firstName,
                LastName = req.lastName,
                gender = "NA",
                ImageUrl = "imageLink"
            };

            _db.Users.Add(user);
            
            var ValidateUserKey = new ValidateUserKey
            {
                 Username = req.userName,
                 guid = ID
            };

            _db.ValidateUserKeys.Add(ValidateUserKey);

            try
            {
                _db.SaveChanges();                
                sendAccountCreationValidationEmail sendAccountCreationValidationEmail = new sendAccountCreationValidationEmail();
                emailRetVal = sendAccountCreationValidationEmail.sendAccountCreationValidationEmailMessage(req.userName, ID,Request);
            }
            catch (DbEntityValidationException e)
            {
                dbContextException.logDbContextException(e);
                throw;
            }

            //Users User = _db.Users.SingleOrDefault(x => x.Username == req.userName);
            //ValidateUserKey key = _db.ValidateUserKeys.SingleOrDefault(x => x.Username == req.userName);

            return Json(new { code="200",msg="successfully created account" });
        }

        [HttpPost]
        public JsonResult validateAccount(ValidateAccountRequest req)
        {            
            var _db = new ZestorkContainer();
            if (_db.ValidateUserKeys.Any(x => x.Username == req.userName && x.guid == req.guid))
            {
                Users User = _db.Users.SingleOrDefault(x => x.Username == req.userName);
                User.isActive = "true";
                try
                {
                    _db.SaveChanges();                    
                }
                catch (DbEntityValidationException e)
                {
                    dbContextException.logDbContextException(e);
                    throw;
                }
                return Json(new { code = "200", msg = "account validated successfully" });
            }
            else
            {
                return Json(new { code = "402", msg = "Link might be expired" });
            }                        

        }

        [HttpPost]
        public JsonResult checkUsernameExists(ValidateAccountRequest req)
        {
            var _db = new ZestorkContainer();
            if (_db.Users.Any(x => x.Username == req.userName))
            {
                return Json(new { code = "402", msg = "username already exists" });
            }
            else
            {
                return Json(new { code = "200", msg = "this is a new username" });                
            }

        }

        public JsonResult showData()
        {
            String path = Request.Url.AbsolutePath;
            String code = Request.QueryString["code"];

            var _db = new ZestorkContainer();
            Users User = _db.Users.SingleOrDefault(x=>x.Username== "sumitchourasia91@gmail.com");
            ValidateUserKey key = _db.ValidateUserKeys.SingleOrDefault(x => x.Username == "sumitchourasia91@gmail.com");
            return Json(User, JsonRequestBehavior.AllowGet);

        }

        public JsonResult getUserInfo()
        {
            String path = Request.Url.AbsolutePath;
            String code = Request.QueryString["code"];

            var _db = new ZestorkContainer();
            Users User = _db.Users.SingleOrDefault(x => x.Username == "sumitchourasia91@gmail.com");
            ValidateUserKey key = _db.ValidateUserKeys.SingleOrDefault(x => x.Username == "sumitchourasia91@gmail.com");
            return Json(User, JsonRequestBehavior.AllowGet);

        }
    }
}
