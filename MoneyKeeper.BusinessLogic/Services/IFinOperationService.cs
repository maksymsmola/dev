using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.BusinessLogic.Dto.FinancialOperation;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IFinOperationService
    {
        List<FinOperationDto> GetAllForUser(long userId);

        void Add(FinOperationDto model);
    }
}