using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.Web.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            return this.View();
        }
    }
}