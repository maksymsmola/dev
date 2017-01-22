using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Mappings
{
    internal static class FinOpMapping
    {
        public static FinOperationDto ToFinOperationDto(this FinancialOperation finOp)
        {
            return new FinOperationDto
            {
                Id = finOp.Id,
                Date = finOp.Date,
                Description = finOp.Description,
                Type = finOp.Type,
                Value = finOp.Value
            };
        }
    }
}