using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memoirs.Common
{
    public class ApiException : Exception
    {
        public string ExplicitDescription { get; set; }
    }
}
