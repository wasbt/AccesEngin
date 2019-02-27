namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfkdemandtypeEnngin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DemandeAccesEngin", "TypeEnginId", c => c.Int(nullable: false));
            AddColumn("dbo.DemandeAccesEngin", "TypeEngin_Id", c => c.Long(nullable: false));
            CreateIndex("dbo.DemandeAccesEngin", "TypeEngin_Id");
            AddForeignKey("dbo.DemandeAccesEngin", "TypeEngin_Id", "dbo.TypeEngin", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DemandeAccesEngin", "TypeEngin_Id", "dbo.TypeEngin");
            DropIndex("dbo.DemandeAccesEngin", new[] { "TypeEngin_Id" });
            DropColumn("dbo.DemandeAccesEngin", "TypeEngin_Id");
            DropColumn("dbo.DemandeAccesEngin", "TypeEnginId");
        }
    }
}
