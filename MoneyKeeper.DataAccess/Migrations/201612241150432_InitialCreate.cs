using System.Data.Entity.Migrations;

namespace MoneyKeeper.DataAccess.Migrations
{
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            this.CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.FinancialOperations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Value = c.Double(nullable: false),
                        Description = c.String(maxLength: 200),
                        Type = c.Byte(nullable: false),
                        UserId = c.Long(nullable: false),
                        CategoryId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.UserId)
                .Index(t => t.CategoryId);

            this.CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        UserId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

            this.CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 100),
                        LastName = c.String(maxLength: 100),
                        HashedPasword = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            this.CreateTable(
                "dbo.FinancialOperationTags",
                c => new
                    {
                        FinancialOperation_Id = c.Long(nullable: false),
                        Tag_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.FinancialOperation_Id, t.Tag_Id })
                .ForeignKey("dbo.FinancialOperations", t => t.FinancialOperation_Id)
                .ForeignKey("dbo.Tags", t => t.Tag_Id)
                .Index(t => t.FinancialOperation_Id)
                .Index(t => t.Tag_Id);
        }

        public override void Down()
        {
            this.DropForeignKey("dbo.FinancialOperations", "CategoryId", "dbo.Categories");
            this.DropForeignKey("dbo.FinancialOperationTags", "Tag_Id", "dbo.Tags");
            this.DropForeignKey("dbo.FinancialOperationTags", "FinancialOperation_Id", "dbo.FinancialOperations");
            this.DropForeignKey("dbo.Tags", "UserId", "dbo.Users");
            this.DropForeignKey("dbo.FinancialOperations", "UserId", "dbo.Users");
            this.DropIndex("dbo.FinancialOperationTags", new[] { "Tag_Id" });
            this.DropIndex("dbo.FinancialOperationTags", new[] { "FinancialOperation_Id" });
            this.DropIndex("dbo.Tags", new[] { "UserId" });
            this.DropIndex("dbo.FinancialOperations", new[] { "CategoryId" });
            this.DropIndex("dbo.FinancialOperations", new[] { "UserId" });
            this.DropTable("dbo.FinancialOperationTags");
            this.DropTable("dbo.Users");
            this.DropTable("dbo.Tags");
            this.DropTable("dbo.FinancialOperations");
            this.DropTable("dbo.Categories");
        }
    }
}