namespace WillPower.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondmigrationaddeddatatomonthlyfinance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MonthlyFinance", "Month", c => c.Int(nullable: false));
            AddColumn("dbo.MonthlyFinance", "Year", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MonthlyFinance", "Year");
            DropColumn("dbo.MonthlyFinance", "Month");
        }
    }
}
