using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            return this.View();
        }

        public JsonResult GetJson()
        {
            return this.Json(new { Name = "Jhonny", LastName = "Depp" }, JsonRequestBehavior.AllowGet);
        }
    }
}