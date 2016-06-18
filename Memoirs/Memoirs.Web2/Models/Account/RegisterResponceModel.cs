using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memoirs.Web2.Models.Account
{
    public class RegisterResponceModel
    {
        public string Login { get; set; }
        public string RegisterStatus { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}