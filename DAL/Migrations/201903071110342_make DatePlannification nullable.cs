namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeDatePlannificationnullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DemandeAccesEngin", "DatePlannification", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DemandeAccesEngin", "DatePlannification", c => c.DateTime(nullable: false));
        }
    }
}
