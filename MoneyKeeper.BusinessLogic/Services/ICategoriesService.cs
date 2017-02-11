using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Categories;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface ICategoriesService
    {
        List<GeneralCategoryDto> GetAll();
    }
}