﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace BankApp
{
    public interface IBankAccount
    {
        decimal AccountBalance { get; set; }
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void AddCard(string type);
        void AppendCard(Card card);
    }

    public class BankAccount : IBankAccount
    {
        //Main Bank Account Variables
        [Key]
        public int BankID { get; set; }
        public string AccountHolder { get; set; }
        public string AccountNumber { get; set; }
        public decimal AccountBalance { get; set; }
        public int CardCount { get; set; }
        public string AccountType { get; set; }

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
        //Append a new Card to the list of cards where the card already is created
        public void AppendCard(Card card)
        {
            Cards.Add(card);
        }
        //Deposit to the account
        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }
            else if (amount > 0)
            {
                AccountBalance += amount;
            }
        }

        //Withdraw from the Account
        public void Withdraw(decimal amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("Withdrawal amount must be greater than zero.");
            }
            else if (amount <= AccountBalance)
            {
                AccountBalance -= amount;
            }

        }

        public BankAccount() { }

    }
}
