namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bloccoutente : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Bloccato", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Bloccato");
        }
    }
}
