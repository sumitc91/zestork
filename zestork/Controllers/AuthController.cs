﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Common.Infrastructure;
using zestork.Models;
using zestork.CommonMethods;
using System.Data.Entity.Validation;
using zestork.Models.DataContract;

namespace zestork.Controllers
{
    public class AuthController : Controller
    {
        //
        // GET: /Auth/

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult details(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            detailsEditUserPage detailsEditUserPage = new detailsEditUserPage();
            Users user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");

            UserPageSetting pageSetting = _db.UserPageSettings.SingleOrDefault(x=>x.Username == userName);
            if (user.Locked == "true")
            {
                //Response.Redirect("/Locked/index/"+guid);
            }
            detailsEditUserPage.Username = user.Username;
            detailsEditUserPage.isActive = user.isActive;
            detailsEditUserPage.Type = user.Type;
            detailsEditUserPage.Source = user.Source;
            detailsEditUserPage.guid = user.guid;
            detailsEditUserPage.FirstName = user.FirstName;
            detailsEditUserPage.LastName = user.LastName;
            detailsEditUserPage.ImageUrl = user.ImageUrl;
            detailsEditUserPage.gender = user.gender;
            detailsEditUserPage.Locked = Convert.ToBoolean(user.Locked);

            if (pageSetting.PageThemeColor != null)
                detailsEditUserPage.PageThemeColor = "theme-"+pageSetting.PageThemeColor;
            else
                detailsEditUserPage.PageThemeColor = "";

            if (pageSetting.LayoutWidth != null)
                detailsEditUserPage.pageLayoutWidth = pageSetting.LayoutWidth;
            else
                detailsEditUserPage.pageLayoutWidth = "container-fluid";

            detailsEditUserPage.skillTags = _db.UserSkills.Where(x => x.Username == userName).Select(x => x.Skill).ToList();
            if (detailsEditUserPage.ImageUrl == "NA" || detailsEditUserPage.ImageUrl == null)
                detailsEditUserPage.ImageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
            return Json(detailsEditUserPage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult userTypeInfoAvailable()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            bool userTypeInfoAvailableResponse = false;
            var _db = new ZestorkContainer();
            Users user = _db.Users.SingleOrDefault(x => x.Username == userName);
            if (user.Type == "NA")
                userTypeInfoAvailableResponse = false;
            else
                userTypeInfoAvailableResponse = true;
            return Json(userTypeInfoAvailableResponse, JsonRequestBehavior.AllowGet);
        }

        public JsonResult submitUserTypeInfo(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");
            
            var _db = new ZestorkContainer();
            Users User = _db.Users.SingleOrDefault(x => x.Username == userName);
                User.Type = id;
                try
                {
                    _db.SaveChanges();                    
                }
                catch (DbEntityValidationException e)
                {
                    dbContextException dbContextException = new CommonMethods.dbContextException();
                    dbContextException.logDbContextException(e);
                    return Json(500, JsonRequestBehavior.AllowGet);
                }
            return Json(200, JsonRequestBehavior.AllowGet);
        }

        public JsonResult addUsersTag(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            var userSkillTag = new UserSkills
            {
                Username = userName,
                Skill = id,
                Rating = "0"
            };
            _db.UserSkills.Add(userSkillTag);
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new CommonMethods.dbContextException();
                dbContextException.logDbContextException(e);
                return Json(500, JsonRequestBehavior.AllowGet);
            }
            return Json(200, JsonRequestBehavior.AllowGet);
        }

        public JsonResult deleteUsersTag(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");
            var _db = new ZestorkContainer();
            var userTagToBeRemoved = _db.UserSkills.SingleOrDefault(x => x.Username == userName && x.Skill == id);
            _db.UserSkills.Remove(userTagToBeRemoved);
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new CommonMethods.dbContextException();
                dbContextException.logDbContextException(e);
                return Json(500, JsonRequestBehavior.AllowGet);
            }
            return Json(200, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckIfUserNewPasswordIsSet()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");
            var _db = new ZestorkContainer();
            var Users = _db.Users.SingleOrDefault(x => x.Username == userName);
            if(Users.Password.Length == 36)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult changeUserPassword(changePasswordRequest req)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            Users User = _db.Users.SingleOrDefault(x => x.Username == userName);
            User.Password = req.password;
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new CommonMethods.dbContextException();
                dbContextException.logDbContextException(e);
                return Json("500");
            }
            return Json("200");
        }

        public JsonResult getUserPageThemeData()
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            var UserPageTheme = _db.UserPageSettings.SingleOrDefault(x => x.Username == userName);
            
            return Json(UserPageTheme, JsonRequestBehavior.AllowGet);
        }

        public JsonResult submitUserPageThemeColor(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            var UserPageTheme = _db.UserPageSettings.SingleOrDefault(x => x.Username == userName);
            if (UserPageTheme == null)
            {
                UserPageTheme = new UserPageSetting
                {
                     Username = userName,
                     PageThemeColor = id
                };
                _db.UserPageSettings.Add(UserPageTheme);
            }
            else
            {
                UserPageTheme.PageThemeColor = id;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new CommonMethods.dbContextException();
                dbContextException.logDbContextException(e);
                return Json(500, JsonRequestBehavior.AllowGet);
            }
            return Json(200, JsonRequestBehavior.AllowGet);
        }

        public JsonResult submitUserPageLayoutWidth(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/", "");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            var UserPageTheme = _db.UserPageSettings.SingleOrDefault(x => x.Username == userName);
            if (UserPageTheme == null)
            {
                UserPageTheme = new UserPageSetting
                {
                    Username = userName,
                    LayoutWidth = id
                };
                _db.UserPageSettings.Add(UserPageTheme);
            }
            else
            {
                UserPageTheme.LayoutWidth = id;
            }

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                dbContextException dbContextException = new CommonMethods.dbContextException();
                dbContextException.logDbContextException(e);
                return Json(500, JsonRequestBehavior.AllowGet);
            }
            return Json(200, JsonRequestBehavior.AllowGet);
        }
    }
}
