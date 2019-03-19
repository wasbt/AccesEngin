namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrelationdemandetoappfile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DemandeAccesEngin", "AppFileId", c => c.Long());
            CreateIndex("dbo.DemandeAccesEngin", "AppFileId");
            AddForeignKey("dbo.DemandeAccesEngin", "AppFileId", "dbo.AppFiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemandeAccesEngin", "AppFileId", "dbo.AppFiles");
            DropIndex("dbo.DemandeAccesEngin", new[] { "AppFileId" });
            DropColumn("dbo.DemandeAccesEngin", "AppFileId");
        }
    }
}
