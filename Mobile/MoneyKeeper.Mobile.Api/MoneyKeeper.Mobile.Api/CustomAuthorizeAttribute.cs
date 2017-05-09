using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using MoneyKeeper.BusinessLogic.Dto.User;

namespace MoneyKeeper.Mobile.Api
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            this.Authorize(actionContext);
        }

        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            this.Authorize(actionContext);
            return Task.FromResult(0);
        }

        private void Authorize(HttpActionContext actionContext)
        {
            string token = actionContext.Request.Headers.Authorization?.Parameter;

            if (string.IsNullOrWhiteSpace(token))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }

            // todo: implement token validation
            SimpleUserDto user = TokenHelper.GetUserFromToken(token);
            if (user.Id == 0 || string.IsNullOrWhiteSpace(user.LoginName))
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }
    }
}