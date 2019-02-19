namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAppFileTbale : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AppFiles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        SourceName = c.String(),
                        SourceId = c.String(),
                        ContainerName = c.String(),
                        OriginalFileName = c.String(),
                        SystemFileName = c.String(),
                        FileSize = c.Long(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CheckListExigence",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CheckListRubriqueId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                        ShowOrder = c.Int(nullable: false),
                        IsActif = c.Boolean(nullable: false),
                        Poids = c.Double(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CheckListRubrique", t => t.CheckListRubriqueId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.CheckListRubriqueId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.CheckListRubrique",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeCheckListId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        ShowOrder = c.Int(nullable: false),
                        IsActif = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeCheckList", t => t.TypeCheckListId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.TypeCheckListId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.TypeCheckList",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.DemandeAccesEngin",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeCheckListId = c.Int(nullable: false),
                        Observation = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeCheckList", t => t.TypeCheckListId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.TypeCheckListId)
                .Index(t => t.CreatedBy);
            
            CreateTable(
                "dbo.ResultatExigence",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DemandeAccesEnginId = c.Long(nullable: false),
                        CheckListExigenceId = c.Long(nullable: false),
                        IsConform = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Oobservation = c.String(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DemandeAccesEngin", t => t.DemandeAccesEnginId)
                .ForeignKey("dbo.CheckListExigence", t => t.CheckListExigenceId)
                .Index(t => t.DemandeAccesEnginId)
                .Index(t => t.CheckListExigenceId);
            
            CreateTable(
                "dbo.Profile",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Details = c.String(),
                        DtLastConnection = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        RoleId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.RoleId, t.UserId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.TypeCheckList", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.Profile", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DemandeAccesEngin", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.CheckListRubrique", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.CheckListExigence", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResultatExigence", "CheckListExigenceId", "dbo.CheckListExigence");
            DropForeignKey("dbo.DemandeAccesEngin", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.ResultatExigence", "DemandeAccesEnginId", "dbo.DemandeAccesEngin");
            DropForeignKey("dbo.CheckListRubrique", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.CheckListExigence", "CheckListRubriqueId", "dbo.CheckListRubrique");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.Profile", new[] { "Id" });
            DropIndex("dbo.ResultatExigence", new[] { "CheckListExigenceId" });
            DropIndex("dbo.ResultatExigence", new[] { "DemandeAccesEnginId" });
            DropIndex("dbo.DemandeAccesEngin", new[] { "CreatedBy" });
            DropIndex("dbo.DemandeAccesEngin", new[] { "TypeCheckListId" });
            DropIndex("dbo.TypeCheckList", new[] { "CreatedBy" });
            DropIndex("dbo.CheckListRubrique", new[] { "CreatedBy" });
            DropIndex("dbo.CheckListRubrique", new[] { "TypeCheckListId" });
            DropIndex("dbo.CheckListExigence", new[] { "CreatedBy" });
            DropIndex("dbo.CheckListExigence", new[] { "CheckListRubriqueId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.Profile");
            DropTable("dbo.ResultatExigence");
            DropTable("dbo.DemandeAccesEngin");
            DropTable("dbo.TypeCheckList");
            DropTable("dbo.CheckListRubrique");
            DropTable("dbo.CheckListExigence");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AppFiles");
        }
    }
}
