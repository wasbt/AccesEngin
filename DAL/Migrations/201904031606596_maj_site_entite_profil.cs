namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class maj_site_entite_profil : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Entities", "HSEEntiteUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Entities", "ADFUserId", c => c.String());
            AddColumn("dbo.Entities", "ResponsableEntiteUserId", c => c.String());
            AddColumn("dbo.Entities", "IsHSE", c => c.Boolean());
            AddColumn("dbo.Sites", "HSESiteId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Profile", "EntiteId", c => c.Long());
            CreateIndex("dbo.Entities", "HSEEntiteUserId");
            CreateIndex("dbo.Profile", "EntiteId");
            CreateIndex("dbo.Sites", "HSESiteId");
            AddForeignKey("dbo.Entities", "HSEEntiteUserId", "dbo.Profile", "Id");
            AddForeignKey("dbo.Sites", "HSESiteId", "dbo.Profile", "Id");
            AddForeignKey("dbo.Profile", "EntiteId", "dbo.Entities", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Profile", "EntiteId", "dbo.Entities");
            DropForeignKey("dbo.Sites", "HSESiteId", "dbo.Profile");
            DropForeignKey("dbo.Entities", "HSEEntiteUserId", "dbo.Profile");
            DropIndex("dbo.Sites", new[] { "HSESiteId" });
            DropIndex("dbo.Profile", new[] { "EntiteId" });
            DropIndex("dbo.Entities", new[] { "HSEEntiteUserId" });
            DropColumn("dbo.Profile", "EntiteId");
            DropColumn("dbo.Sites", "HSESiteId");
            DropColumn("dbo.Entities", "IsHSE");
            DropColumn("dbo.Entities", "ResponsableEntiteUserId");
            DropColumn("dbo.Entities", "ADFUserId");
            DropColumn("dbo.Entities", "HSEEntiteUserId");
        }
    }
}
