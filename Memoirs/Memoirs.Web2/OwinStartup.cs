using System;
using System.Threading.Tasks;
using System.Web;
using Memoirs.Common;
using Memoirs.Web2.Utils;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Ninject;
using Owin;

[assembly: OwinStartup(typeof(Memoirs.Web2.OwinStartup))]
namespace Memoirs.Web2
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            var oAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/Account/LoginUrlEncoded"),
                Provider = new AppOAuthProvider(),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true,

            };
            app.UseOAuthBearerTokens(oAuthOptions);


            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider()
                {
                    OnApplyRedirect = ctx =>
                    {
                        string apiPath = VirtualPathUtility.ToAbsolute("~/api/");
                        if (!ctx.Request.Uri.LocalPath.StartsWith(apiPath))
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                    }
                },
                
                //Provider = new CookieAuthenticationProvider
                //{
                //    // Enables the application to validate the security stamp when the user logs in.
                //    // This is a security feature which is used when you change a password or add an external login to your account.  
                //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //        validateInterval: TimeSpan.FromMinutes(30),
                //        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //}
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            var appSettingsProvider = NinjectWebCommon.Bootstrapper.Kernel.Get<IAppSettingsProvider>();

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions
            {
                ClientId = appSettingsProvider.OauthGoogleClientId,
                ClientSecret = appSettingsProvider.OauthGoogleClientSecret,
                CallbackPath = new PathString("/api/account/login/google"),
                Provider = new GoogleOAuth2AuthenticationProvider
                {
                    OnAuthenticated = context =>
                    {
                        // Retrieve the OAuth access token to store for subsequent API calls
                        string accessToken = context.AccessToken;

                        // Retrieve the name of the user in Google
                        string googleName = context.Name;

                        // Retrieve the user's email address
                        string googleEmailAddress = context.Email;

                        // You can even retrieve the full JSON-serialized user
                        var serializedUser = context.User;
                        return Task.FromResult<object>(null);
                    }
                }
            });

        }
    }
}