using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using zestork.Models;
using zestork.Common.Infrastructure;

namespace zestork.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public ActionResult Index(String id)
        {
            string userId = id;
            
            return View();
        }

        public JsonResult details(string id)
        {
            IEnumerable<string> headerValues = Request.Headers.GetValues("Authorization");
            String guid = headerValues.FirstOrDefault();
            guid = guid.Replace("/","");
            CPSession retVal = TokenManager.getSessionInfo(guid);
            string userName = retVal.getAttributeValue("userName");

            var _db = new ZestorkContainer();
            Users user = _db.Users.SingleOrDefault(x => x.Username == userName && x.isActive == "true");
            return Json(user, JsonRequestBehavior.AllowGet);
        }

    }
}
