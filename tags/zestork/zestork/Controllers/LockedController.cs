using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models;
using zestork.Common.Infrastructure;
using zestork.CommonMethods;
using System.Data.Entity.Validation;

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
            return View();
        }

    }
}
