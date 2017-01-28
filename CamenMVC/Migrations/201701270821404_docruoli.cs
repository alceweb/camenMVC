namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class docruoli : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocRuolis",
                c => new
                    {
                        DocRuoli_Id = c.Int(nullable: false, identity: true),
                        Documento_Id = c.Int(nullable: false),
                        RuoloId = c.String(),
                    })
                .PrimaryKey(t => t.DocRuoli_Id)
                .ForeignKey("dbo.Documentis", t => t.Documento_Id, cascadeDelete: true)
                .Index(t => t.Documento_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocRuolis", "Documento_Id", "dbo.Documentis");
            DropIndex("dbo.DocRuolis", new[] { "Documento_Id" });
            DropTable("dbo.DocRuolis");
        }
    }
}
