using System.Threading.Tasks;
using Memoirs.Android.App.Account;

namespace Memoirs.Android.App.Login
{
    public interface ILoginProvider
    {
        LoginResult Login(string login, string password);
        Task<LoginResult> LoginAsync(string login, string password);
        void Logout();
    }
}