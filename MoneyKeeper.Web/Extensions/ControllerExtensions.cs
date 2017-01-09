using System.Web.Mvc;
using MoneyKeeper.Web.ActionResults;
using MoneyKeeper.Web.Models;

namespace MoneyKeeper.Web.Extensions
{
    internal static class ControllerExtensions
    {
        public static CustomJsonResult CustomJson(this Controller controller, object data)
        {
            return new CustomJsonResult(data);
        }

        public static CustomJsonResult SuccessResult(this Controller controller)
        {
            return new CustomJsonResult(new SimpleResponseModel(true));
        }

        public static CustomJsonResult ErrorResult(this Controller controller)
        {
            return new CustomJsonResult(new SimpleResponseModel(false));
        }
    }
}