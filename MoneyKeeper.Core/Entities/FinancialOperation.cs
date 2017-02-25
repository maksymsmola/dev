using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MoneyKeeper.Core.Entities
{
    public class FinancialOperation : BaseEntity
    {
        public DateTime Date { get; set; }

        public double Value { get; set; }

        public string Description { get; set; }

        public FinOperationType Type { get; set; }

        public long UserId { get; set; }

        public long? CategoryId { get; set; }

        public virtual User User { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }

        public FinancialOperation()
        {
            this.Tags = new HashSet<Tag>();
        }

        public List<FinancialOperation> Clone(int amount)
        {
            if (amount < 1)
            {
                throw new InvalidEnumArgumentException("amount should be non-zero positive integer");
            }

            var result = new List<FinancialOperation>(amount);

            for (int i = 0; i < amount; i++)
            {
                result.Add((FinancialOperation)this.MemberwiseClone());
            }

            return result;
        }
    }
}