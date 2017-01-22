using System.Collections.Generic;
using MoneyKeeper.BusinessLogic.Dto;

namespace MoneyKeeper.BusinessLogic.Services
{
    public interface IFinOperationService
    {
        List<FinOperationDto> GetAllForUser(long userId);
    }
}