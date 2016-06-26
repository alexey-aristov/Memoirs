using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public LoginResult Login(string login, string password)
        {
            if (_loggedUser!=null)
            {
                return null;
            }
            var newUser = new LoginResult()
            {
                Login = login
            };
            return newUser;
        }

        public Task<LoginResult> LoginAsync(string login, string password)
        {
            return Task.FromResult(Login(login, password));
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