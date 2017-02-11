using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.Web.ActionResults;
using MoneyKeeper.Web.Extensions;

namespace MoneyKeeper.Web.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        [HttpGet]
        public CustomJsonResult GetAll()
        {
            return this.CustomJson(this.categoriesService.GetAll());
        }
    }
}