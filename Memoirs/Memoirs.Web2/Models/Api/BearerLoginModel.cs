using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memoirs.Web2.Models.Api
{
    public class BearerLoginModel
    {
        public string GrantType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}