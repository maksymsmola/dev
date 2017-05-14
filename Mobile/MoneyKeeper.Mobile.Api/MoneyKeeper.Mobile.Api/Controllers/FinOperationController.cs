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

        public IHttpActionResult Post([FromBody] AddEditFinOperationDto model)
        {
            model.UserId = this.GetCurrentUserId();

            FinOperationSyncDto result =  this.finOperationService.AddPersistant(model);

            return this.Content(HttpStatusCode.Created, result, new JsonMediaTypeFormatter(), "application/json");
        }

        public IHttpActionResult Put([FromBody] AddEditFinOperationDto model)
        {
            model.UserId = this.GetCurrentUserId();

            FinOperationSyncDto result = this.finOperationService.UpdatePersistant(model);

            return this.Content(HttpStatusCode.OK, result, new JsonMediaTypeFormatter(), "application/json");
        }

        public void Delete([FromUri] long id)
        {
            this.finOperationService.DeletePersistant(id);
        }
    }
}