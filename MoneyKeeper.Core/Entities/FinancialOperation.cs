using System;
using System.Collections.Generic;

namespace MoneyKeeper.Core.Entities
{
    public class FinancialOperation : BaseEntity
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public FinOperationType Type { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public FinancialOperation()
        {
            this.Tags = new HashSet<Tag>();
        }
    }
}