﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using zestork.Models.DataContract;
using SunPower.Common.Infrastructure.Logger;
using System.Reflection;
using Newtonsoft.Json;
using zestork.Models;
using System.Data.Entity.Validation;

namespace zestork.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private ILogger logger = new Logger(Convert.ToString(MethodBase.GetCurrentMethod().DeclaringType));
       
        //public AccountController(ZestorkContainer ZestorkContainer)
        //{
        //    _db = ZestorkContainer;
        //}

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAccount(CreateAccountRequest req)
        {           
            //logger.Info("new account creation request : " + JsonConvert.SerializeObject(req));           
            logger.Info("new account creation request");            
            var _db = new ZestorkContainer();

            //if user already exists
            if(_db.Users.Any(x=>x.Username==req.userName))
                return Json(new { code="402",msg="User Already Exists" });

            String ID = Guid.NewGuid().ToString();
            var user = new Users
            {
                Username = req.userName,
                Password = req.password,
                Source = req.source,
                isActive = "false",
                Type = req.type,                
                Uid = "abcde"
            };

            _db.Users.Add(user);
            
            var ValidateUserKey = new ValidateUserKey
            {
                 Username = req.userName,
                 guid = ID
            };

            _db.ValidateUserKeys.Add(ValidateUserKey);

            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    logger.Info("Entity of type \"" + eve.Entry.Entity.GetType().Name + "\" in state \"" + eve.Entry.State + "\" has the following validation errors:");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        logger.Info("- Property: \"" + ve.PropertyName + "\", Error: \"" + ve.ErrorMessage + "\"");
                    }
                }
                throw;
            }

            return Json(new { code="200",msg="successfully created account" });
        }

        public JsonResult showData()
        {
            var _db = new ZestorkContainer();
            Users User = _db.Users.SingleOrDefault(x=>x.Username== "sumitchourasia91@gmail.com");
            ValidateUserKey key = _db.ValidateUserKeys.SingleOrDefault(x => x.Username == "sumitchourasia91@gmail.com");
            return Json(User, JsonRequestBehavior.AllowGet);

        }
    }
}
