using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Memoirs.Web2.Models.Account
{
    public class ExternalLoginModel
    {
        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }
        [Required(ErrorMessage = "EmailIsRequired")]
        public string Email { get; set; }
        public List<string> ValidationErrors { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public bool IsPasswordSet { get; set; }
    }
}