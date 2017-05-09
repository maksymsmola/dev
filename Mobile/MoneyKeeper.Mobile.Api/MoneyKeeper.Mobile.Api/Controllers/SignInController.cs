using System.Net;
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

        public IHttpActionResult Post([FromBody] SignInModel model)
        {
            var user = this.userService.GetUserByCredentials(model.UserName, model.Password);

            if (user == null)
            {
                return this.BadRequest("User not found");
            }

            return this.Content(HttpStatusCode.OK, TokenHelper.GenerateToken(user));
        }
    }
}