using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
using MoneyKeeper.BusinessLogic.Dto.Synchronization.FinOperation;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Mobile.Api.Extensions;

namespace MoneyKeeper.Mobile.Api.Controllers
{
    public class FinOperationController : ApiController
    {
        private readonly IFinOperationService finOperationService;

        public FinOperationController(IFinOperationService finOperationService)
        {
            this.finOperationService = finOperationService;
        }

        public IHttpActionResult Post([FromBody] FinOperationSyncDto model)
        {
            long userId = this.GetCurrentUserId();

            this.finOperationService.Add(new AddEditFinOperationDto
            {
                Amount = 1,
                Date = model.Date,
                Value = model.Value,
                Type = model.Type,
                Description = model.Description,
                UserId = this.GetCurrentUserId()
            });

            model.Id = this.finOperationService.LastAddedFinOperationForUser(userId);

            return this.Content(HttpStatusCode.Created, model, new JsonMediaTypeFormatter(), "application/json");
        }
    }
}