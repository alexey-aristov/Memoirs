namespace Memoirs.Web2.Models.Api
{
    public class RegisterModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}