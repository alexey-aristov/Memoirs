using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.Identity;
using Memoirs.Web2.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Memoirs.Web2.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IAuthenticationManager _authenticationManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationManager = authenticationManager;
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
        public Task<ActionResult> Register()
        {
            return null;
        }
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ExternalLogin(string provider)
        {
            _authenticationManager.Challenge(new AuthenticationProperties()
            {
                RedirectUri = Url.Action("ExternalLoginCallback")
            },provider);
            return new HttpUnauthorizedResult();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> ExternalLoginCallback()
        {
            var loginInfo = await _authenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login","Account");
            }
            var result = await _signInManager.ExternalSignInAsync(loginInfo, isPersistent: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                    return View(new ExternalLoginModel()
                    {
                        Login = loginInfo.Email,
                        Email = loginInfo.Email,
                        ValidationErrors = new List<string>()
                    });
                    
                default:
                   return RedirectToAction("Login");
                   
            }
            return null;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> ExternalLoginCallback(ExternalLoginModel model)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            model.ValidationErrors=new List<string>();
            if (ModelState.IsValid)
            {
                var info = await _authenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return null;//todo
                    // return View("ExternalLoginFailure");
                }

                var newUser = new ApplicationUser()
                {
                    UserName = model.Login,
                    Email = model.Email
                };
                IdentityResult createResult;
                if (model.IsPasswordSet)
                {
                    createResult = await _userManager.CreateAsync(newUser, model.Password);
                }
                else
                {
                    createResult = await _userManager.CreateAsync(newUser);
                }
                
                if (createResult.Succeeded)
                {
                    createResult = await _userManager.AddLoginAsync(newUser.Id, info.Login);
                    if (createResult.Succeeded)
                    {
                        await _signInManager.SignInAsync(newUser, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    model.ValidationErrors.AddRange(createResult.Errors);
                }
            }
            model.ValidationErrors.AddRange( ModelState.Values.SelectMany(a=>a.Errors).Select(a=>a.ErrorMessage));
            return View(model);
        }
    }
}