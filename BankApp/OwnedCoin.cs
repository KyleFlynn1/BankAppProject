using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp
{
    public class OwnedCoin
    {
        [Key]
        public int OwnedCoinID { get; set; }
        public string CoinUUID { get; set; }
        public int WalletID { get; set; }
        public decimal OwnedAmount { get; set; }
        public decimal AvgPricePerCoin { get; set; }
        public decimal CurrentValue { get; set; }

        public OwnedCoin(string coinUUID)
        {
            CoinUUID = coinUUID;
        }

        public OwnedCoin() { }

    }
}
