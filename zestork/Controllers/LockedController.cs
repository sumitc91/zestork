using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models;
using zestork.Common.Infrastructure;
using zestork.CommonMethods;
using System.Data.Entity.Validation;
using zestork.Models.DataContract;

namespace zestork.Controllers
{
    public class LockedController : Controller
    {
        //
        // GET: /Locked/

        public ActionResult Index(string id)
        {
            var _db = new ZestorkContainer();
            CPSession retVal = TokenManager.getSessionInfo(id);
            string userName = retVal.getAttributeValue("userName");
            Users User = _db.Users.SingleOrDefault(x => x.Username == userName);
            LockedScreenModel userInfo = new LockedScreenModel();
            userInfo.firstName = User.FirstName;
            userInfo.lastName = User.LastName;
            if (User.ImageUrl == "NA" || User.ImageUrl == null)
                userInfo.imageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
            else
                userInfo.imageUrl = User.ImageUrl;

            if (userInfo.imageUrl.Contains("../../"))
            {
                userInfo.imageUrl = "../" + userInfo.imageUrl;
            }
            userInfo.message = "";
            userInfo.guid = id;
            userInfo.userName = User.Username;
            userInfo.postUrl = "http://" + Request.Url.Authority + "Locked/unlock/" + id;
            User.Locked = "true";

            UserPageSetting pageSetting = _db.UserPageSettings.SingleOrDefault(x => x.Username == userName);
            if (pageSetting != null)
                userInfo.PageThemeColor = "theme-" + pageSetting.PageThemeColor;
            else
                userInfo.PageThemeColor = "";

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new dbContextException();
                dbContextException.logDbContextException(e);
                throw;
            }
            return View(userInfo);
        }

        public ActionResult unlock()
        {
            var _db = new ZestorkContainer();

            string userName = Request.QueryString["username"].ToString();
            String password = Request.QueryString["password"].ToString();
            String id = Request.QueryString["id"].ToString();
            if (_db.Users.Any(x => x.Username == userName && x.Password == password))
            {
                Users user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");
                if (user != null)
                {
                    user.Locked = "false";
                    try
                    {
                        _db.SaveChanges();
                        try
                        {
                            CPSession retVal = TokenManager.getSessionInfo(id);
                            TokenManager.removeSession(id);// remove session if available.
                        }
                        catch (Exception)
                        {
                            //if session is not available.. leave it.                            
                        }

                        #region Session
                        CPSession session = new CPSession();
                        session.addAttribute("userName", userName);
                        bool isPersistent = false; // as of now we have only 1 type of login
                        TokenManager.CreateSession(session, isPersistent);
                        #endregion

                        Response.Redirect("/Account/welcome?guid=" + session.getID() + "&username=" + userName + "/#/");
                    }
                    catch (DbEntityValidationException e)
                    {
                        dbContextException dbContextException = new dbContextException();
                        dbContextException.logDbContextException(e);
                        throw;
                    }

                }

                Users User = _db.Users.SingleOrDefault(x => x.Username == userName);
                LockedScreenModel userInfo = new LockedScreenModel();
                userInfo.firstName = User.FirstName;
                userInfo.lastName = User.LastName;
                if (User.ImageUrl == "NA" || User.ImageUrl == null)
                    userInfo.imageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
                else
                    userInfo.imageUrl = User.ImageUrl;

                if (userInfo.imageUrl.Contains("../../"))
                {
                    userInfo.imageUrl = "../" + userInfo.imageUrl;
                }
                userInfo.message = "Enter Your Password to Unlock !!";
                userInfo.guid = id;
                userInfo.userName = User.Username;
                userInfo.postUrl = "http://" + Request.Url.Authority + "Locked/unlock/" + id;
                User.Locked = "true";

                UserPageSetting pageSetting = _db.UserPageSettings.SingleOrDefault(x => x.Username == userName);
                if (pageSetting != null)
                    userInfo.PageThemeColor = "theme-" + pageSetting.PageThemeColor;
                else
                    userInfo.PageThemeColor = "";

                userInfo.message = "Inactive Account.";
                return View("index", userInfo);
            }
            else
            {
                Users User = _db.Users.SingleOrDefault(x => x.Username == userName);
                LockedScreenModel userInfo = new LockedScreenModel();
                userInfo.firstName = User.FirstName;
                userInfo.lastName = User.LastName;
                if (User.ImageUrl == "NA" || User.ImageUrl == null)
                    userInfo.imageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
                else
                    userInfo.imageUrl = User.ImageUrl;

                if (userInfo.imageUrl.Contains("../../"))
                {
                    userInfo.imageUrl = "../" + userInfo.imageUrl;
                }

                userInfo.guid = id;
                userInfo.userName = User.Username;
                userInfo.postUrl = "http://" + Request.Url.Authority + "Locked/unlock/" + id;
                User.Locked = "true";

                UserPageSetting pageSetting = _db.UserPageSettings.SingleOrDefault(x => x.Username == userName);
                if (pageSetting != null)
                    userInfo.PageThemeColor = "theme-" + pageSetting.PageThemeColor;
                else
                    userInfo.PageThemeColor = "";

                userInfo.message = "invalid entry !! try again.";
                return View("index", userInfo);
            }
        }

        public JsonResult forgetPassword(string id)
        {
            var _db = new ZestorkContainer();
            String guid = Guid.NewGuid().ToString();
            String guidSession = Request.QueryString["guidSession"].ToString();

            if (_db.Users.Any(x => x.Username == id))
            {
                AccountController AccountController = new AccountController();
                Users UserDetail = _db.Users.SingleOrDefault(x => x.Username == id);
                if (UserDetail.Source == "facebook")
                {
                    UserDetail.Locked = "false";
                    UserDetail.Password = guid;
                    try
                    {
                        _db.SaveChanges();
                        try
                        {
                            TokenManager.removeSession(guidSession);
                        }
                        catch (Exception)
                        {
                            //no need to remove the session if it is invalid...                          
                        }
                                               
                        return Json(210, JsonRequestBehavior.AllowGet); // unreachable code //210 for facebook..
                    }
                    catch (DbEntityValidationException e)
                    {
                        dbContextException dbContextException = new CommonMethods.dbContextException();
                        dbContextException.logDbContextException(e);                        
                        return Json(500, JsonRequestBehavior.AllowGet); // unreachable code
                    }                             
                }
                else
                {

                    if (UserDetail != null)
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
                            dbContextException dbContextException = new CommonMethods.dbContextException();
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
            }
            else
            {
                return Json("Username doesn't exists..", JsonRequestBehavior.AllowGet);
            }
        }
    }
}
