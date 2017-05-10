using System.Collections.Generic;

namespace MoneyKeeper.Core.Entities
{
    public class Tag : StatefulEntity
    {
        public string Name { get; set; }

        public long UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<FinancialOperation> FinancialOperations { get; set; }

        public Tag()
        {
            this.FinancialOperations = new HashSet<FinancialOperation>();
        }
    }
}