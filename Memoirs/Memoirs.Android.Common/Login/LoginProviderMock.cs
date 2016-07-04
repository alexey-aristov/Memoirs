using System.Threading.Tasks;

namespace Memoirs.Android.Common.Login
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

        public void Logout()
        {
            _loggedUser = null;
        }
    }
}