namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtableentete : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResultatExigence", "DemandeAccesEnginId", "dbo.DemandeAccesEngin");
            DropForeignKey("dbo.ResultatExigence", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.ResultatExigence", new[] { "DemandeAccesEnginId" });
            DropIndex("dbo.ResultatExigence", new[] { "CreatedBy" });
            CreateTable(
                "dbo.DemandeResultatEntete",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DemandeAccesEnginId = c.Long(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DemandeAccesEngin", t => t.DemandeAccesEnginId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.DemandeAccesEnginId)
                .Index(t => t.CreatedBy);
            
            AddColumn("dbo.ResultatExigence", "DemandeResultatEnteteId", c => c.Long(nullable: false));
            CreateIndex("dbo.ResultatExigence", "DemandeResultatEnteteId");
            AddForeignKey("dbo.ResultatExigence", "DemandeResultatEnteteId", "dbo.DemandeResultatEntete", "Id");
            DropColumn("dbo.ResultatExigence", "DemandeAccesEnginId");
            DropColumn("dbo.ResultatExigence", "CreatedOn");
            DropColumn("dbo.ResultatExigence", "CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ResultatExigence", "CreatedBy", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ResultatExigence", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.ResultatExigence", "DemandeAccesEnginId", c => c.Long(nullable: false));
            DropForeignKey("dbo.DemandeResultatEntete", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.DemandeResultatEntete", "DemandeAccesEnginId", "dbo.DemandeAccesEngin");
            DropForeignKey("dbo.ResultatExigence", "DemandeResultatEnteteId", "dbo.DemandeResultatEntete");
            DropIndex("dbo.ResultatExigence", new[] { "DemandeResultatEnteteId" });
            DropIndex("dbo.DemandeResultatEntete", new[] { "CreatedBy" });
            DropIndex("dbo.DemandeResultatEntete", new[] { "DemandeAccesEnginId" });
            DropColumn("dbo.ResultatExigence", "DemandeResultatEnteteId");
            DropTable("dbo.DemandeResultatEntete");
            CreateIndex("dbo.ResultatExigence", "CreatedBy");
            CreateIndex("dbo.ResultatExigence", "DemandeAccesEnginId");
            AddForeignKey("dbo.ResultatExigence", "CreatedBy", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ResultatExigence", "DemandeAccesEnginId", "dbo.DemandeAccesEngin", "Id");
        }
    }
}
