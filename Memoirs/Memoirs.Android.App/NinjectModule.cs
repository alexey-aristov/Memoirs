using Memoirs.Android.Common;
using Memoirs.Android.Common.Login;
using Memoirs.Android.Common.Records;
using Ninject.Modules;

namespace Memoirs.Android.App
{
    public class MemoirsNinjectModule: NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ILoginProvider>().To<WebApiLoginProvider>();
            Kernel.Bind<IRecordsProvider>().To<WebApiRecordsProvider>();
            Kernel.Bind<IUserManager>().To<UserManager>().InSingletonScope();
            Kernel.Bind<IConnectionChecker>().To<AndroidConnectionChecker>();
        }
    }
}