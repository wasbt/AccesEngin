namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class columndatesortie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DemandeAccesEngin", "DateSortie", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DemandeAccesEngin", "DateSortie");
        }
    }
}
