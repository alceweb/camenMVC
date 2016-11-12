namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuRuolo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Menus", "Ruolo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Menus", "Ruolo");
        }
    }
}
