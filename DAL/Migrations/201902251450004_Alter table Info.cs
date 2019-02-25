namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AltertableInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultatInfoGenerale", "ValueInfo", c => c.String(nullable: false));
            DropColumn("dbo.ResultatInfoGenerale", "IsConform");
            DropColumn("dbo.ResultatInfoGenerale", "Date");
            DropColumn("dbo.ResultatInfoGenerale", "Observation");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultatInfoGenerale", "Observation", c => c.String(nullable: false));
            AddColumn("dbo.ResultatInfoGenerale", "Date", c => c.DateTime(nullable: false));
            AddColumn("dbo.ResultatInfoGenerale", "IsConform", c => c.Boolean(nullable: false));
            DropColumn("dbo.ResultatInfoGenerale", "ValueInfo");
        }
    }
}
