using System;
using System.Data.Entity;
using System.Web;
using Memoirs.Common;
using Memoirs.Common.EntityFramework;
using Memoirs.Common.Identity;
using Memoirs.Web2;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Memoirs.Web2
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IUnitOfWork>().To<UnitOfWorkEf>().InRequestScope();
            //kernel.Bind<DbContext>().ToMethod(c => HttpContext.Current.GetOwinContext().Get<AppDataContext>());
            kernel.Bind<DbContext>().To<AppDataContext>().InRequestScope();
            kernel.Bind<IUserStore<ApplicationUser>>().To<UserStore<ApplicationUser>>().InRequestScope();

            kernel.Bind<ApplicationSignInManager>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            kernel.Bind<IOwinContext>().ToMethod(a => HttpContext.Current.GetOwinContext());
            kernel.Bind<IAuthenticationManager>().ToMethod(a => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
            //kernel.Bind(IAuthenticationManager)

            //kernel.Bind<IAuthenticationManager>().ToMethod(
            //        c => HttpContext.Current.GetOwinContext().Authentication).InRequestScope();

            //kernel.Bind<SignInManager<ApplicationUser, string>>().ToMethod(
            //    c => HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>()).InRequestScope();
            //kernel.Bind<UserManager<ApplicationUser>>().ToMethod(
            //    c => HttpContext.Current.GetOwinContext().Get<ApplicationUserManager>()).InRequestScope();
        }
    }
}
