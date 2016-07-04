using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Android.Common.Login
{
    public class UserManager:IUserManager
    {
        private ILoginProvider _loginProvider;
        public UserManager(ILoginProvider loginProvider)
        {
            _loginProvider = loginProvider;
        }

        public bool IsLogged()
        {
            return User != null;
        }

        public User User { get; set; }
        public LoginResult Login(string login, string password)
        {
            var loginRes=_loginProvider.LoginAsync(login, password).Result;
            return loginRes;
        }

        public void Logout()
        {
            _loginProvider.Logout();
        }
    }
}
