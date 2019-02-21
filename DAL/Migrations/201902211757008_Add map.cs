namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addmap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MAP_InfoGenerale_TypeCheckList",
                c => new
                    {
                        InfoGeneraleId = c.Long(nullable: false),
                        TypeCheckListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InfoGeneraleId, t.TypeCheckListId })
                .ForeignKey("dbo.InfoGenerale", t => t.InfoGeneraleId, cascadeDelete: true)
                .ForeignKey("dbo.TypeCheckList", t => t.TypeCheckListId, cascadeDelete: true)
                .Index(t => t.InfoGeneraleId)
                .Index(t => t.TypeCheckListId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.MAP_InfoGenerale_TypeCheckList", "InfoGeneraleId", "dbo.InfoGenerale");
            DropIndex("dbo.MAP_InfoGenerale_TypeCheckList", new[] { "TypeCheckListId" });
            DropIndex("dbo.MAP_InfoGenerale_TypeCheckList", new[] { "InfoGeneraleId" });
            DropTable("dbo.MAP_InfoGenerale_TypeCheckList");
        }
    }
}
