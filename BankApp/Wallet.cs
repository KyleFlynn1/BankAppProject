using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class Wallet
    {
        //Variables
        [Key]
        public int WalletID { get; set; }
        public string WalletAddress { get; set; }


        //Foreign Keys
        public int UserID { get; set; }
        public virtual UserAccount UserAccount { get; set; } = null;


        public Wallet(int userID)
        {
            string characters = "0123456789abcdef";
            var random = new Random();
            var sb = new StringBuilder();

            sb.Append("0x");

            for (int i = 0; i < 40; i++)
            {
                int index = random.Next(characters.Length);
                sb.Append(characters[index]);
            }

            WalletAddress = sb.ToString();
        }
        public Wallet() { }

    }

}
