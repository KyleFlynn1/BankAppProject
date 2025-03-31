using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace BankApp
{
    public class AllTimeHigh
    {
        public string price { get; set; }
        public int timestamp { get; set; }
    }

    public class Coin
    {
        public string uuid { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string color { get; set; }
        public string iconUrl { get; set; }
        public string websiteUrl { get; set; }
        public List<Link> links { get; set; }
        public Supply supply { get; set; }
        public string _24hVolume { get; set; }
        public string marketCap { get; set; }
        public string price { get; set; }
        public string btcPrice { get; set; }
        public string change { get; set; }
        public int rank { get; set; }
        public int numberOfMarkets { get; set; }
        public int numberOfExchanges { get; set; }
        public List<string> sparkline { get; set; }
        public AllTimeHigh allTimeHigh { get; set; }
        public string coinrankingUrl { get; set; }
    }

    public class Data
    {
        public Coin coin { get; set; }
    }

    public class Link
    {
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
    }

    public class Root
    {
        public string status { get; set; }
        public Data data { get; set; }
    }

    public class Supply
    {
        public bool confirmed { get; set; }
        public string circulating { get; set; }
        public string total { get; set; }
    }


}
