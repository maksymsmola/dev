using System;
using System.Linq.Expressions;
using MoneyKeeper.BusinessLogic.Dto.Filters;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpDateSpec : Specification<FinancialOperation>
    {
        private readonly RangeFilter<DateTime?> filter;

        public FinOpDateSpec(RangeFilter<DateTime?> filter)
        {
            this.filter = filter;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                if (!this.filter.From.HasValue && !this.filter.To.HasValue)
                    return _ => true;

                DateTime from = this.filter.From ?? DateTime.MinValue;
                DateTime to = this.filter.To ?? DateTime.MaxValue;

                return this.filter.ExactValue
                    ? (Expression<Func<FinancialOperation, bool>>) (finOp => finOp.Date == from)
                    : (finOp => finOp.Date >= from && finOp.Date <= to);
            }
        }
    }
}