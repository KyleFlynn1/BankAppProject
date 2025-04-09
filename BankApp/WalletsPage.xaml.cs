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
using System.Windows.Interop;
using System.Reflection;
using System.Threading;
using System.Windows.Threading;
using System.Security.Principal;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for WalletsPage.xaml
    /// </summary>
    public partial class WalletsPage : Page
    {
        UserData db = new UserData();
        static Wallet wallet = new Wallet();
        static Root myDeserializedClass = new Root();
        static int selectedCoinNum = 0;
        static OwnedCoin currentCoin = new OwnedCoin();
        DispatcherTimer timer = new DispatcherTimer();
        public WalletsPage()
        { 
            InitializeComponent();
            GetAPIData();
            //Timer to Keep API constantly Refreshing for proper data and also showing up to date P/L
            timer.Interval = TimeSpan.FromSeconds(5);
            timer.Tick += Timer_Tick;
            timer.Start();

            MainGrid.Visibility = Visibility.Hidden;
            CreateWalletGrid.Visibility = Visibility.Hidden;
            selectedCoinGrid.Visibility = Visibility.Hidden;

            var walletQuery = (from w in db.Wallets
                                    where w.UserID == LoginPage.UserID
                                    select w).FirstOrDefault();

            if (walletQuery != null)
            {
                MainGrid.Visibility = Visibility.Visible;
                walletAddressLBL.Content = $"Wallet Address : {walletQuery.WalletAddress}";
                wallet = walletQuery;
            }
            else
            {
                CreateWalletGrid.Visibility = Visibility.Visible;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            //Update the API data every 15 seconds
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
                myDeserializedClass = JsonConvert.DeserializeObject<Root>(body);

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
                var hasCoins = (from c in db.OwnedCoins
                                    where c.WalletID == wallet.WalletID
                                    select c).FirstOrDefault();
                if (hasCoins != null && wallet.WalletID != 0)
                {
                    UpdateCards();
                }

            }
        }

        private void confirmBTN_Click(object sender, RoutedEventArgs e)
        {
            Wallet w1 = new Wallet(LoginPage.UserID);
            OwnedCoin c1 = new OwnedCoin(myDeserializedClass.data.coins[0].uuid);
            OwnedCoin c2 = new OwnedCoin(myDeserializedClass.data.coins[1].uuid);
            OwnedCoin c3 = new OwnedCoin(myDeserializedClass.data.coins[2].uuid);
            w1.UserAccount = db.Users.Find(LoginPage.UserID);
            db.Wallets.Add(w1);
            db.SaveChanges();
            c1.WalletID = w1.WalletID;
            c2.WalletID = w1.WalletID;
            c3.WalletID = w1.WalletID;
            db.OwnedCoins.Add(c1);
            db.OwnedCoins.Add(c2);
            db.OwnedCoins.Add(c3);
            db.SaveChanges();
            wallet = w1;
            MainGrid.Visibility = Visibility.Visible;
            CreateWalletGrid.Visibility = Visibility.Hidden;
        }

        private void selectOne_Click(object sender, RoutedEventArgs e)
        {
            selectOne.Content = "Selected";
            selectTwo.Content = "Select";
            selectThree.Content = "Select"; 
            selectedCoinGrid.Visibility = Visibility.Visible;
            var coinUuid = myDeserializedClass.data.coins[0].uuid;
            var selectedCoin = (from c in db.OwnedCoins
                                where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuid
                                select c).FirstOrDefault();

            if (selectedCoin != null)
            {
                currentCoin = selectedCoin;
                selectedCoinNum = 0;
                UpdateCoinGrid();
            }
        }

        private void selectTwo_Click(object sender, RoutedEventArgs e)
        {
            selectOne.Content = "Select";
            selectTwo.Content = "Selected";
            selectThree.Content = "Select";
            selectedCoinGrid.Visibility = Visibility.Visible;
            var coinUuid = myDeserializedClass.data.coins[1].uuid;
            var selectedCoin = (from c in db.OwnedCoins
                                where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuid
                                select c).FirstOrDefault();

            if (selectedCoin != null)
            {
                currentCoin = selectedCoin;
                selectedCoinNum = 1;
                UpdateCoinGrid();
            }
        }

        private void selectThree_Click(object sender, RoutedEventArgs e)
        {
            selectOne.Content = "Select";
            selectTwo.Content = "Select";
            selectThree.Content = "Selected";
            selectedCoinGrid.Visibility = Visibility.Visible;
            var coinUuid = myDeserializedClass.data.coins[2].uuid;
            var selectedCoin = (from c in db.OwnedCoins
                                where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuid
                                select c).FirstOrDefault();

            if (selectedCoin != null)
            {
                currentCoin = selectedCoin;
                selectedCoinNum = 2;
                UpdateCoinGrid();
            }
        }

        private void purchaseBTN_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(amountToBuyINPUT.Text, out decimal amountToBuy))
            {
                var bankAccountQuery = (from b in db.BankAccounts
                                        where b.BankID == AccountsPage.CurrentBankID
                                        select b).FirstOrDefault();
                if (bankAccountQuery != null)
                {
                    decimal priceToBuy = amountToBuy * myDeserializedClass.data.coins[selectedCoinNum].price;
                    if (bankAccountQuery.AccountBalance >= priceToBuy)
                    {


                        var coinUuid = myDeserializedClass.data.coins[selectedCoinNum].uuid;
                        OwnedCoin ownedCoin = (from c in db.OwnedCoins
                                               where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuid
                                               select c).FirstOrDefault();
                        if (ownedCoin != null)
                        {
                            bankAccountQuery.Withdraw(priceToBuy);
                            Transaction t1 = new Transaction(priceToBuy, myDeserializedClass.data.coins[selectedCoinNum].symbol, "CryptoB", DateTime.Now, 0.55m, "Crypto");
                            bankAccountQuery.Transactions.Add(t1);
                            db.Transactions.Add(t1);
                            ownedCoin.AvgPricePerCoin = ((ownedCoin.OwnedAmount * ownedCoin.AvgPricePerCoin) + (amountToBuy * myDeserializedClass.data.coins[selectedCoinNum].price)) / (ownedCoin.OwnedAmount + amountToBuy); ownedCoin.OwnedAmount += amountToBuy;
                            errorTXT.Text = $"You have purchased {amountToBuy}";
                            UpdateCoinGrid();
                            UpdateCards();
                            db.SaveChanges();
                            purchaseCostTXT.Text = "";
                        }
                    }
                    else
                    {
                        errorTXT.Text = $"Not Enough Funds in your Bank";
                    }
                }
            }
        }

        private void amountToBuyINPUT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(amountToBuyINPUT.Text, out decimal amountToBuy))
            {
                decimal priceToBuy = amountToBuy * myDeserializedClass.data.coins[selectedCoinNum].price;
                purchaseCostTXT.Text = $"{priceToBuy:C2}";
            }
        }

        private void UpdateCoinGrid()
        {
            currentCoin.CurrentValue = myDeserializedClass.data.coins[selectedCoinNum].price * currentCoin.OwnedAmount;
            amountOwnedTXT.Text = $"{currentCoin.OwnedAmount}";
            avgPriceTXT.Text = $"{currentCoin.AvgPricePerCoin:C2}";
            totalValueTXT.Text = $"{currentCoin.CurrentValue:C2}";
            initialInvestmentTXT.Text = $"{currentCoin.OwnedAmount * currentCoin.AvgPricePerCoin:C2}";
            if (currentCoin.OwnedAmount > 0)
            {
                decimal percentChange = ((myDeserializedClass.data.coins[selectedCoinNum].price - currentCoin.AvgPricePerCoin) / currentCoin.AvgPricePerCoin) * 100;
                
                changeTXT.Text = $"{percentChange:F2}";
            }
            else { changeTXT.Text = $"0"; }
        }

        private void UpdateCards()
        {
            var coinUuidOne = myDeserializedClass.data.coins[0].uuid;
            var selectedCoinOne = (from c in db.OwnedCoins
                                where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuidOne
                                select c).FirstOrDefault();       

            var coinUuidTwo = myDeserializedClass.data.coins[1].uuid;
            var selectedCoinTwo = (from c in db.OwnedCoins
                                where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuidTwo
                                   select c).FirstOrDefault(); 
            
            var coinUuidThree = myDeserializedClass.data.coins[2].uuid;
            var selectedCoinThree = (from c in db.OwnedCoins
                                where c.WalletID == wallet.WalletID && c.CoinUUID == coinUuidThree
                                     select c).FirstOrDefault();

            coinAMTOwnedLBL.Content = $"Amount Owned: {selectedCoinOne.OwnedAmount}";
            coinAMTOwnedLBLTwo.Content = $"Amount Owned: {selectedCoinTwo.OwnedAmount}";
            coinAMTOwnedLBLThree.Content = $"Amount Owned: {selectedCoinThree.OwnedAmount}";
            coinPNLBL.Content = $"Current P/L:{(selectedCoinOne.OwnedAmount * myDeserializedClass.data.coins[0].price) - (selectedCoinOne.OwnedAmount * selectedCoinOne.AvgPricePerCoin):C2}";
            coinPNLBLTwo.Content = $"Current P/L: {(selectedCoinTwo.OwnedAmount * myDeserializedClass.data.coins[1].price) - (selectedCoinTwo.OwnedAmount * selectedCoinTwo.AvgPricePerCoin):C2}";
            coinPNLBLThree.Content = $"Current P/L: {(selectedCoinThree.OwnedAmount * myDeserializedClass.data.coins[2].price) - (selectedCoinThree.OwnedAmount * selectedCoinThree.AvgPricePerCoin):C2}";

            //Details to show for total stats
            //Total Invested Caluclation and Text
            decimal totalInvestment = (selectedCoinOne.OwnedAmount * selectedCoinOne.AvgPricePerCoin) + (selectedCoinTwo.OwnedAmount * selectedCoinTwo.AvgPricePerCoin) + (selectedCoinThree.OwnedAmount * selectedCoinThree.AvgPricePerCoin);
            totalInvestmentTXT.Text = $"{totalInvestment:C2}";

            //Total Change percent Calculation and Text
            int coinsChangeCount = 0;
            decimal coinOneChange =0, coinTwoChange = 0, coinThreeChange = 0;
            if(selectedCoinOne.OwnedAmount > 0)
            {
                coinOneChange = ((myDeserializedClass.data.coins[0].price - selectedCoinOne.AvgPricePerCoin) / selectedCoinOne.AvgPricePerCoin) * 100;
                coinsChangeCount ++;
            }
            if(selectedCoinTwo.OwnedAmount > 0)
            {
                coinTwoChange = ((myDeserializedClass.data.coins[1].price - selectedCoinTwo.AvgPricePerCoin) / selectedCoinTwo.AvgPricePerCoin) * 100;
                coinsChangeCount++;
            }
            if (selectedCoinThree.OwnedAmount > 0)
            {
                coinThreeChange = ((myDeserializedClass.data.coins[2].price - selectedCoinThree.AvgPricePerCoin) / selectedCoinThree.AvgPricePerCoin) * 100;
                coinsChangeCount++;
            }
            if(coinsChangeCount > 0)
            {
                totalChangeTXT.Text = $"{(coinOneChange + coinTwoChange + coinThreeChange) / coinsChangeCount:F2}%";
            }

            //Total Value Calculation and Text
            totalValueAllTXT.Text = $"{selectedCoinOne.CurrentValue + selectedCoinTwo.CurrentValue + selectedCoinThree.CurrentValue:c2}";
        }
    }
}
