using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Memoirs.Android.Common.Login;

namespace Memoirs.Android.Common
{
    public interface IDeviceAccountManager
    {
        bool AddAccount(User user);
        User GetUser();

    }
}
