using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;

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
        
    }
}
