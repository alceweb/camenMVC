namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class menuruoli : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MenuRuolis",
                c => new
                    {
                        MenuRuoli_Id = c.Int(nullable: false, identity: true),
                        Ruolo = c.String(),
                        Menu_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuRuoli_Id)
                .ForeignKey("dbo.Menus", t => t.Menu_Id, cascadeDelete: true)
                .Index(t => t.Menu_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MenuRuolis", "Menu_Id", "dbo.Menus");
            DropIndex("dbo.MenuRuolis", new[] { "Menu_Id" });
            DropTable("dbo.MenuRuolis");
        }
    }
}
