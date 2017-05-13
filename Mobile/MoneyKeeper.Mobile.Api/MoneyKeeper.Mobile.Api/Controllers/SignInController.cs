using System.Net;
using System.Net.Http;
using System.Web.Http;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Mobile.Api.Models;

namespace MoneyKeeper.Mobile.Api.Controllers
{
    [AllowAnonymous]
    public class SignInController : ApiController
    {
        private readonly IUserService userService;

        public SignInController(IUserService userService)
        {
            this.userService = userService;
        }

        public HttpResponseMessage Post([FromBody] SignInModel model)
        {
            var user = this.userService.GetUserByCredentials(model.UserName, model.Password);

            if (user == null)
            {
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

            var resp = new HttpResponseMessage(HttpStatusCode.OK);

            resp.Content = new StringContent(
                TokenHelper.GenerateToken(user),
                System.Text.Encoding.UTF8,
                "text/plain");

            return resp;
        }
    }
}