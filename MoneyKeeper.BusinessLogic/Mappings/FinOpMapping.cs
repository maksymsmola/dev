using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
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
                Value = finOp.Value,
                CategoryName = finOp.Category?.Name
            };
        }

        public static FinancialOperation ToFinancialOperation(this FinOperationDto dto)
        {
            return new FinancialOperation
            {
                UserId = dto.UserId,
                CategoryId = dto.CategoryId,
                Type = dto.Type,
                Description = dto.Description,
                Date = dto.Date,
                Value = dto.Value
            };
        }
    }
}