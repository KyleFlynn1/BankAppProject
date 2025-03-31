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
                RequestUri = new Uri("https://coinranking1.p.rapidapi.com/coin/Qwsogvtv82FCd?referenceCurrencyUuid=yhjMzLPhuIDl&timePeriod=24h"),
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

                coinNameLBL.Content = myDeserializedClass.data.coin.name;
            }

        }
    }
}
