using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;

namespace zestork.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));

        public ActionResult Index()
        {
            logger.Info("Home-Index");
            return View();
        }

    }
}
