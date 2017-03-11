using System;
using System.Linq.Expressions;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpUserSpec : Specification<FinancialOperation>
    {
        private readonly long userId;

        public FinOpUserSpec(long userId)
        {
            this.userId = userId;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                return finOp => finOp.UserId == this.userId;
            }
        }
    }
}