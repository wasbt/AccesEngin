namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIdtolong : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CheckListRubrique", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.DemandeAccesEngin", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.NatureMatiere", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.TypeEngin", "TypeCheckListId", "dbo.TypeCheckList");
            DropIndex("dbo.CheckListRubrique", new[] { "TypeCheckListId" });
            DropIndex("dbo.DemandeAccesEngin", new[] { "TypeCheckListId" });
            DropIndex("dbo.NatureMatiere", new[] { "TypeCheckListId" });
            DropIndex("dbo.TypeEngin", new[] { "TypeCheckListId" });
            DropIndex("dbo.MAP_InfoGenerale_TypeCheckList", new[] { "TypeCheckListId" });
            DropPrimaryKey("dbo.TypeCheckList");
            DropPrimaryKey("dbo.MAP_InfoGenerale_TypeCheckList");
            AlterColumn("dbo.CheckListRubrique", "TypeCheckListId", c => c.Long(nullable: false));
            AlterColumn("dbo.TypeCheckList", "Id", c => c.Long(nullable: false, identity: true));
            AlterColumn("dbo.DemandeAccesEngin", "TypeCheckListId", c => c.Long(nullable: false));
            AlterColumn("dbo.NatureMatiere", "TypeCheckListId", c => c.Long(nullable: false));
            AlterColumn("dbo.TypeEngin", "TypeCheckListId", c => c.Long(nullable: false));
            AlterColumn("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", c => c.Long(nullable: false));
            AddPrimaryKey("dbo.TypeCheckList", "Id");
            AddPrimaryKey("dbo.MAP_InfoGenerale_TypeCheckList", new[] { "InfoGeneraleId", "TypeCheckListId" });
            CreateIndex("dbo.CheckListRubrique", "TypeCheckListId");
            CreateIndex("dbo.DemandeAccesEngin", "TypeCheckListId");
            CreateIndex("dbo.NatureMatiere", "TypeCheckListId");
            CreateIndex("dbo.TypeEngin", "TypeCheckListId");
            CreateIndex("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId");
            AddForeignKey("dbo.CheckListRubrique", "TypeCheckListId", "dbo.TypeCheckList", "Id");
            AddForeignKey("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", "dbo.TypeCheckList", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DemandeAccesEngin", "TypeCheckListId", "dbo.TypeCheckList", "Id");
            AddForeignKey("dbo.NatureMatiere", "TypeCheckListId", "dbo.TypeCheckList", "Id");
            AddForeignKey("dbo.TypeEngin", "TypeCheckListId", "dbo.TypeCheckList", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TypeEngin", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.NatureMatiere", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.DemandeAccesEngin", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", "dbo.TypeCheckList");
            DropForeignKey("dbo.CheckListRubrique", "TypeCheckListId", "dbo.TypeCheckList");
            DropIndex("dbo.MAP_InfoGenerale_TypeCheckList", new[] { "TypeCheckListId" });
            DropIndex("dbo.TypeEngin", new[] { "TypeCheckListId" });
            DropIndex("dbo.NatureMatiere", new[] { "TypeCheckListId" });
            DropIndex("dbo.DemandeAccesEngin", new[] { "TypeCheckListId" });
            DropIndex("dbo.CheckListRubrique", new[] { "TypeCheckListId" });
            DropPrimaryKey("dbo.MAP_InfoGenerale_TypeCheckList");
            DropPrimaryKey("dbo.TypeCheckList");
            AlterColumn("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", c => c.Int(nullable: false));
            AlterColumn("dbo.TypeEngin", "TypeCheckListId", c => c.Int(nullable: false));
            AlterColumn("dbo.NatureMatiere", "TypeCheckListId", c => c.Int(nullable: false));
            AlterColumn("dbo.DemandeAccesEngin", "TypeCheckListId", c => c.Int(nullable: false));
            AlterColumn("dbo.TypeCheckList", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.CheckListRubrique", "TypeCheckListId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.MAP_InfoGenerale_TypeCheckList", new[] { "InfoGeneraleId", "TypeCheckListId" });
            AddPrimaryKey("dbo.TypeCheckList", "Id");
            CreateIndex("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId");
            CreateIndex("dbo.TypeEngin", "TypeCheckListId");
            CreateIndex("dbo.NatureMatiere", "TypeCheckListId");
            CreateIndex("dbo.DemandeAccesEngin", "TypeCheckListId");
            CreateIndex("dbo.CheckListRubrique", "TypeCheckListId");
            AddForeignKey("dbo.TypeEngin", "TypeCheckListId", "dbo.TypeCheckList", "Id");
            AddForeignKey("dbo.NatureMatiere", "TypeCheckListId", "dbo.TypeCheckList", "Id");
            AddForeignKey("dbo.DemandeAccesEngin", "TypeCheckListId", "dbo.TypeCheckList", "Id");
            AddForeignKey("dbo.MAP_InfoGenerale_TypeCheckList", "TypeCheckListId", "dbo.TypeCheckList", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CheckListRubrique", "TypeCheckListId", "dbo.TypeCheckList", "Id");
        }
    }
}
