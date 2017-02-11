using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Dto.Categories
{
    public class GeneralCategoryDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public FinOperationType Type { get; set; }
    }
}