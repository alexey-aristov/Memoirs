using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Memoirs.Android.Common;
using Ninject;

namespace Memoirs.Android.App
{
    [Application(Label = "Memoirs", Icon = "@drawable/icon")]
    public class App:Application
    {
        public static ContextWrapper ContextWrapper { get; set; }

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
        }

        public override void OnCreate()
        {
            var kernel = new StandardKernel(new MemoirsNinjectModule());
            Dependencies.Container = kernel;
            ContextWrapper = this;
            base.OnCreate();
        }
    }
}