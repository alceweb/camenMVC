namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class documenti : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocRuolis",
                c => new
                    {
                        DocRuoli_Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(),
                        Documenti_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DocRuoli_Id);
            
            CreateTable(
                "dbo.Documentis",
                c => new
                    {
                        Documenti_Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        SottoTitolo = c.String(),
                        Evento = c.Int(nullable: false),
                        Descrizione = c.String(),
                    })
                .PrimaryKey(t => t.Documenti_Id);
            
            CreateTable(
                "dbo.Eventis",
                c => new
                    {
                        Evento_Id = c.Int(nullable: false, identity: true),
                        NomeEvento = c.String(),
                    })
                .PrimaryKey(t => t.Evento_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Eventis");
            DropTable("dbo.Documentis");
            DropTable("dbo.DocRuolis");
        }
    }
}
