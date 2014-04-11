using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Collections.Specialized;
using Newtonsoft.Json;
using zestork.Models.DataWrapper;
using System.Configuration;

namespace zestork.Service
{
    public class imageService
    {
        public string imgurUploadImage(HttpPostedFileBase photo)
        {
            string result = "";
            string albumid = ConfigurationManager.AppSettings["imgurAlbumId"].ToString();            
            MemoryStream target = new MemoryStream();
            photo.InputStream.CopyTo(target);
            byte[] data = target.ToArray();
            using (var w = new WebClient())
            {
                var values = new NameValueCollection
                {
                {"image", Convert.ToBase64String((data))},
                {"album", albumid}
            };

                w.Headers.Add("Authorization", "Client-ID " + ConfigurationManager.AppSettings["imgurClientId"].ToString());
                byte[] response = w.UploadValues("https://api.imgur.com/3/upload.json", values);
                result = System.Text.Encoding.UTF8.GetString(response);
                imgurUploadImageResponse imgurUploadImageResponse = JsonConvert.DeserializeObject<imgurUploadImageResponse>(Convert.ToString(result));
                return imgurUploadImageResponse.data.link;
            }            
        }

    }
}