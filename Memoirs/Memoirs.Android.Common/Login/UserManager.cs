using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Android.Common.Login
{
    public class UserManager:IUserManager
    {
        public bool IsLogged()
        {
            throw new NotImplementedException();
        }

        public User User { get; set; }
    }
}
