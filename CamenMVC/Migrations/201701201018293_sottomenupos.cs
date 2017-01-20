namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sottomenupos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SottoMenus", "Posizione", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SottoMenus", "Posizione");
        }
    }
}
