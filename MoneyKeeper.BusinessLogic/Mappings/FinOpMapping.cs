using System.Linq;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;
using MoneyKeeper.BusinessLogic.Dto.Statistic;
using MoneyKeeper.Core;
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

        public static AddEditFinOperationDto ToAddEditFinOperationDto(this FinancialOperation finOp)
        {
            return new AddEditFinOperationDto
            {
                Id = finOp.Id,
                Date = finOp.Date,
                Description = finOp.Description,
                CategoryId = finOp.CategoryId,
                Type = finOp.Type,
                Value = finOp.Value,
                Tags = finOp.Tags.Select(tag => tag.ToSimpleTagDto()).ToList()
            };
        }

        public static GeneralFinOperationModel ToGeneralFinOperationModel(this FinancialOperation finOp)
        {
            return new GeneralFinOperationModel
            {
                Type = finOp.Type,
                Value = finOp.Value,
                Date = finOp.Date,
                Description = finOp.Description,
                CategoryName = finOp.Category?.Name ?? Constants.NO_CATEGORY
            };
        }
    }
}