using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Xml.Linq;
using System.IO;
using System.Collections.Specialized;
using zestork.Service;
using zestork.Models;
using zestork.Common.Infrastructure;
using System.Data.Entity.Validation;
using zestork.CommonMethods;

namespace zestork.Controllers
{
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }
    
        [HttpPost]
        public JsonResult UploadToAlbum(String id)
        {
            var _db = new ZestorkContainer();
            CPSession retVal = TokenManager.getSessionInfo(id);
            string userName = retVal.getAttributeValue("userName");
            var user = _db.Users.SingleOrDefault(x => x.Username == userName);
            if (user != null)
            {
                HttpPostedFileBase photo = Request.Files["photo"];
                imageService imageService = new Service.imageService();
                string uploadedImageLink = imageService.imgurUploadImage(photo);
                user.ImageUrl = uploadedImageLink;
                try
                {
                    _db.SaveChanges();
                    Response.Redirect("/secure#/edit");
                    return Json(500, JsonRequestBehavior.AllowGet);
                }
                catch (DbEntityValidationException e)
                {
                    dbContextException dbContextException = new CommonMethods.dbContextException();
                    dbContextException.logDbContextException(e);
                    return Json("Internal Server Error Occured !!", JsonRequestBehavior.AllowGet);
                }
            }
            else
                return Json("invalid username !!!");

        }

        public JsonResult album()
        {

            string result = "";
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                {"ids[]", "5FBgDJI"},
                {"cover", "5FBgDJI"},
                {"layout", "grid"},
                {"title", "Title"},
                {"description", "This is the caption of the image"}
            };

                w.Headers.Add("Authorization", "Client-ID dac37a6b08b4974");
                byte[] response = w.UploadValues("https://api.imgur.com/3/album/{0}.json", values);
                System.Console.WriteLine(response);
                result = System.Text.Encoding.UTF8.GetString(response);
            }

            return Json(result, JsonRequestBehavior.AllowGet);


        }

        public JsonResult getalbumdetails()
        {

            string result = "";
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                {"id", "H9RNG"}
                
            };
                w.Headers.Add("Authorization", "Client-ID dac37a6b08b4974");
                byte[] response = w.UploadValues("https://api.imgur.com/3/album/{0}.json", values);
                System.Console.WriteLine(response);
                result = System.Text.Encoding.UTF8.GetString(response);
            }

            return Json(result, JsonRequestBehavior.AllowGet);


        }
    }
}
