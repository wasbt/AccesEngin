namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tableREF_Mailing : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.REF_MailingList",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        EntityId = c.Long(nullable: false),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Entities", t => t.EntityId)
                .Index(t => t.EntityId);
            
            CreateTable(
                "dbo.Map_REF_MailingListAspNetUsers",
                c => new
                    {
                        MailingListId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.MailingListId, t.UserId })
                .ForeignKey("dbo.REF_MailingList", t => t.MailingListId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.MailingListId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.REF_MailingList", "EntityId", "dbo.Entities");
            DropForeignKey("dbo.Map_REF_MailingListAspNetUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Map_REF_MailingListAspNetUsers", "MailingListId", "dbo.REF_MailingList");
            DropIndex("dbo.Map_REF_MailingListAspNetUsers", new[] { "UserId" });
            DropIndex("dbo.Map_REF_MailingListAspNetUsers", new[] { "MailingListId" });
            DropIndex("dbo.REF_MailingList", new[] { "EntityId" });
            DropTable("dbo.Map_REF_MailingListAspNetUsers");
            DropTable("dbo.REF_MailingList");
        }
    }
}
