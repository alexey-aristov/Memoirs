using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Ninject;
using Ninject.Web.Common;
using Owin;

[assembly: OwinStartup(typeof(Memoirs.Web2.OwinStartup))]
namespace Memoirs.Web2
{
    public class OwinStartup
    {


        // private IKernel kernel = null;
        public void Configuration(IAppBuilder app)
        {
            //kernel = CreateKernel();
            //app.CreatePerOwinContext(AppDataContext.Create);
            //app.UseNinjectMiddleware(() => NinjectWebCommon.CreateKernel());
            //app.CreatePerOwinContext(AppDataContext.Create);
            //app.CreatePerOwinContext(IdentityContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            //app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);


            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                //Provider = new CookieAuthenticationProvider
                //{
                //    // Enables the application to validate the security stamp when the user logs in.
                //    // This is a security feature which is used when you change a password or add an external login to your account.  
                //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //        validateInterval: TimeSpan.FromMinutes(30),
                //        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //}
            });
        }

        
    }
}