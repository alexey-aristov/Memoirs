using System.Collections.Generic;

namespace Memoirs.Web2.Models.Api
{
    public class RegisterResponceModel
    {
        public string Login { get; set; }
        public string RegisterStatus { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}