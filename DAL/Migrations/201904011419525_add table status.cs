namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablestatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StatutDemandes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DemandeAccesEngin", "StatutDemandeId", c => c.Long());
            CreateIndex("dbo.DemandeAccesEngin", "StatutDemandeId");
            AddForeignKey("dbo.DemandeAccesEngin", "StatutDemandeId", "dbo.StatutDemandes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemandeAccesEngin", "StatutDemandeId", "dbo.StatutDemandes");
            DropIndex("dbo.DemandeAccesEngin", new[] { "StatutDemandeId" });
            DropColumn("dbo.DemandeAccesEngin", "StatutDemandeId");
            DropTable("dbo.StatutDemandes");
        }
    }
}
