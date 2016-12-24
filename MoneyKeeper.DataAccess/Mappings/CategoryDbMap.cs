using System.Data.Entity.ModelConfiguration;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Mappings
{
    public class CategoryDbMap : EntityTypeConfiguration<Category>
    {
        public CategoryDbMap()
        {
            this.Property(category => category.Name).HasMaxLength(100);
            this.HasMany(category => category.FinancialOperations)
                .WithOptional(finOp => finOp.Category)
                .WillCascadeOnDelete(false);
        }
    }
}