using System;
using System.Linq.Expressions;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpDescriptionSpec : Specification<FinancialOperation>
    {
        private readonly string description;

        public FinOpDescriptionSpec(string description)
        {
            this.description = description;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                return string.IsNullOrEmpty(this.description)
                    ? (Expression<Func<FinancialOperation, bool>>)(_ => true)
                    : (finOp => finOp.Description.ToLower().Contains(this.description.ToLower()));
            }
        }
    }
}