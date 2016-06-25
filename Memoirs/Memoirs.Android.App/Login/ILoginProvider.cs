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
using Memoirs.Android.App.Account;

namespace Memoirs.Android.App.Login
{
    public interface ILoginProvider
    {
        User Login(string login, string password);
        User GetCurrentUser();
        void Logout();
    }
}