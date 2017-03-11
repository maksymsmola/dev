using System;
using System.Linq;
using System.Linq.Expressions;
using MoneyKeeper.Core;
using MoneyKeeper.Core.Entities;

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
                return finOp => finOp.Tags.Any(tag => this.tagsIds.Contains(tag.Id));
            }
        }
    }
}