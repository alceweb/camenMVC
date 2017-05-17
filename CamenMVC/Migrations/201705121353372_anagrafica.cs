namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anagrafica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Indirizzo", c => c.String());
            AddColumn("dbo.AspNetUsers", "Telefono", c => c.String());
            AddColumn("dbo.AspNetUsers", "Professione", c => c.String());
            AddColumn("dbo.AspNetUsers", "Organizzazione", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Organizzazione");
            DropColumn("dbo.AspNetUsers", "Professione");
            DropColumn("dbo.AspNetUsers", "Telefono");
            DropColumn("dbo.AspNetUsers", "Indirizzo");
        }
    }
}
