namespace CamenMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anagrafica1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Città", c => c.String());
            AddColumn("dbo.AspNetUsers", "CAP", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "CAP");
            DropColumn("dbo.AspNetUsers", "Città");
        }
    }
}
