namespace BankApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBankTypeToBank : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BankAccounts", "AccountType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BankAccounts", "AccountType");
        }
    }
}
