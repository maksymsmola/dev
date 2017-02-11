using MoneyKeeper.BusinessLogic.Dto.Categories;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Mappings
{
    internal static class CategoryMapping
    {
        public static GeneralCategoryDto ToGeneralCategoryDto(this Category category)
        {
            return new GeneralCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type
            };
        }
    }
}