using System.Web.Mvc;

namespace MoneyKeeper.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}