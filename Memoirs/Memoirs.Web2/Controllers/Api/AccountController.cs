using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.Identity;
using Memoirs.Web2.Models.Api;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Memoirs.Web2.Controllers.Api
{
    public class AccountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ILogger _logger;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, ILogger logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        [Route("api/account/register")]
        public async Task<RegisterResponceModel> Register(RegisterModel model)
        {
            //todo: validate model
            var newUser = new ApplicationUser()
            {
                UserName = model.Login,
                Email = model.Email
            };
            var createResult = await _userManager.CreateAsync(newUser, model.Password);
            if (createResult.Succeeded)
            {
                await _signInManager.SignInAsync(newUser, false, false);
                return new RegisterResponceModel()
                {
                    Login = model.Login,
                    RegisterStatus = "registered"
                };
                //todo send email to confirm
            }

            return new RegisterResponceModel()
            {
                Login = model.Login,
                RegisterStatus = "error",
                Errors = createResult.Errors
            };
        }

        [HttpPost]
        [Route("api/account/login")]
        public async Task<HttpResponseMessage> Login(BearerLoginModel model)
        {
            
            string apiUrl = HttpContext.Current.Request.Url.Scheme+"://"+ HttpContext.Current.Request.Url.Authority+ "/api/Account/LoginUrlEncoded";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                if (model == null)
                {
                    model = new BearerLoginModel();
                }
                HttpContent content = new FormUrlEncodedContent(new []
                {
                    new KeyValuePair<string, string>("grant_type", string.IsNullOrEmpty(model.GrantType)?"":model.GrantType),
                    new KeyValuePair<string, string>("username", string.IsNullOrEmpty(model.UserName)?"":model.UserName),
                    new KeyValuePair<string, string>("password", string.IsNullOrEmpty(model.Password)?"":model.Password),
                });
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                HttpContext.Current.Response.StatusCode = (int)response.StatusCode;
                dynamic jsonResponse = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);

                return new HttpResponseMessage()
                {
                    StatusCode = response.StatusCode,
                    Content = new ObjectContent(typeof(object), jsonResponse, new JsonMediaTypeFormatter())
                };
            }
            throw new NotSupportedException();
        }

    }
}
