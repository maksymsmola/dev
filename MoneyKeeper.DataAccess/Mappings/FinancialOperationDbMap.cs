using System.Data.Entity.ModelConfiguration;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Mappings
{
    public class FinancialOperationDbMap : EntityTypeConfiguration<FinancialOperation>
    {
        public FinancialOperationDbMap()
        {
            this.Property(finOp => finOp.Description).HasMaxLength(200);
            this.HasMany(finOp => finOp.Tags)
                .WithMany(tag => tag.FinancialOperations);
        }
    }
}