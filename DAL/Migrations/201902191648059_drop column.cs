namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropcolumn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.CheckListExigence", "Poids");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CheckListExigence", "Poids", c => c.Double(nullable: false));
        }
    }
}
