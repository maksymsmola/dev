using System.Web.Http;
using System.Web.Http.Dispatcher;
using Castle.Windsor;

namespace MoneyKeeper.Mobile.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        // todo: this needed for dependecy injection in CustomAuthAttr. Need to remove it in future
        public static IWindsorContainer Container { get; private set; }

        public WebApiApplication()
        {
            Container = new WindsorContainer().Install(new DependencyConventions());
        }

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator),
                new WindsorCompositionRoot(Container));
        }

        public override void Dispose()
        {
            Container.Dispose();
            base.Dispose();
        }
    }
}