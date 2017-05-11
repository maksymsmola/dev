using System.Web.Http;

namespace MoneyKeeper.Mobile.Api.Extensions
{
    public static class ApiControllerExtension
    {
        public static long GetCurrentUserId(this ApiController controller)
        {
            return
                TokenHelper
                    .GetUserFromToken(controller.Request.Headers.Authorization.Parameter)
                    .Id;
        }
    }
}