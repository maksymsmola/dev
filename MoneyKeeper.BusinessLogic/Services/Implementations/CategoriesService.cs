using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.Categories;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class CategoriesService : ICategoriesService
    {
        private readonly IRepository repository;

        public CategoriesService(IRepository repository)
        {
            this.repository = repository;
        }

        public List<GeneralCategoryDto> GetAll()
        {
            return
                this.repository.GetByCriteria<Category>(_ => true)
                    .Select(category => category.ToGeneralCategoryDto())
                    .ToList();
        }
    }
}