using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Memoirs.Identity;
using Memoirs.Web2.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace Memoirs.Web2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private SignInManager<ApplicationUser, string> _signInManager;
        private UserManager<ApplicationUser> _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        // GET: Account
        public ActionResult Login()
        {
            var userId = User.Identity.GetUserId();
            if(userId!=null)
                return RedirectToAction("Index", "Home");
            return View();
        }

        [AllowAnonymous]
        // GET: Account
        public async Task<ActionResult> Register()
        {
            return null;
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, false, false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index","Home");
                case SignInStatus.LockedOut:
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View();
            }
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            _signInManager.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}