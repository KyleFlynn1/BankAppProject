namespace BankApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Wallets : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Wallets", "WalletAddress", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Wallets", "WalletAddress", c => c.Int(nullable: false));
        }
    }
}
