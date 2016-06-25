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
using Ninject;

namespace Memoirs.Android.App
{
    [Application]
    public class App:Application
    {
        public static IKernel Container { get; set; }

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
        }

        public override void OnCreate()
        {
            var kernel = new Ninject.StandardKernel(new MemoirsNinjectModule());

            App.Container = kernel;

            base.OnCreate();
        }
    }
}