using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
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
using static System.Net.Mime.MediaTypeNames;

namespace BankApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FullFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
            UserData db = new UserData();
            Account.AddCard("Debit");

            Transaction t1 = new Transaction(56.00m, "Test", "Food", DateTime.Now, 0.00m);
            Transaction t2 = new Transaction(656.00m, "Test", "Food", DateTime.Now, 5.00m);

            Account.Transactions.Add(t1);
            Account.Transactions.Add(t2);

            UpdateCardDetails();

            while (Account.CardCount > 1) {
                btnAddCard.Visibility = Visibility.Hidden;
                btnCardRight.Visibility = Visibility.Visible;
            }

            //var getAccountQuery = from a in db.Users
            //                      where a.UserID == LoginPage.UserID
            //                      select a.BankAccountID;

            //var getCards = from c in db.Cards
            //               where c.BankAccountID == getAccountQuery.First()
            //               select c;

            //var getBank = from b in db.BankAccounts
            //              where b.UserID == LoginPage.UserID
            //              select b;

            //Account = (BankAccount)getBank;
        }

        private void btnDashBoardWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = null;
        }

        private void btnAccountsWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("AccountsPage.xaml", UriKind.Relative));
        }

        private void btnTransactionsWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("TransactionsPage.xaml", UriKind.Relative));

        }

        private void btnWalletsWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("WalletsPage.xaml", UriKind.Relative));

        }

        private void btnDepositWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("DepositPage.xaml", UriKind.Relative));

        }

        private void btnWithdrawWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("WithdrawPage.xaml", UriKind.Relative));

        }

        /// <summary>
        /// Move back to the left card and update details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCardLeft_Click(object sender, RoutedEventArgs e)
        {
            btnCardRight.Visibility = Visibility.Visible;
            btnAddCard.Visibility = Visibility.Hidden;
            CurrentCard--;
            UpdateCardDetails();
            if (CurrentCard == 0)
            {
                btnCardLeft.Visibility = Visibility.Hidden;
            }
        }

        /// <summary>
        /// Move over to the next card on the right and update details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCardRight_Click(object sender, RoutedEventArgs e)
        {
            btnCardLeft.Visibility = Visibility.Visible;
            CurrentCard++;
            UpdateCardDetails();

            if (CurrentCard+1 == Account.CardCount)
            {
                if (Account.CardCount == 5)
                {
                    btnCardRight.Visibility = Visibility.Hidden;
                }
                else
                {
                    btnAddCard.Visibility = Visibility.Visible;
                    btnCardRight.Visibility = Visibility.Hidden;
                }
            }
            

        }

        /// <summary>
        /// Add a new card using the Add Card Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            Account.AddCard("Credit");
            btnAddCard.Visibility = Visibility.Hidden;
            btnCardRight.Visibility = Visibility.Visible;
            if(lblCardCount.Visibility == Visibility.Hidden)
            {
                lblCardCount.Visibility = Visibility.Visible;
            }
            lblCardCount.Content = $"Card {CurrentCard+1}/{Account.CardCount}";
            btnCardRight_Click(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            btnCardLeft.Visibility = Visibility.Hidden;
            btnCardRight.Visibility = Visibility.Hidden;
            lblCardCount.Visibility = Visibility.Hidden;
            txtAccountBalance.Content = $"{Account.AccountBalance:c}";
        }

        /// <summary>
        /// Update the labels and text for the Card Image to match the currently selected card
        /// </summary>
        private void UpdateCardDetails()
        {
            TxtCardHolder.Content = Account.AccountHolder;
            TxtCardNumber.Content = Account.Cards[CurrentCard].CardNumberS1 + "-" + Account.Cards[CurrentCard].CardNumberS2 + "-" + Account.Cards[CurrentCard].CardNumberS3;
            TxtCVV.Content = Account.Cards[CurrentCard].CVV;
            TxtExpiryDate.Content = Account.Cards[CurrentCard].ExpiryDate.Month + "/" + Account.Cards[CurrentCard].ExpiryDate.Year;
            lblCardCount.Content = $"Card {CurrentCard + 1}/{Account.CardCount}";
            txtCardType.Content = Account.Cards[CurrentCard].CardType;
            if (Account.Cards[CurrentCard].CardType == "Credit")
            {
                txtCreditLimit.Visibility = Visibility.Visible;
                txtCreditLimit.Content = $"Credit Limit : {Account.Cards[CurrentCard].CreditLimit:c}";
            }
            else
                txtCreditLimit.Visibility = Visibility.Hidden;


            switch (CurrentCard)
            {
                case 0:
                    cardImage.Source = new BitmapImage(new Uri(@"/BankCardBackground.png", UriKind.Relative));
                    break;
                case 1:
                    cardImage.Source = new BitmapImage(new Uri(@"/BankCardBackgroundTwo.png", UriKind.Relative));
                    break;
                case 2:
                    cardImage.Source = new BitmapImage(new Uri(@"/BankCardBackgroundThree.png", UriKind.Relative));
                    break;
                case 3:
                    cardImage.Source = new BitmapImage(new Uri(@"/BankCardBackgroundFour.png", UriKind.Relative));
                    break;
                case 4:
                    cardImage.Source = new BitmapImage(new Uri(@"/BankCardBackgroundFive.png", UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        //public BankAccount Account { get; set; }
        public static BankAccount Account = new BankAccount("King Kyle");
        public int CurrentCard = 0;
    }
}
