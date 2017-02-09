namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class checkprivacy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Privacy", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Privacy");
        }
    }
}
