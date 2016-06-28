using System.Collections.Generic;

namespace Memoirs.Android.App.Login
{
    public class LoginResult
    {
        public List<string> Errors { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

    }
}