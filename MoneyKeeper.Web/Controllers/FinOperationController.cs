using System.Web.Mvc;
using MoneyKeeper.BusinessLogic.Dto.Filters;
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

        [HttpGet]
        public CustomJsonResult GetByFilter(FinOperationFilterDto filter)
        {
            filter.UserId = this.Session.GetCurrentUserId();

            return this.CustomJson(this.finOperationService.GetByFilter(filter));
        }

        [HttpPost]
        public CustomJsonResult Add(AddEditFinOperationDto model)
        {
            model.UserId = this.Session.GetCurrentUserId();
            this.finOperationService.Add(model);
            return this.SuccessResult();
        }

        [HttpGet]
        public CustomJsonResult GetForCrud(long id)
        {
            return this.CustomJson(this.finOperationService.GetForCrud(id));
        }

        [HttpPost]
        public CustomJsonResult Edit(AddEditFinOperationDto model)
        {
            model.UserId = this.Session.GetCurrentUserId();
            this.finOperationService.Edit(model);
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