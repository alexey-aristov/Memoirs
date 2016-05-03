using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.WebPages;
using Memoirs.Common;
using Memoirs.Common.Entities;

namespace Memoirs.Web.Controllers
{
    public class RestController : ApiController
    {
        IUnitOfWork _unitOfWork;

        public RestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public string Test()
        {
            return _unitOfWork.RecordsRepository.Get().First().Text;
        }
    }
}
