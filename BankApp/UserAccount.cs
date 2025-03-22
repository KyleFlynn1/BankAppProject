using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class UserAccount
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        //Foreign Keys
        public int BankAccountID { get; set; }
        public int WalletID { get; set; }

        //Sub Classes
        public virtual List<BankAccount> BankAccounts { get; set; } = new List<BankAccount>();
        public virtual List<Wallet> Wallets { get; set; } = new List<Wallet>();

    }

    public class UserData : DbContext
    {
        public UserData() : base("MyUserData") { }

        public DbSet<BankAccount> BankAccounts { get; set;}
        public DbSet<Card> Cards { get; set;}
        public DbSet<Transaction> Transactions { get; set;}
        public DbSet<Wallet> Wallets { get; set;}
        public DbSet<UserAccount> Users { get; set;}

    }
}
