namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTblTypeEngin : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TypeEngin",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TypeCheckListId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        DureeEstimative = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TypeCheckList", t => t.TypeCheckListId)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy)
                .Index(t => t.TypeCheckListId)
                .Index(t => t.CreatedBy);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeEngin", "CreatedBy", "dbo.AspNetUsers");
            DropForeignKey("dbo.TypeEngin", "TypeCheckListId", "dbo.TypeCheckList");
            DropIndex("dbo.TypeEngin", new[] { "CreatedBy" });
            DropIndex("dbo.TypeEngin", new[] { "TypeCheckListId" });
            DropTable("dbo.TypeEngin");
        }
    }
}
