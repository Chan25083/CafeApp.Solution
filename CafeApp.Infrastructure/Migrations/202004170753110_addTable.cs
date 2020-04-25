namespace CafeApp.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        TableId = c.Int(nullable: false, identity: true),
                        TableName = c.String(),
                        TablePlace = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TableId);
            
            AddColumn("dbo.Carts", "TableId", c => c.Int(nullable: false));
            CreateIndex("dbo.Carts", "TableId");
            AddForeignKey("dbo.Carts", "TableId", "dbo.Tables", "TableId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Carts", "TableId", "dbo.Tables");
            DropIndex("dbo.Carts", new[] { "TableId" });
            DropColumn("dbo.Carts", "TableId");
            DropTable("dbo.Tables");
        }
    }
}
