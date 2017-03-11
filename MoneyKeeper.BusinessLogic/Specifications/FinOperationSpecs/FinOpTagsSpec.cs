using System;
using System.Linq;
using System.Linq.Expressions;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.Core.Extensions;

namespace MoneyKeeper.BusinessLogic.Specifications.FinOperationSpecs
{
    public class FinOpTagsSpec : Specification<FinancialOperation>
    {
        private readonly long[] tagsIds;

        public FinOpTagsSpec(long[] tagsIds)
        {
            this.tagsIds = tagsIds;
        }

        public override Expression<Func<FinancialOperation, bool>> Predicate
        {
            get
            {
                return this.tagsIds.IsNullOrEmpty()
                    ? (Expression<Func<FinancialOperation, bool>>)(_ => true)
                    : (finOp => finOp.Tags.Any(tag => this.tagsIds.Contains(tag.Id)));
            }
        }
    }
}