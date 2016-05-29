using System.Data.Entity;
using Memoirs.Common;
using Memoirs.EntityFramework;
using Memoirs.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Memoirs.Web2.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Memoirs.Web2.App_Start.NinjectWebCommon), "Stop")]

namespace Memoirs.Web2.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

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
