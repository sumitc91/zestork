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

            if(userInfo.imageUrl.Contains("../../"))
            {
                userInfo.imageUrl = "../" + userInfo.imageUrl;
            }
            userInfo.message = "Enter Your Password to Unlock !!";
            userInfo.guid = id;
            userInfo.userName = User.Username;
            userInfo.postUrl = "http://" + Request.Url.Authority + "Locked/unlock/" + id;
            User.Locked = "true";
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
            if (_db.Users.Any(x => x.Username == userName && x.Password == password))
            {
                Users user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");
                if (user != null)
                {
                    Users User = _db.Users.SingleOrDefault(x => x.Username == userName);
                    User.Locked = "false";
                    try
                    {
                        _db.SaveChanges();
                        try
                        {
                            String id = Request.QueryString["id"].ToString();
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

                         Response.Redirect("/" + user.FirstName + "-" + user.LastName + "?guid=" + session.getID() + "/#/");                     
                    }
                    catch (DbEntityValidationException e)
                    {
                        dbContextException dbContextException = new dbContextException();
                        dbContextException.logDbContextException(e);
                        throw;
                    }

                }
                LockedScreenModel userInfo = new LockedScreenModel();
                Users UserDetails = _db.Users.SingleOrDefault(x => x.Username == userName);
                userInfo.firstName = UserDetails.FirstName;
                userInfo.lastName = UserDetails.LastName;
                if (UserDetails.ImageUrl == "NA" || UserDetails.ImageUrl == null)
                    userInfo.imageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
                else
                    userInfo.imageUrl = UserDetails.ImageUrl;

                if (userInfo.imageUrl.Contains("../../"))
                {
                    userInfo.imageUrl = "../" + userInfo.imageUrl;
                }
                userInfo.message = "Wrong Username or Password !!";
                return View("index");
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
                userInfo.message = "Wrong Username or Password !!";
                return View("index",userInfo);
            }            
        }
    }
}
