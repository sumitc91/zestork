using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Common.Infrastructure;
using zestork.Models;
using zestork.CommonMethods;
using System.Data.Entity.Validation;

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
            Users user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");
            user.skillTags = _db.UserSkills.Where(x => x.Username == userName).Select(x => x.Skill).ToList();
            if (user.ImageUrl == "NA")
                user.ImageUrl = "../../Resource/templates/afterLogin/web/img/demo/user-avatar.jpg";
            return Json(user, JsonRequestBehavior.AllowGet);
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

    }
}
