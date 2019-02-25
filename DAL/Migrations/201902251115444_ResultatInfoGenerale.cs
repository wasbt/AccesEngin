namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResultatInfoGenerale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResultatInfoGenerale",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DemandeAccesEnginId = c.Long(nullable: false),
                        InfoGeneraleId = c.Long(nullable: false),
                        IsConform = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Observation = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InfoGenerale", t => t.InfoGeneraleId)
                .ForeignKey("dbo.DemandeAccesEngin", t => t.DemandeAccesEnginId)
                .Index(t => t.DemandeAccesEnginId)
                .Index(t => t.InfoGeneraleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultatInfoGenerale", "DemandeAccesEnginId", "dbo.DemandeAccesEngin");
            DropForeignKey("dbo.ResultatInfoGenerale", "InfoGeneraleId", "dbo.InfoGenerale");
            DropIndex("dbo.ResultatInfoGenerale", new[] { "InfoGeneraleId" });
            DropIndex("dbo.ResultatInfoGenerale", new[] { "DemandeAccesEnginId" });
            DropTable("dbo.ResultatInfoGenerale");
        }
    }
}
