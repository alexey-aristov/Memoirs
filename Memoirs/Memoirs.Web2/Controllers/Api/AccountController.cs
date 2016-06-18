using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using System.Web.Http;
using Memoirs.Common;
using Memoirs.Common.Identity;
using Memoirs.Web2.Models.Account;
using Memoirs.Web2.Utils;
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
        
        //[HttpGet]
        //public async Task<int> Logout()
        //{
        //    //review it!
        //    _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //    return 0;
        //}
    }
}
