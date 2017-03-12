using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MoneyKeeper.BusinessLogic.Dto.Filters;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpValueSpec : Specification<FinancialOperation>
    {
        private readonly RangeFilter<double?> filter;

        public FinOpValueSpec(RangeFilter<double?> filter)
        {
            this.filter = filter;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                if (this.filter.From == null && this.filter.To == null)
                {
                    return _ => true;
                }

                double from = this.filter.From ?? 0.0;
                double to = this.filter.To ?? double.MaxValue;

                return this.filter.ExactValue
                    ? (Expression<Func<FinancialOperation, bool>>)(finOp => finOp.Value == from)
                    : (finOp => finOp.Value >= from && finOp.Value <=to);
            }
        }
    }
}
