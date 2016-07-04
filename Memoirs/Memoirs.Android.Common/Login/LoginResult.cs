using System.Collections.Generic;

namespace Memoirs.Android.Common.Login
{
    public class LoginResult
    {
        public List<string> Errors { get; set; }
        public User User { get; set; }

    }
}