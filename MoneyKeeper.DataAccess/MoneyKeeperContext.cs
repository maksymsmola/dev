using System.Data.Entity;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess
{
    public class MoneyKeeperContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<FinancialOperation> FinancialOperations { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }
    }
}