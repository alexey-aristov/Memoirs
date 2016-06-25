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
    public class LoginProviderMock:ILoginProvider
    {
        private static User _loggedUser=null;
        public User Login(string login, string password)
        {
            if (_loggedUser!=null)
            {
                return null;
            }
            var newUser = new User()
            {
                Login = login,
                Id = new Guid().ToString()
            };
            return newUser;
        }

        public User GetCurrentUser()
        {
            return _loggedUser;
        }


        public void Logout()
        {
            _loggedUser = null;
        }
    }
}