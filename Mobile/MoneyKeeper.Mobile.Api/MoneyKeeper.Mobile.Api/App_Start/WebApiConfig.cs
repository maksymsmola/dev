using System.Web.Http;

namespace MoneyKeeper.Mobile.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Filters.Add(new CustomAuthorizeAttribute());

            // Web API configuration and services
            config.Formatters.Add(new BrowserJsonFormatter());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}