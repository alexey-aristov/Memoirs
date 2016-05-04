using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.Video;

namespace Memoirs.Web.Controllers
{
    public class RestController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoginVideoProvider _loginVideoProvider;

        public RestController(IUnitOfWork unitOfWork,ILoginVideoProvider loginVideoProvider)
        {
            _unitOfWork = unitOfWork;
            _loginVideoProvider = loginVideoProvider;
        }

        [HttpGet]
        public string Test()
        {
            return _unitOfWork.RecordsRepository.Get().First().Text;
        }

        [HttpGet]
        public HttpResponseMessage Video()
        {
            var response = Request.CreateResponse();
            response.Content = new PushStreamContent((stream, httpContent, transportContext) =>
            {
                _loginVideoProvider.WriteToStream(stream);
            },
            _loginVideoProvider.GetMediaType());
            return response;
        }
        
    }
}
