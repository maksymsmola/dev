using System.Data.Entity.Migrations;

namespace MoneyKeeper.DataAccess.Migrations
{
    public partial class AddedLoginNameForUser : DbMigration
    {
        public override void Up()
        {
            this.AddColumn("dbo.Users", "LoginName", c => c.String(maxLength: 100));
        }

        public override void Down()
        {
            this.DropColumn("dbo.Users", "LoginName");
        }
    }
}