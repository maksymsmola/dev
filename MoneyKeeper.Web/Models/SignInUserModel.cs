namespace MoneyKeeper.Web.Models
{
    public class SignInUserModel
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}