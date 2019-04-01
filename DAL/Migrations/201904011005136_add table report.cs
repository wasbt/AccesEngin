namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablereport : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reports",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DemandeAccesEnginId = c.Long(nullable: false),
                        AncienneDate = c.DateTime(nullable: false),
                        NouvelleDate = c.DateTime(nullable: false),
                        MotifReport = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DemandeAccesEngin", t => t.DemandeAccesEnginId)
                .Index(t => t.DemandeAccesEnginId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reports", "DemandeAccesEnginId", "dbo.DemandeAccesEngin");
            DropIndex("dbo.Reports", new[] { "DemandeAccesEnginId" });
            DropTable("dbo.Reports");
        }
    }
}
