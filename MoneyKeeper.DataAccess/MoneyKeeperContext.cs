using System.Data.Entity;
using MoneyKeeper.Core.Entities;
using MoneyKeeper.DataAccess.Mappings;

namespace MoneyKeeper.DataAccess
{
    public class MoneyKeeperContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<FinancialOperation> FinancialOperations { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserDbMap());
            modelBuilder.Configurations.Add(new FinancialOperationDbMap());
            modelBuilder.Configurations.Add(new TagDbMap());
            modelBuilder.Configurations.Add(new CategoryDbMap());
        }
    }
}