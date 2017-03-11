using System;
using System.Linq;
using System.Linq.Expressions;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.Core.Extensions;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpCategorySpec : Specification<FinancialOperation>
    {
        private readonly long?[] categoryIds;

        public FinOpCategorySpec(long?[] categoryIds)
        {
            this.categoryIds = categoryIds;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                return this.categoryIds.IsNullOrEmpty()
                    ? (Expression<Func<FinancialOperation, bool>>)(_ => true)
                    : (finOp => this.categoryIds.Contains(finOp.CategoryId));
            }
        }
    }
}