namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removerequiredfromobservation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DemandeAccesEngin", "Observation", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DemandeAccesEngin", "Observation", c => c.String(nullable: false));
        }
    }
}
