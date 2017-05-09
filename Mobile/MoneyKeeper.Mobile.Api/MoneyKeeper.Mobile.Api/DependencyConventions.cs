using System.Data.Entity;
using System.Web.Http;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MoneyKeeper.BusinessLogic.Services;
using MoneyKeeper.BusinessLogic.Services.Implementations;
using MoneyKeeper.DataAccess;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.Mobile.Api
{
    public class DependencyConventions : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly().BasedOn<ApiController>().LifestyleTransient());

            container.Register(Component.For<DbContext>().ImplementedBy<MoneyKeeperContext>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IRepository>().ImplementedBy<Repository>().LifeStyle.PerWebRequest);

            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IFinOperationService>().ImplementedBy<FinOperationService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICategoriesService>().ImplementedBy<CategoriesService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ITagsService>().ImplementedBy<TagsService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IStatisticService>().ImplementedBy<StatisticService>().LifeStyle.PerWebRequest);
        }
    }
}