using System.Data.Entity.ModelConfiguration;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Mappings
{
    public class UserDbMap : EntityTypeConfiguration<User>
    {
        public UserDbMap()
        {
            this.Property(user => user.FirstName).HasMaxLength(100);
            this.Property(user => user.LastName).HasMaxLength(100);
        }
    }
}