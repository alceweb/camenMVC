namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StrutturaMenu : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Menu_Id = c.Int(nullable: false, identity: true),
                        Posizione = c.Int(nullable: false),
                        TestoMenu = c.String(),
                        Pubblica = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Menu_Id);
            
            CreateTable(
                "dbo.SottoMenus",
                c => new
                    {
                        Smenu_Id = c.Int(nullable: false, identity: true),
                        Menu_Id = c.Int(nullable: false),
                        TestoSmenu = c.String(),
                        Pubblica = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Smenu_Id)
                .ForeignKey("dbo.Menus", t => t.Menu_Id, cascadeDelete: true)
                .Index(t => t.Menu_Id);
            
            CreateTable(
                "dbo.Paginas",
                c => new
                    {
                        Pagina_Id = c.Int(nullable: false, identity: true),
                        Smenu_Id = c.Int(nullable: false),
                        Contenuo = c.String(),
                    })
                .PrimaryKey(t => t.Pagina_Id)
                .ForeignKey("dbo.SottoMenus", t => t.Smenu_Id, cascadeDelete: true)
                .Index(t => t.Smenu_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SottoMenus", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.Paginas", "Smenu_Id", "dbo.SottoMenus");
            DropIndex("dbo.Paginas", new[] { "Smenu_Id" });
            DropIndex("dbo.SottoMenus", new[] { "Menu_Id" });
            DropTable("dbo.Paginas");
            DropTable("dbo.SottoMenus");
            DropTable("dbo.Menus");
        }
    }
}
