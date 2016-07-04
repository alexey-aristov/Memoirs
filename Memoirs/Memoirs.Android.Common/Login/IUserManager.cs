using System.Diagnostics.Contracts;

namespace Memoirs.Android.Common.Login
{
    public interface IUserManager
    {
        bool IsLogged();
        User User { get; set; }
        LoginResult Login(string login, string password);
        void Logout();
    }
}
