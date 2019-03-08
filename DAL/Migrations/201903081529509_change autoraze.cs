namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeautoraze : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DemandeAccesEngin", "Autorise", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DemandeAccesEngin", "Autorise", c => c.Boolean());
        }
    }
}
