namespace BankApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Coins : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OwnedCoins");
            AddColumn("dbo.OwnedCoins", "OwnedCoinID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.OwnedCoins", "CoinUUID", c => c.String());
            AddPrimaryKey("dbo.OwnedCoins", "OwnedCoinID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.OwnedCoins");
            AlterColumn("dbo.OwnedCoins", "CoinUUID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.OwnedCoins", "OwnedCoinID");
            AddPrimaryKey("dbo.OwnedCoins", "CoinUUID");
        }
    }
}
