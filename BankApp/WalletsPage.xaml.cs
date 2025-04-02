using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Diagnostics.Eventing.Reader;
using static System.Net.Mime.MediaTypeNames;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for WalletsPage.xaml
    /// </summary>
    public partial class WalletsPage : Page
    {
        public WalletsPage()
        {
            InitializeComponent();
            GetAPIData();
        }

        public async void GetAPIData()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://coinranking1.p.rapidapi.com/coins?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h&orderBy=marketCap&orderDirection=desc&limit=50&offset=0"),
                Headers =
                {
                    { "x-rapidapi-key", "61d3ea00eemsh04cd69a0ce65e77p1b227cjsnd38242e62bd2" },
                    { "x-rapidapi-host", "coinranking1.p.rapidapi.com" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(body);

                //Get Data for Coin 1 and Fill details in for card 1
                coinNameLBL.Content = $"{myDeserializedClass.data.coins[0].name}";
                coinSymbolLBL.Content = $"{myDeserializedClass.data.coins[0].symbol}";
                coinCurrentPriceLBL.Content = $"Current Price: {myDeserializedClass.data.coins[0].price:c2}";
                coinAllTimeHighLBL.Content = $"Market Cap: {myDeserializedClass.data.coins[0].marketCap}";
                coinRankLBL.Content = $"Coin Rank: {myDeserializedClass.data.coins[0].rank}";
                coinDayChangeLBL.Content = $"Change: {myDeserializedClass.data.coins[0].change}%";
                coinAMTOwnedLBL.Content = $"Amount Owned: 0.00";
                coinPNLBL.Content = $"Current P/L: +0.00";
                string ImageURL = myDeserializedClass.data.coins[0].iconUrl;
                ImageURL = ImageURL.Remove(ImageURL.Length - 3, 3);
                ImageURL = ImageURL + "png";
                coinImage.Source = new BitmapImage(new Uri(ImageURL));

                //Get Data for Coin 2 and Fill details in for card 2
                coinNameLBLTwo.Content = $"{myDeserializedClass.data.coins[1].name}";
                coinSymbolLBLTwo.Content = $"{myDeserializedClass.data.coins[1].symbol}";
                coinCurrentPriceLBLTwo.Content = $"Current Price: {myDeserializedClass.data.coins[1].price:c2}";
                coinAllTimeHighLBLTwo.Content = $"Market Cap: {myDeserializedClass.data.coins[1].marketCap}";
                coinRankLBLTwo.Content = $"Coin Rank: {myDeserializedClass.data.coins[1].rank}";
                coinDayChangeLBLTwo.Content = $"Change: {myDeserializedClass.data.coins[1].change}%";
                coinAMTOwnedLBLTwo.Content = $"Amount Owned: 0.00";
                coinPNLBLTwo.Content = $"Current P/L: +0.00";
                string ImageURLTwo = myDeserializedClass.data.coins[1].iconUrl;
                ImageURLTwo = ImageURLTwo.Remove(ImageURLTwo.Length - 3, 3);
                ImageURLTwo = ImageURLTwo + "png";
                coinImageTwo.Source = new BitmapImage(new Uri(ImageURLTwo));

                //Get Data for Coin 3 and Fill details in for card 3
                coinNameLBLThree.Content = $"{myDeserializedClass.data.coins[2].name}";
                coinSymbolLBLThree.Content = $"{myDeserializedClass.data.coins[2].symbol}";
                coinCurrentPriceLBLThree.Content = $"Current Price: {myDeserializedClass.data.coins[2].price:c2}";
                coinAllTimeHighLBLThree.Content = $"Market Cap: {myDeserializedClass.data.coins[2].marketCap}";
                coinRankLBLThree.Content = $"Coin Rank: {myDeserializedClass.data.coins[2].rank}";
                coinDayChangeLBLThree.Content = $"Change: {myDeserializedClass.data.coins[2].change}%";
                coinAMTOwnedLBLThree.Content = $"Amount Owned: 0.00";
                coinPNLBLThree.Content = $"Current P/L: +0.00";
                string ImageURLThree = myDeserializedClass.data.coins[2].iconUrl;
                ImageURLThree = ImageURLThree.Remove(ImageURLThree.Length - 3, 3);
                ImageURLThree = ImageURLThree + "png";
                coinImageThree.Source = new BitmapImage(new Uri(ImageURLThree));
            }
        }
    }
}
