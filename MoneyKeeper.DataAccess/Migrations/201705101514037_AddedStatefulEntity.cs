using System.Data.Entity.Migrations;
using MoneyKeeper.Core.Entities;

namespace MoneyKeeper.DataAccess.Migrations
{
    public partial class AddedStatefulEntity : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.FinancialOperations", "State", c => c.Int(nullable: true));
            this.AddColumn("dbo.Tags", "State", c => c.Int(nullable: true));

            this.Sql($"UPDATE dbo.FinancialOperations SET State={EntityState.Untouched:D};");
            this.Sql($"UPDATE dbo.Tags SET State={EntityState.Untouched:D};");

            this.AlterColumn("dbo.FinancialOperations", "State", c => c.Int(nullable: false));
            this.AlterColumn("dbo.Tags", "State", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            this.DropColumn("dbo.Tags", "State");
            this.DropColumn("dbo.FinancialOperations", "State");
        }
    }
}