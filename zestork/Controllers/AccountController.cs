﻿using System;
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
            String userType = string.Empty;
            String referral = Request.QueryString["ref"];
            var userData = new LogOnModel();
            LoginService LoginService = new LoginService();
            if (id == "facebook")
            {                
                String code = Request.QueryString["code"];
                if (code == null)
                    Session["userType"] = Request.QueryString["userType"];
                else
                {
                    userType = Session["userType"].ToString();
                    Session.Remove("userType");
                }
                userData = LoginService.facebookLogin("http://" + Request.Url.Authority + "/Account/Login/facebook/", code, referral, userType);
            }            
            else if (id == "web")
            {
                String userName = Request.Form["userName"];
                String password = Request.Form["password"];
                String keepMeSignedIn = Request.Form["keepMeSignedInCheckBox"];
                if (keepMeSignedIn != null)
                    keepMeSignedIn = "true";
                else
                    keepMeSignedIn = "false";
                userData = LoginService.webLogin(userName, password, returnUrl, keepMeSignedIn);                
            }
            else if (id == "google")
            {
                if (Request.QueryString["access_token"] != null)
                {
                    string access_token = Request.QueryString["access_token"];
                }
                String code = Request.QueryString["code"];
                if (code == null)
                    Session["userType"] = Request.QueryString["userType"];
                else
                {
                    userType = Session["userType"].ToString();
                    Session.Remove("userType");
                }
                userData = LoginService.googleLogin("http://" + Request.Url.Authority + "/Account/Login/google", code, referral,userType);
            }
            else if (id == "linkedin")
            {
                String AbsoluteUri = Request.Url.AbsoluteUri;

                string oauth_token = Request.QueryString["oauth_token"];
                string oauth_verifier = Request.QueryString["oauth_verifier"];
                if (oauth_token != null && oauth_verifier != null)
                {
                    Session["userType"] = Request.QueryString["userType"];
                }
                else
                {
                    userType = Session["userType"].ToString();
                    Session.Remove("userType");
                }
                userData = LoginService.linkedinLogin("http://" + Request.Url.Authority + "/Account/Login/linkedin", AbsoluteUri, oauth_token, oauth_verifier, referral, userType);
                
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
                    session.addAttribute("type", AccountControllerMethods.getUserType(userData.User.Username));
                    bool isPersistent = false; // as of now we have only 1 type of login
                    TokenManager.CreateSession(session, isPersistent);
                    userData.User.guid = session.getID();                    
                    #endregion
                    
                    if (userData.User.ImageUrl == "NA")
                        userData.User.ImageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
                    Response.Redirect("/Account/welcome?guid=" + userData.User.guid + "&username=" + userData.User.Username + "&keepMeSignedIn=" + userData.User.keepMeSignedIn + "&type=" + session.getAttributeValue("type") + "&pass=true/#/");
                    //return View("Index", "User" , userData);
                    //HttpContext.Response.AppendHeader("Authorization", userData.User.guid);                    
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
                ImageUrl = "NA"
            };

            _db.Users.Add(user);

            if (req.referral != null && req.referral != "")
            {
                var referral = new RecommendedBy
                {
                    RecommendedFrom = req.referral,
                    RecommendedTo = req.userName
                };
                _db.RecommendedBies.Add(referral);
            }
            if (req.type == "client")
            {
                var clientDetails = new ClientDetails
                {
                    Username = req.userName,
                    CompanyName = req.CompanyName
                };
                _db.ClientDetails.Add(clientDetails);
            }
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

        public JsonResult logout(string id)
        {            
            try
            {
                var _db = new ZestorkContainer();

                CPSession retVal = TokenManager.getSessionInfo(id);
                if (retVal != null)
                {
                    string userName = retVal.getAttributeValue("userName");
                    Users user = _db.Users.SingleOrDefault(x => x.Username == userName);
                    if (user != null)
                    {
                        try
                        {
                            user.KeepMeSignedIn = "false";
                            _db.SaveChanges();
                        }
                        catch (DbEntityValidationException e)
                        {
                            dbContextException dbContextException = new CommonMethods.dbContextException();
                            dbContextException.logDbContextException(e);
                        }

                    }
                    TokenManager.removeSession(id);
                }
                else
                {
                    string username = Request.QueryString["username"].ToString();
                    if (username != null || username != "")
                    {
                        username = username.Split('/')[0];
                        Users user = _db.Users.SingleOrDefault(x => x.Username == username);
                        if (user != null && user.KeepMeSignedIn != null)
                        {
                            if (user.KeepMeSignedIn == "true")
                            {
                                try
                                {
                                    user.KeepMeSignedIn = "false";
                                    _db.SaveChanges();
                                }
                                catch (DbEntityValidationException e)
                                {
                                    dbContextException dbContextException = new CommonMethods.dbContextException();
                                    dbContextException.logDbContextException(e);
                                }
                            }
                        }
                    }                    
                }
                Response.Redirect("/");
                return Json(200, JsonRequestBehavior.AllowGet); // unreachable code
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new CommonMethods.dbContextException();
                dbContextException.logDbContextException(e);
                Response.Redirect("/");
                return Json(500, JsonRequestBehavior.AllowGet); // unreachable code
            }                        
        }

        public JsonResult isValidToken(string id)
        {
            var _db = new ZestorkContainer();

            string username = Request.QueryString["username"].ToString();
            username = username.Split('/')[0];

            string password = string.Empty;
            string key = Request.QueryString["key"].ToString();
            key = key.Replace(' ', '+');
            if (TokenManager.isValidSession(id))
            {
                CPSession retVal = TokenManager.getSessionInfo(id);
                string type = retVal.getAttributeValue("type");
                if(type=="client")
                    return Json(new { isValid = true, url = "http://" + Request.Url.Authority + "/Client" }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new { isValid = true, url = "http://" + Request.Url.Authority + "/secure"+type+"Clientcompare"+type=="client" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Users user = _db.Users.SingleOrDefault(x => x.Username == username);                
                if (user != null && user.KeepMeSignedIn != null)
                {
                    if (user.KeepMeSignedIn == "true")
                    {
                        Encryption.Encryption EncryptionObj = new Encryption.Encryption();
                        password = EncryptionObj.getDecryptionValue(key, user.guid);
                        if (password == user.Password)
                        {
                            CPSession session = new CPSession();
                            session.addAttribute("userName", user.Username);
                            session.addAttribute("type", AccountControllerMethods.getUserType(user.Username));
                            bool isPersistent = false; // as of now we have only 1 type of login
                            session.setID(id);
                            TokenManager.CreateSession(session, isPersistent);                            
                            return Json(new { isValid = true, url = "http://" + Request.Url.Authority + "/secure" }, JsonRequestBehavior.AllowGet);
                        }

                        return Json(new { isValid = false, url = "http://" + Request.Url.Authority + "/secure" }, JsonRequestBehavior.AllowGet);
                    }
                }
                
                return Json(new { isValid = false, url = "http://" + Request.Url.Authority + "/secure" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        public ActionResult welcome()
        {
            return View();
        }

        public JsonResult forgetPassword(string id)
        {
            var _db = new ZestorkContainer();
            String guid = Guid.NewGuid().ToString();

            if (_db.Users.Any(x => x.Username == id))
            {
                var forgetPasswordDataAlreadyExists = _db.ForgetPasswords.SingleOrDefault(x => x.Username == id);
                if (forgetPasswordDataAlreadyExists != null)
                    _db.ForgetPasswords.Remove(forgetPasswordDataAlreadyExists);

                var forgetPasswordData = new ForgetPassword
                {
                    Username = id,
                    guid = guid
                };
                _db.ForgetPasswords.Add(forgetPasswordData);

                try
                {
                    _db.SaveChanges();
                    forgetPasswordValidationEmail forgetPasswordValidationEmail = new forgetPasswordValidationEmail();
                    forgetPasswordValidationEmail.sendForgetPasswordValidationEmailMessage(id, guid, Request);
                }
                catch (DbEntityValidationException e)
                {
                    dbContextException.logDbContextException(e);
                    return Json(500, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(404, JsonRequestBehavior.AllowGet);
            }

                        
            return Json(200, JsonRequestBehavior.AllowGet);
        }

        public ActionResult validateForgetPassword()
        {
            var _db = new ZestorkContainer();
            String guid = Request.QueryString["guid"];
            String username = Request.QueryString["username"];

            if (!_db.Users.Any(x => x.Username == username))
            {
                Response.Redirect("/");
            }
            if(_db.ForgetPasswords.Any(x=>x.Username == username && x.guid == guid))
            {                
                var removeForgetPasswordData = _db.ForgetPasswords.SingleOrDefault(x => x.Username == username);
                _db.ForgetPasswords.Remove(removeForgetPasswordData);

                var UserData = _db.Users.SingleOrDefault(x => x.Username == username);
                UserData.Password = Guid.NewGuid().ToString();
                UserData.Locked = "false";
                try
                {
                    _db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    dbContextException dbContextException = new CommonMethods.dbContextException();
                    dbContextException.logDbContextException(e);                    
                }

                #region Session
                CPSession session = new CPSession();
                session.addAttribute("userName", username);
                bool isPersistent = false; // as of now we have only 1 type of login
                TokenManager.CreateSession(session, isPersistent);                
                #endregion

                Response.Redirect("/Account/welcome?guid=" + session.getID() + "&username=" + username + "/#/");
            }
            else
            {
                Response.Redirect("/#/forgetpassword");
            }
            return View("Home","Index");
        }
    }
}
