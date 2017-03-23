namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pubblicaPagina : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Paginas", "Pubblica", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Paginas", "Pubblica");
        }
    }
}
