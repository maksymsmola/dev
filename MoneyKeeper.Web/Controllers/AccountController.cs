using System;
using System.Web.Mvc;
using System.Web.Security;

namespace MoneyKeeper.Web.Controllers
{
    public class AccountController : Controller
    {
        public JsonResult SignIn(string name , string password)
        {
            FormsAuthentication.SetAuthCookie(name, false);

            return this.Json(new { success = true });
        }

        [Authorize]
        public JsonResult SignOut()
        {
            FormsAuthentication.SignOut();
            this.Session.Abandon();

            return this.Json(new { sucess = true });
        }

        public JsonResult SignUp()
        {
            throw new NotImplementedException();
        }
    }
}