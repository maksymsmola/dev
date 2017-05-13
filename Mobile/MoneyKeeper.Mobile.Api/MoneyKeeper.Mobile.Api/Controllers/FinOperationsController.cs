using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Mobile.Api.Extensions;

namespace MoneyKeeper.Mobile.Api.Controllers
{
    public class FinOperationsController : ApiController
    {
        private readonly IFinOperationService finOperationService;

        public FinOperationsController(IFinOperationService finOperationService)
        {
            this.finOperationService = finOperationService;
        }

        public IHttpActionResult Get()
        {
            long userId = this.GetCurrentUserId();

            return this.Content(
                HttpStatusCode.OK,
                this.finOperationService.GetAllForUser(userId),
                new JsonMediaTypeFormatter(),
                "application/json");
        }
    }
}