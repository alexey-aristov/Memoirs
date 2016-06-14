using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

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
        [HttpGet]
        public async Task<int> Login(string login, string password)
        {
            if (!ModelState.IsValid)
            {
                return -1;
            }
            
            var result = await _signInManager.PasswordSignInAsync(login, password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return 0;
                case SignInStatus.LockedOut:
                case SignInStatus.Failure:
                default:
                    throw new ApiException() {ExplicitDescription = "Invalid login attempt." };
            }
            return 0;
        }
        [HttpGet]
        public async Task<int> Logout()
        {
            _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return 0;
        }
    }
}
