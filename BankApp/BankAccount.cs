using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace BankApp
{
    public class BankAccount
    {
        //Main Bank Account Variables
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public int CardCount { get; set; }

        //Sub Class Lists
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<Transaction> Transactions { get; set; } = new List<Transaction>();
        public List<Wallet> Wallets { get; set; } = new List<Wallet>();


        //Constructor
        public BankAccount(string accountHolder)
        {
            Random rng = new Random();
            int randNum = rng.Next(1000000, 9999999); 

            AccountHolder = accountHolder;
            AccountNumber = randNum.ToString();
            AccountBalance = 15545355.00m;
        }

        //Add Card
        public void AddCard(string type)
        {
            Card card = new Card(type, DateTime.Now);
            Cards.Add(card);
            CardCount++;
        }

        /// <summary>
        /// Deposit Money into the Bank Account
        /// </summary>
        /// <param name="amount"></param>
        public void DepositMoney(decimal amount)
        {

        }

        /// <summary>
        /// Withdraw Money from the Bank Account
        /// </summary>
        /// <param name="amount"></param>
        public void WithdrawMoney(decimal amount)
        {

        }

    }
}
