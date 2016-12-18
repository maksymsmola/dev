using System;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MoneyKeeper.DataAccess;

namespace MoneyKeeper.Web.IocContainer
{
    internal static class IocInitializer
    {
        internal static IWindsorContainer Container;

        internal static IWindsorContainer Initialize()
        {
            if (Container == null)
            {
                Container = new WindsorContainer();

                Container.Register(Classes.FromAssemblyNamed("Elmah.Mvc").BasedOn<IController>().LifestyleTransient());
                Container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());

                Container.Register(Component.For<DbContext>().ImplementedBy<MoneyKeeperContext>().LifeStyle.PerWebRequest);
            }

            return Container;
        }
    }
}