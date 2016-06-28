using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Memoirs.Android.App.Account;
using Ninject;

namespace Memoirs.Android.App
{
    [Application(Label = "Memoirs", Icon = "@drawable/icon")]
    public class App:Application
    {
        public static IKernel Container { get; set; }
        public static ContextWrapper ContextWrapper { get; set; }
        public static User CurrentUser { get; set; }

        public App(IntPtr h, JniHandleOwnership jho) : base(h, jho)
        {
        }

        public override void OnCreate()
        {
            var kernel = new StandardKernel(new MemoirsNinjectModule());
            Container = kernel;
            ContextWrapper = this;
            base.OnCreate();
        }
    }
}