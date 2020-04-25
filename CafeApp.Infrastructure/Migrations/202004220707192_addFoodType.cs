namespace CafeApp.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFoodType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "FoodType", c => c.Int(nullable: false));
            DropColumn("dbo.Menus", "Category");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "Category", c => c.Int(nullable: false));
            DropColumn("dbo.Menus", "FoodType");
        }
    }
}
