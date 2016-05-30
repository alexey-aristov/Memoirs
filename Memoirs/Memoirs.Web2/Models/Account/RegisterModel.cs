using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memoirs.Web2.Models.Account
{
    public class RegisterModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}