using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using MoneyKeeper.Web.IocContainer;

namespace MoneyKeeper.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            IWindsorContainer ioc = IocInitializer.Initialize();
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(ioc));
        }
    }
}