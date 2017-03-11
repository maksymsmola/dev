using System;

namespace MoneyKeeper.BusinessLogic.Dto.Filters
{
    public class FinOperationFilterDto
    {
        public long UserId { get; set; }

        public FinOpType Type { get; set; }

        public RangeFilter<DateTime?> Date { get; set; }

        public RangeFilter<double?> Value { get; set; }

        public string Description { get; set; }

        public long?[] CategoriesIds { get; set; }

        public long[] TagsIds { get; set; }
    }
}