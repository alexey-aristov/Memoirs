using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Memoirs.Web2.Controllers.Api
{
    public class PicturesController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Login()
        {
            var path = ConfigurationManager.AppSettings.Get("login_image_file");
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("image/gif");
            return result;
        }
    }
}
