namespace WillPower.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hopefullyfixingnullissue : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MonthlyFinance", "GoalItemID", "dbo.GoalItem");
            DropForeignKey("dbo.NoBuy", "GoalItemID", "dbo.GoalItem");
            DropIndex("dbo.MonthlyFinance", new[] { "GoalItemID" });
            DropIndex("dbo.NoBuy", new[] { "GoalItemID" });
            AlterColumn("dbo.MonthlyFinance", "GoalItemID", c => c.Int());
            AlterColumn("dbo.NoBuy", "GoalItemID", c => c.Int());
            CreateIndex("dbo.MonthlyFinance", "GoalItemID");
            CreateIndex("dbo.NoBuy", "GoalItemID");
            AddForeignKey("dbo.MonthlyFinance", "GoalItemID", "dbo.GoalItem", "GoalItemID");
            AddForeignKey("dbo.NoBuy", "GoalItemID", "dbo.GoalItem", "GoalItemID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NoBuy", "GoalItemID", "dbo.GoalItem");
            DropForeignKey("dbo.MonthlyFinance", "GoalItemID", "dbo.GoalItem");
            DropIndex("dbo.NoBuy", new[] { "GoalItemID" });
            DropIndex("dbo.MonthlyFinance", new[] { "GoalItemID" });
            AlterColumn("dbo.NoBuy", "GoalItemID", c => c.Int(nullable: false));
            AlterColumn("dbo.MonthlyFinance", "GoalItemID", c => c.Int(nullable: false));
            CreateIndex("dbo.NoBuy", "GoalItemID");
            CreateIndex("dbo.MonthlyFinance", "GoalItemID");
            AddForeignKey("dbo.NoBuy", "GoalItemID", "dbo.GoalItem", "GoalItemID", cascadeDelete: true);
            AddForeignKey("dbo.MonthlyFinance", "GoalItemID", "dbo.GoalItem", "GoalItemID", cascadeDelete: true);
        }
    }
}
