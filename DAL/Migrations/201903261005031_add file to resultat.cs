namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfiletoresultat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CheckListExigence", "IsHasDate", c => c.Boolean(nullable: false));
            AddColumn("dbo.DemandeResultatEntete", "AppFileId", c => c.Long());
            CreateIndex("dbo.DemandeResultatEntete", "AppFileId");
            AddForeignKey("dbo.DemandeResultatEntete", "AppFileId", "dbo.AppFiles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemandeResultatEntete", "AppFileId", "dbo.AppFiles");
            DropIndex("dbo.DemandeResultatEntete", new[] { "AppFileId" });
            DropColumn("dbo.DemandeResultatEntete", "AppFileId");
            DropColumn("dbo.CheckListExigence", "IsHasDate");
        }
    }
}
