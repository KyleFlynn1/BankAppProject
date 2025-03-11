using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankApp
{
    public class Card
    {
        [Key]
        public int CardID { get; set; }
        public string CardType { get; set; }
        public int CardNumberS1 { get; set; }
        public int CardNumberS2 { get; set; }
        public int CardNumberS3 { get; set; }
        public int CVV { get; set; }
        public decimal CreditLimit { get; set; }
        public DateTime ExpiryDate { get; set; }

        //Foreign Key
        public int BankAccountID { get; set; }
        public virtual BankAccount BankAccount { get; set; } = null;


        //Construtor
        public Card(string cardType, DateTime expiryDate)
        {
            Random rng = new Random();

            CardType = cardType;
            CardNumberS1 = rng.Next(1000, 9999); 
            CardNumberS2 = rng.Next(1000, 9999); 
            CardNumberS3 = rng.Next(1000, 9999); 
            CVV = rng.Next(100, 999);
            ExpiryDate = expiryDate.AddYears(4);
            if (cardType == "Credit")
            {
                CreditLimit = 6000.00m; 
            }
        }


    }
}
