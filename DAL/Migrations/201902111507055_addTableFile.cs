namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableFile : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AppFiles");
        }
    }
}
