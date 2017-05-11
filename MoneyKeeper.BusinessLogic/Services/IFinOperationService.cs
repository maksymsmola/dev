using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.Filters;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IFinOperationService
    {
        List<FinOperationListItemDto> GetAllForUser(long userId);

        List<FinOperationListItemDto> GetByFilter(FinOperationFilterDto filter);

        void Add(AddEditFinOperationDto model);

        AddEditFinOperationDto GetForCrud(long id);

        void Edit(AddEditFinOperationDto model);

        void Delete(long id);

        long LastAddedFinOperationForUser(long userId);
    }
}