using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models.DataContract;

namespace zestork.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/

        public ActionResult Index(String id)
        {
            string userId = id;            

            return View();
        }
        
    }
}
