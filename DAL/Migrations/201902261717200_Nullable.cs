namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResultatExigence", "Date", c => c.DateTime());
            AlterColumn("dbo.ResultatExigence", "Observation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResultatExigence", "Observation", c => c.String(nullable: false));
            AlterColumn("dbo.ResultatExigence", "Date", c => c.DateTime(nullable: false));
        }
    }
}
