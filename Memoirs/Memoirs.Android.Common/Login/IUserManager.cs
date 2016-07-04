namespace Memoirs.Android.Common.Login
{
    public interface IUserManager
    {
        bool IsLogged();
        User User { get; set; }
    }
}
