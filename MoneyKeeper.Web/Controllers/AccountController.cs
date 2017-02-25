using System.Web.Mvc;
using System.Web.Security;
using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.BusinessLogic.Dto.User;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.Web.ActionResults;
using MoneyKeeper.Web.Extensions;
using MoneyKeeper.Web.Mappings;
using MoneyKeeper.Web.Models;

namespace MoneyKeeper.Web.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        public CustomJsonResult SignIn(SignInUserModel model)
        {
            SimpleUserDto user = this.userService.GetUserByCredentials(model.Name, model.Password);

            if (user == null)
            {
                return this.ErrorResult(); //todo: handle this on client
            }

            this.Session.SetCurrentUserId(user.Id);
            FormsAuthentication.SetAuthCookie(model.Name, model.RememberMe);

            return this.SuccessResult();
        }

        [Authorize]
        public CustomJsonResult SignOut()
        {
            FormsAuthentication.SignOut();
            this.Session.Abandon();

            return this.SuccessResult();
        }

        public CustomJsonResult SignUp(SignUpUserModel model)
        {
            long userId = this.userService.CreateUser(model.ToCreateUserDto());

            this.Session.SetCurrentUserId(userId);
            FormsAuthentication.SetAuthCookie(model.Name, true);

            return this.SuccessResult();
        }
    }
}