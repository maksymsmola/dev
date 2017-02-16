using System.Web.Mvc;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Web.ActionResults;
using MoneyKeeper.Web.Extensions;

namespace MoneyKeeper.Web.Controllers
{
    public class TagsController : Controller
    {
        private readonly ITagsService tagsService;

        public TagsController(ITagsService tagsService)
        {
            this.tagsService = tagsService;
        }

        public CustomJsonResult GetAllForUser()
        {
            long userId = this.Session.GetCurrentUserId();

            return this.CustomJson(this.tagsService.GetAllForUser(userId));
        }
    }
}