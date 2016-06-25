using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Memoirs.Android.App.Login;
using Memoirs.Android.App.Records;
using Ninject.Modules;

namespace Memoirs.Android.App
{
    public class MemoirsNinjectModule: NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<ILoginProvider>().To<LoginProviderMock>();
            Kernel.Bind<IRecordsProvider>().To<RecordsProviderMock>();
        }
    }
}