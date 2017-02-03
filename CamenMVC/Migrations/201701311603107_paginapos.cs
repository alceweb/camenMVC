namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class paginapos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paginas", "Posizione", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paginas", "Posizione");
        }
    }
}
