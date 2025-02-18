using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Transaction
    {
        public decimal TransactionAmount { get; set; }
        public string Description { get; set; }
        public string OtherParty { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal TransactionFee { get; set; }

        public Transaction(decimal transactionAmount, string description, string otherParty, DateTime transactionDate, decimal transactionFee)
        {
            TransactionAmount = transactionAmount;
            Description = description;
            OtherParty = otherParty;
            TransactionDate = transactionDate;
            TransactionFee = transactionFee;
        }
    }
}
