namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documenti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Categoria_Id = c.Int(nullable: false, identity: true),
                        Categoria = c.String(),
                    })
                .PrimaryKey(t => t.Categoria_Id);
            
            CreateTable(
                "dbo.Documentis",
                c => new
                    {
                        Documento_Id = c.Int(nullable: false, identity: true),
                        Categoria_Id = c.Int(nullable: false),
                        Evento_Id = c.Int(nullable: false),
                        Linea_Id = c.Int(nullable: false),
                        Sessione_Id = c.Int(nullable: false),
                        Oratore = c.String(),
                        Titolo = c.String(),
                        Data = c.DateTime(nullable: false),
                        Riferimento = c.String(),
                        Lingua = c.String(),
                        NomeFile = c.String(),
                    })
                .PrimaryKey(t => t.Documento_Id)
                .ForeignKey("dbo.Categories", t => t.Categoria_Id, cascadeDelete: true)
                .ForeignKey("dbo.Eventis", t => t.Evento_Id, cascadeDelete: true)
                .ForeignKey("dbo.Linees", t => t.Linea_Id, cascadeDelete: true)
                .ForeignKey("dbo.Sessionis", t => t.Sessione_Id, cascadeDelete: true)
                .Index(t => t.Categoria_Id)
                .Index(t => t.Evento_Id)
                .Index(t => t.Linea_Id)
                .Index(t => t.Sessione_Id);
            
            CreateTable(
                "dbo.Eventis",
                c => new
                    {
                        Evento_Id = c.Int(nullable: false, identity: true),
                        Evento = c.String(),
                    })
                .PrimaryKey(t => t.Evento_Id);
            
            CreateTable(
                "dbo.Linees",
                c => new
                    {
                        Linea_Id = c.Int(nullable: false, identity: true),
                        Linea = c.String(),
                    })
                .PrimaryKey(t => t.Linea_Id);
            
            CreateTable(
                "dbo.Sessionis",
                c => new
                    {
                        Sessione_Id = c.Int(nullable: false, identity: true),
                        Sessione = c.String(),
                    })
                .PrimaryKey(t => t.Sessione_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Documentis", "Sessione_Id", "dbo.Sessionis");
            DropForeignKey("dbo.Documentis", "Linea_Id", "dbo.Linees");
            DropForeignKey("dbo.Documentis", "Evento_Id", "dbo.Eventis");
            DropForeignKey("dbo.Documentis", "Categoria_Id", "dbo.Categories");
            DropIndex("dbo.Documentis", new[] { "Sessione_Id" });
            DropIndex("dbo.Documentis", new[] { "Linea_Id" });
            DropIndex("dbo.Documentis", new[] { "Evento_Id" });
            DropIndex("dbo.Documentis", new[] { "Categoria_Id" });
            DropTable("dbo.Sessionis");
            DropTable("dbo.Linees");
            DropTable("dbo.Eventis");
            DropTable("dbo.Documentis");
            DropTable("dbo.Categories");
        }
    }
}
