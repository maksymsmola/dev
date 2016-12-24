using System.Data.Entity.ModelConfiguration;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Mappings
{
    public class TagDbMap : EntityTypeConfiguration<Tag>
    {
        public TagDbMap()
        {
            this.Property(tag => tag.Name).HasMaxLength(100);
            this.HasMany(tag => tag.FinancialOperations)
                .WithMany(finOp => finOp.Tags);
        }
    }
}