using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace BankApp
{
    public class BankAccount
    {
        //Main Bank Account Variables
        [Key]
        public int BankID { get; set; }
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public int CardCount { get; set; }

        //Foreign Keys
        public virtual UserAccount UserAccount { get; set; }
        public int UserID { get; set; }
        public int[] TransactionID { get; set; }
        public int[] CardID{ get; set; }

        public virtual List<Card> Cards { get; set; } = new List<Card>();
        public virtual List<Transaction> Transactions { get; set; } = new List<Transaction>();
        


        //Constructor
        public BankAccount(string accountHolder)
        {
            Random rng = new Random();
            int randNum = rng.Next(1000000, 9999999); 

            AccountHolder = accountHolder;
            AccountNumber = randNum.ToString();
            AccountBalance = 0.00m;
        }

        //Add Card
        public void AddCard(string type)
        {
            Card card = new Card(type, DateTime.Now);
            Cards.Add(card);
            CardCount++;
        }

        public void AppendCard(Card card)
        {
            Cards.Add(card);
        }

        public void Deposit(decimal amount)
        {
            AccountBalance += amount;
        }


        public void Withdraw(decimal amount)
        {
            if(AccountBalance >= amount)
            {
                AccountBalance -= amount;
            }
        }

        public BankAccount() { }

    }
}
