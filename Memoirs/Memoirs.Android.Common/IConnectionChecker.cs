using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Android.Common
{
    public interface IConnectionChecker
    {
        bool IsInternetAvailable();
    }
}
