using System.Net;
using System.Net.Http.Formatting;
using System.Web.Http;
using MoneyKeeper.BusinessLogic.Dto.Synchronization;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Mobile.Api.Extensions;

namespace MoneyKeeper.Mobile.Api.Controllers
{
    public class SyncController : ApiController
    {
        private readonly ISynchronizationService synchronizationService;

        public SyncController(ISynchronizationService synchronizationService)
        {
            this.synchronizationService = synchronizationService;
        }

        public IHttpActionResult Post([FromBody] SyncRequest syncRequest)
        {
            long currentUserId = this.GetCurrentUserId();

            syncRequest.UserId = currentUserId;

            SyncResponse syncResponse = this.synchronizationService.SynchronizeData(syncRequest);

            return this.Content(HttpStatusCode.OK, syncResponse, new JsonMediaTypeFormatter(), "application/json");
        }
    }
}