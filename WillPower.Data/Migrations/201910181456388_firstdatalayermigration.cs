namespace WillPower.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstdatalayermigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GoalItem",
                c => new
                    {
                        GoalItemID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        GoalItemName = c.String(nullable: false),
                        GoalItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoalItemLocation = c.String(),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.GoalItemID);
            
            CreateTable(
                "dbo.MonthlyFinance",
                c => new
                    {
                        MonthlyFinanceID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        MonthlyTakeHome = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CostOfBills = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GoalItemID = c.Int(nullable: false),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.MonthlyFinanceID)
                .ForeignKey("dbo.GoalItem", t => t.GoalItemID, cascadeDelete: true)
                .Index(t => t.GoalItemID);
            
            CreateTable(
                "dbo.NoBuy",
                c => new
                    {
                        ItemID = c.Int(nullable: false, identity: true),
                        UserID = c.Guid(nullable: false),
                        ItemName = c.String(nullable: false),
                        ItemPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ItemLocation = c.String(),
                        GoalItemID = c.Int(nullable: false),
                        CreatedUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        ModifiedUTC = c.DateTimeOffset(precision: 7),
                    })
                .PrimaryKey(t => t.ItemID)
                .ForeignKey("dbo.GoalItem", t => t.GoalItemID, cascadeDelete: true)
                .Index(t => t.GoalItemID);
            
            CreateTable(
                "dbo.IdentityRole",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(),
                        IdentityRole_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.IdentityRole", t => t.IdentityRole_Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.IdentityRole_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.ApplicationUser",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.IdentityUserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.IdentityUserLogin",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.ApplicationUser", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IdentityUserRole", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserLogin", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserClaim", "ApplicationUser_Id", "dbo.ApplicationUser");
            DropForeignKey("dbo.IdentityUserRole", "IdentityRole_Id", "dbo.IdentityRole");
            DropForeignKey("dbo.NoBuy", "GoalItemID", "dbo.GoalItem");
            DropForeignKey("dbo.MonthlyFinance", "GoalItemID", "dbo.GoalItem");
            DropIndex("dbo.IdentityUserLogin", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserClaim", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.IdentityUserRole", new[] { "IdentityRole_Id" });
            DropIndex("dbo.NoBuy", new[] { "GoalItemID" });
            DropIndex("dbo.MonthlyFinance", new[] { "GoalItemID" });
            DropTable("dbo.IdentityUserLogin");
            DropTable("dbo.IdentityUserClaim");
            DropTable("dbo.ApplicationUser");
            DropTable("dbo.IdentityUserRole");
            DropTable("dbo.IdentityRole");
            DropTable("dbo.NoBuy");
            DropTable("dbo.MonthlyFinance");
            DropTable("dbo.GoalItem");
        }
    }
}
