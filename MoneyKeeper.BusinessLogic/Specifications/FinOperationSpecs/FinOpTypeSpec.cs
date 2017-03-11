using System;
using System.Linq.Expressions;
using MoneyKeeper.BusinessLogic.Dto.Filters;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpTypeSpec : Specification<FinancialOperation>
    {
        private readonly FinOpType type;

        public FinOpTypeSpec(FinOpType type)
        {
            this.type = type;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                switch (this.type)
                {
                    case FinOpType.Income:
                        return finOp => finOp.Type == FinOperationType.Income;
                    case FinOpType.Expense:
                        return finOp => finOp.Type == FinOperationType.Expense;
                    case FinOpType.Both:
                    default:
                        return _ => true;
                }
            }
        }
    }
}