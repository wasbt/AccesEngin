namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class edittablereport : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reports", "CreatedBy", c => c.String());
            AddColumn("dbo.Reports", "CreatedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reports", "AncienneDate");
            DropColumn("dbo.Reports", "NouvelleDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Reports", "NouvelleDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reports", "AncienneDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reports", "CreatedOn");
            DropColumn("dbo.Reports", "CreatedBy");
        }
    }
}
