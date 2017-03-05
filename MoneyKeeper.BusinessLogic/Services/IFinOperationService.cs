using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IFinOperationService
    {
        List<FinOperationListItemDto> GetAllForUser(long userId);

        void Add(AddEditFinOperationDto model);

        AddEditFinOperationDto GetForCrud(long id);

        void Edit(AddEditFinOperationDto model);

        void Delete(long id);
    }
}