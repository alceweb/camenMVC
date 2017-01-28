namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class eventoRuoli_Id : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventiRuolis",
                c => new
                    {
                        EventoRuoliId = c.Int(nullable: false, identity: true),
                        Evento_Id = c.Int(nullable: false),
                        RuoloId = c.String(),
                    })
                .PrimaryKey(t => t.EventoRuoliId)
                .ForeignKey("dbo.Eventis", t => t.Evento_Id, cascadeDelete: true)
                .Index(t => t.Evento_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventiRuolis", "Evento_Id", "dbo.Eventis");
            DropIndex("dbo.EventiRuolis", new[] { "Evento_Id" });
            DropTable("dbo.EventiRuolis");
        }
    }
}
