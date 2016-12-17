using System.Collections.Generic;

namespace MoneyKeeper.Core.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<FinancialOperation> FinancialOperations { get; set; }

        public Tag()
        {
            this.FinancialOperations = new HashSet<FinancialOperation>();
        }
    }
}