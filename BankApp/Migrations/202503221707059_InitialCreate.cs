namespace BankApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BankAccounts",
                c => new
                    {
                        BankID = c.Int(nullable: false, identity: true),
                        AccountHolder = c.String(),
                        AccountNumber = c.String(),
                        AccountBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CardCount = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BankID)
                .ForeignKey("dbo.UserAccounts", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Cards",
                c => new
                    {
                        CardID = c.Int(nullable: false, identity: true),
                        CardType = c.String(),
                        CardNumberS1 = c.Int(nullable: false),
                        CardNumberS2 = c.Int(nullable: false),
                        CardNumberS3 = c.Int(nullable: false),
                        CVV = c.Int(nullable: false),
                        CreditLimit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ExpiryDate = c.DateTime(nullable: false),
                        BankAccountID = c.Int(nullable: false),
                        BankAccount_BankID = c.Int(),
                    })
                .PrimaryKey(t => t.CardID)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_BankID)
                .Index(t => t.BankAccount_BankID);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        TransactionAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(),
                        OtherParty = c.String(),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BankAccountID = c.Int(nullable: false),
                        BankAccount_BankID = c.Int(),
                    })
                .PrimaryKey(t => t.TransactionID)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccount_BankID)
                .Index(t => t.BankAccount_BankID);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        BankAccountID = c.Int(nullable: false),
                        WalletID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Wallets",
                c => new
                    {
                        WalletID = c.Int(nullable: false, identity: true),
                        WalletAddress = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.WalletID)
                .ForeignKey("dbo.UserAccounts", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Wallets", "UserID", "dbo.UserAccounts");
            DropForeignKey("dbo.BankAccounts", "UserID", "dbo.UserAccounts");
            DropForeignKey("dbo.Transactions", "BankAccount_BankID", "dbo.BankAccounts");
            DropForeignKey("dbo.Cards", "BankAccount_BankID", "dbo.BankAccounts");
            DropIndex("dbo.Wallets", new[] { "UserID" });
            DropIndex("dbo.Transactions", new[] { "BankAccount_BankID" });
            DropIndex("dbo.Cards", new[] { "BankAccount_BankID" });
            DropIndex("dbo.BankAccounts", new[] { "UserID" });
            DropTable("dbo.Wallets");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Transactions");
            DropTable("dbo.Cards");
            DropTable("dbo.BankAccounts");
        }
    }
}
