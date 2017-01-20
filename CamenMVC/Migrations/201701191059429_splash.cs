namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class splash : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Splashes",
                c => new
                    {
                        Splash_Id = c.Int(nullable: false, identity: true),
                        Titolo = c.String(),
                        SottoTitolo = c.String(),
                        Testo = c.String(),
                        DataI = c.DateTime(nullable: false),
                        DataF = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Splash_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Splashes");
        }
    }
}
