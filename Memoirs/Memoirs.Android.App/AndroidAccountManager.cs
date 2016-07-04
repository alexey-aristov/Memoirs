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
using Memoirs.Android.Common;
using Memoirs.Android.Common.Login;

namespace Memoirs.Android.App
{
    public class AndroidAccountManager:IDeviceAccountManager
    {
        // ReSharper disable once InconsistentNaming
        private readonly string APP_IDENTIFIER = "Memoirs";
        public bool AddAccount(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUser()
        {
            throw new NotImplementedException();
        }
    }
}