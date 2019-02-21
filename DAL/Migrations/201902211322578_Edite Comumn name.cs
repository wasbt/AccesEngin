namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditeComumnname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultatExigence", "Observation", c => c.String(nullable: false));
            DropColumn("dbo.ResultatExigence", "Oobservation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultatExigence", "Oobservation", c => c.String(nullable: false));
            DropColumn("dbo.ResultatExigence", "Observation");
        }
    }
}
