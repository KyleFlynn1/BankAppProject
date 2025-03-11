using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankApp;

namespace DatabaseManagement
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UserData db = new UserData();

            using (db)
            {
                UserAccount a1 = new UserAccount() { UserID = 1, Username = "Test", Password = "123", BankAccountID = 1 };
                BankAccount b1 = new BankAccount("Kyle F") { BankID = 1, UserAccount = a1, UserID = 1, CardID = new int[] { 1, 2 } };
                Card c1 = new Card("Debit", DateTime.Now) { BankAccount = b1, BankAccountID = 1, CardID = 1};
                Card c2 = new Card("Credit", DateTime.Now) { BankAccount = b1, BankAccountID = 1, CardID = 2};
                b1.AppendCard(c1);
                b1.AppendCard(c2);
                a1.BankAccounts.Add(b1);

                db.Users.Add(a1);

                Console.WriteLine("Added Bank Accoutns");

                db.BankAccounts.Add(b1);

                Console.WriteLine("Added User Accounts");

                db.Cards.Add(c1);   
                db.Cards.Add(c2);

                Console.WriteLine("Added Cards");

                db.SaveChanges();

                Console.WriteLine("Saved to DataBase");
            }
        }
    }
}
