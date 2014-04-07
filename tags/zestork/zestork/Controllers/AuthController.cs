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

    }
}
