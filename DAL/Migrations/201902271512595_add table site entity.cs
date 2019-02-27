namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtablesiteentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Entities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SiteId = c.Long(nullable: false),
                        Name = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sites", t => t.SiteId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.SiteId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.Sites",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber1 = c.String(),
                        PhoneNumber2 = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            AddColumn("dbo.DemandeAccesEngin", "EntityId", c => c.Long(nullable: false));
            CreateIndex("dbo.DemandeAccesEngin", "EntityId");
            AddForeignKey("dbo.DemandeAccesEngin", "EntityId", "dbo.Entities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sites", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Entities", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Entities", "SiteId", "dbo.Sites");
            DropForeignKey("dbo.DemandeAccesEngin", "EntityId", "dbo.Entities");
            DropIndex("dbo.Sites", new[] { "CreatedBy" });
            DropIndex("dbo.Entities", new[] { "CreatedBy" });
            DropIndex("dbo.Entities", new[] { "SiteId" });
            DropIndex("dbo.DemandeAccesEngin", new[] { "EntityId" });
            DropColumn("dbo.DemandeAccesEngin", "EntityId");
            DropTable("dbo.Sites");
            DropTable("dbo.Entities");
        }
    }
}
