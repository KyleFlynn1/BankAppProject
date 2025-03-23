using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Transaction
    {
        [Key]
        public int TransactionID { get; set; }
        public decimal TransactionAmount { get; set; }
        public string Description { get; set; }
        public string OtherParty { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionFee { get; set; }
        public string TransactionType { get; set; }

        //Foreign Key
        public int BankAccountID { get; set; }
        public virtual BankAccount BankAccount { get; set; } = null;


        public Transaction(decimal transactionAmount, string description, string otherParty, DateTime transactionDate, decimal transactionFee, string transactionType)
        {
            TransactionAmount = transactionAmount;
            Description = description;
            OtherParty = otherParty;
            TransactionDate = transactionDate;
            TransactionFee = transactionFee;
            TransactionType = transactionType;
        }

        public override string ToString()
        {
            return $"{TransactionDate.ToShortDateString()} - {TransactionAmount:c} - From {OtherParty} - Note : {Description}"; 
        }

        public Transaction() { }

    }
}
