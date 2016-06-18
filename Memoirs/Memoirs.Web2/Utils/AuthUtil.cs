using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

namespace Memoirs.Web2.Utils
{
    public static class AuthUtil
    {
        public static AuthenticationTicket AuthUserWithToken(ApplicationUser user,
            IAuthenticationManager authenticationManager,
            ApplicationUserManager userManager)
        {
            ClaimsIdentity oAuthIdentity =
                userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType).Result;
            ClaimsIdentity cookiesIdentity = userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType).Result;

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            authenticationManager.SignIn(cookiesIdentity);
            return ticket;
        }
        private static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}