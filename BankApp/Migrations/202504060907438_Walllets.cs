namespace BankApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Walllets : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CoinTransactions",
                c => new
                    {
                        TransactionID = c.Int(nullable: false, identity: true),
                        WalletID = c.Int(nullable: false),
                        CoinUUID = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TransactionDate = c.DateTime(nullable: false),
                        TransactionType = c.String(),
                        TotalValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.TransactionID);
            
            CreateTable(
                "dbo.OwnedCoins",
                c => new
                    {
                        CoinUUID = c.String(nullable: false, maxLength: 128),
                        WalletID = c.Int(nullable: false),
                        OwnedAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AvgPricePerCoin = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CurrentValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CoinUUID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.OwnedCoins");
            DropTable("dbo.CoinTransactions");
        }
    }
}
