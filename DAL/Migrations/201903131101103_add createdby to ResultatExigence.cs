namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcreatedbytoResultatExigence : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResultatExigence", "CreatedBy", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.ResultatExigence", "CreatedBy");
            AddForeignKey("dbo.ResultatExigence", "CreatedBy", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResultatExigence", "CreatedBy", "dbo.AspNetUsers");
            DropIndex("dbo.ResultatExigence", new[] { "CreatedBy" });
            DropColumn("dbo.ResultatExigence", "CreatedBy");
        }
    }
}
