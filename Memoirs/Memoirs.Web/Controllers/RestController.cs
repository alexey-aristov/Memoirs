using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.Entities;
using Memoirs.Web.Models;

namespace Memoirs.Web.Controllers
{
    public class RestController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public RestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public string Test()
        {
            return _unitOfWork.RecordsRepository.Get().First().Text;
        }

        [HttpGet]
        public IList<RecordModel> GetRecords()
        {
           return _unitOfWork.RecordsRepository.Get().Select(a=>new RecordModel()
           {
               Text = a.Text,
               DateCreated = a.DateCreated,
               Description = a.Description,
               Id = a.Id,
               IsDeleted = a.IsDeleted,
               Label = a.Label
           }).ToList();
        }

        public AddRecordResultModel PostRecord(RecordModel model)
        {
            _unitOfWork.RecordsRepository.Add(new SimpleRecord()
            {
                Label = model.Label,
                Text = model.Text,
                DateCreated = DateTime.Now,
                Description = model.Description,
                IsDeleted = false
            });
            _unitOfWork.Save();

            return new AddRecordResultModel()
            {
                Record = model
            };

        }

        public HttpResponseMessage GetLoginImage()
        {
            //Image img = Image.FromFile(ConfigurationManager.AppSettings.Get("login_image_file"));
            //MemoryStream ms = new MemoryStream();
            //img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            //HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //result.Content = new ByteArrayContent(ms.ToArray());
            //result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/gif");
            //return result;

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
