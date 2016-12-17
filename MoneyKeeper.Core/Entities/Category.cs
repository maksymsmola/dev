using System.Collections.Generic;

namespace MoneyKeeper.Core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<FinancialOperation> FinancialOperations { get; set; }

        public Category()
        {
            this.FinancialOperations = new HashSet<FinancialOperation>();
        }
    }
}