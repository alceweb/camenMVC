namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nomefilenotnull : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Documentis", "NomeFile", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Documentis", "NomeFile", c => c.String());
        }
    }
}
