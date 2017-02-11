using System.Data.Entity;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.BusinessLogic.Services.Implementations;
using MoneyKeeper.DataAccess;
using MoneyKeeper.DataAccess.Repository;

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

                RegisterControllers();
                RegisterDal();
                RegisterBl();
            }

            return Container;
        }

        private static void RegisterControllers()
        {
            Container.Register(Classes.FromAssemblyNamed("Elmah.Mvc").BasedOn<IController>().LifestyleTransient());
            Container.Register(Classes.FromThisAssembly().BasedOn<IController>().LifestyleTransient());
        }

        private static void RegisterDal()
        {
            Container.Register(Component.For<DbContext>().ImplementedBy<MoneyKeeperContext>().LifeStyle.PerWebRequest);
            Container.Register(Component.For<IRepository>().ImplementedBy<Repository>().LifeStyle.PerWebRequest);
        }

        private static void RegisterBl()
        {
            Container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.PerWebRequest);
            Container.Register(Component.For<IFinOperationService>().ImplementedBy<FinOperationService>().LifeStyle.PerWebRequest);
            Container.Register(Component.For<ICategoriesService>().ImplementedBy<CategoriesService>().LifeStyle.PerWebRequest);
        }
    }
}