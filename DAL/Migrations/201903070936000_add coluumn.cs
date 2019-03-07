namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcoluumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DemandeAccesEngin", "DatePlannification", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DemandeAccesEngin", "DatePlannification");
        }
    }
}
