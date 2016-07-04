using System.Threading.Tasks;

namespace Memoirs.Android.Common.Login
{
    public interface ILoginProvider
    {
        LoginResult Login(string login, string password);
        Task<LoginResult> LoginAsync(string login, string password);
        void Logout();
    }
}