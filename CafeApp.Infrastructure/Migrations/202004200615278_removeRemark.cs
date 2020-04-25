namespace CafeApp.Infrastructure.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRemark : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Menus", "Remark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Menus", "Remark", c => c.String());
        }
    }
}
