using System.Collections.Generic;
using System.Linq;
using MoneyKeeper.BusinessLogic.Dto;
using MoneyKeeper.BusinessLogic.Mappings;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess.Repository;

namespace MoneyKeeper.BusinessLogic.Services.Implementations
{
    public class FinOperationService : IFinOperationService
    {
        private readonly IRepository repository;

        public FinOperationService(IRepository repository)
        {
            this.repository = repository;
        }

        public List<FinOperationDto> GetAllForUser(long userId)
        {
            return
                this.repository.GetByCriteria<FinancialOperation>(finOp => finOp.UserId == userId)
                    .Select(finOp => finOp.ToFinOperationDto())
                    .ToList();
        }
    }
}