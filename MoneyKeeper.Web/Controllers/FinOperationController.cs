using System.Web.Mvc;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Web.ActionResults;
using MoneyKeeper.Web.Extensions;

namespace MoneyKeeper.Web.Controllers
{
    public class FinOperationController : Controller
    {
        private readonly IFinOperationService finOperationService;

        public FinOperationController(IFinOperationService finOperationService)
        {
            this.finOperationService = finOperationService;
        }

        [HttpGet]
        public CustomJsonResult GetForCurrentUser()
        {
            long userId = this.Session.GetCurrentUserId();
            return this.CustomJson(this.finOperationService.GetAllForUser(userId));
        }

        [HttpPost]
        public CustomJsonResult Add(AddEditFinOperationDto model)
        {
            model.UserId = this.Session.GetCurrentUserId();
            this.finOperationService.Add(model);
            return this.SuccessResult();
        }

        [HttpPost]
        public CustomJsonResult Delete(long id)
        {
            this.finOperationService.Delete(id);

            return this.SuccessResult();
        }
    }
}