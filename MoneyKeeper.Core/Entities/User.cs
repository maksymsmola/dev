using System.Collections.Generic;

namespace MoneyKeeper.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string HashedPasword { get; set; }

        public ICollection<FinancialOperation> FinancialOperations { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public User()
        {
            this.FinancialOperations = new HashSet<FinancialOperation>();
            this.Tags = new HashSet<Tag>();
        }
    }
}