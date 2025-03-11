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
        public int WalletAddress { get; set; }


        //Foreign Keys
        public int UserID { get; set; }
        public virtual UserAccount UserAccount { get; set; } = null;

    }
}
