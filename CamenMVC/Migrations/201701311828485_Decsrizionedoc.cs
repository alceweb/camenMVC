namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Decsrizionedoc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documentis", "Descrizione", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Documentis", "Descrizione");
        }
    }
}
