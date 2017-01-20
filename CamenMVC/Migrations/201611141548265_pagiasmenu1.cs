namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pagiasmenu1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Paginas", "Smenu_Id", "dbo.SottoMenus");
            DropIndex("dbo.Paginas", new[] { "Smenu_Id" });
            AddColumn("dbo.SottoMenus", "Pagina_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Paginas", "Titolo", c => c.String());
            CreateIndex("dbo.SottoMenus", "Pagina_Id");
            AddForeignKey("dbo.SottoMenus", "Pagina_Id", "dbo.Paginas", "Pagina_Id", cascadeDelete: true);
            DropColumn("dbo.Paginas", "Smenu_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Paginas", "Smenu_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.SottoMenus", "Pagina_Id", "dbo.Paginas");
            DropIndex("dbo.SottoMenus", new[] { "Pagina_Id" });
            DropColumn("dbo.Paginas", "Titolo");
            DropColumn("dbo.SottoMenus", "Pagina_Id");
            CreateIndex("dbo.Paginas", "Smenu_Id");
            AddForeignKey("dbo.Paginas", "Smenu_Id", "dbo.SottoMenus", "Smenu_Id", cascadeDelete: true);
        }
    }
}
