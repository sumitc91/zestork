using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models.DataContract;

namespace zestork.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccountRequest req)
        {
            return Json(new { msg = "Successfully added " + req.userName });
        }
    }
}
