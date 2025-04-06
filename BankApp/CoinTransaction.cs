using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class CoinTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public int WalletID { get; set; }
        public string CoinUUID { get; set; }
        public decimal Amount { get; set; }
        public decimal Price { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal TotalValue { get; set; } 

        public CoinTransaction(int walletID, string coinUUID, decimal amount, decimal price, DateTime transactionDate, string transactionType)
        {
            WalletID = walletID;
            CoinUUID = coinUUID;
            Amount = amount;
            Price = price;
            TransactionDate = transactionDate;
            TransactionType = transactionType;
            TotalValue = amount * price;
        }

        public CoinTransaction() { }

    }
}
