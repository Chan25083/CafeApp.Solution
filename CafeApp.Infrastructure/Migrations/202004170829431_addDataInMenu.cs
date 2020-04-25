namespace CafeApp.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDataInMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Data", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Data");
        }
    }
}
