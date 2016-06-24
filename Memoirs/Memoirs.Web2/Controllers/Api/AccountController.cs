using System.Threading.Tasks;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.Identity;
using Memoirs.Web2.Models.Api;


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
    }
}
