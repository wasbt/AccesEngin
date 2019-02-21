namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class infoGeneral : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.InfoGenerale",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        InfoGeneralRubriqueId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.InfoGeneralRubrique", t => t.InfoGeneralRubriqueId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.InfoGeneralRubriqueId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.InfoGeneralRubrique",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ShowOrder = c.Int(nullable: false),
                        IsActif = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.InfoGeneralRubrique", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.InfoGenerale", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.InfoGenerale", "InfoGeneralRubriqueId", "dbo.InfoGeneralRubrique");
            DropIndex("dbo.InfoGeneralRubrique", new[] { "CreatedBy" });
            DropIndex("dbo.InfoGenerale", new[] { "CreatedBy" });
            DropIndex("dbo.InfoGenerale", new[] { "InfoGeneralRubriqueId" });
            DropTable("dbo.InfoGeneralRubrique");
            DropTable("dbo.InfoGenerale");
        }
    }
}
