using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Mappings
{
    internal static class FinOpMapping
    {
        public static FinOperationListItemDto ToFinOperationListItemDto(this FinancialOperation finOp)
        {
            return new FinOperationListItemDto
            {
                Id = finOp.Id,
                Date = finOp.Date,
                Description = finOp.Description,
                Type = finOp.Type,
                Value = finOp.Value,
                CategoryName = finOp.Category?.Name,
                Tags = finOp.Tags.Select(tag => tag.ToSimpleTagDto()).ToList()
            };
        }

        public static FinancialOperation ToFinancialOperation(this AddEditFinOperationDto dto)
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