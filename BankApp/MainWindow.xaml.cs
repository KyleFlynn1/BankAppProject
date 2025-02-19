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

            Card cardOne = new Card("Debit", DateTime.Now);

            Account.AddCard(cardOne);

            UpdateCardDetails();

            while (Account.CardCount > 1) { 
                btnAddCard.Visibility = Visibility.Hidden;
                btnCardRight.Visibility = Visibility.Visible;
            }
        }

        private void btnDashBoardWindow_Click(object sender, RoutedEventArgs e)
        {
            MainWindow DashboardWin = new MainWindow();

            DashboardWin.Owner = this;

            DashboardWin.ShowDialog();
        }

        private void btnAccountsWindow_Click(object sender, RoutedEventArgs e)
        {
            AccountsWindow AccountsWin = new AccountsWindow();

            AccountsWin.Owner = this;

            AccountsWin.ShowDialog();
        }

        private void btnTransactionsWindow_Click(object sender, RoutedEventArgs e)
        {
            TransactionsWindow TransactionsWin = new TransactionsWindow();

            TransactionsWin.Owner = this;

            TransactionsWin.ShowDialog();
        }

        private void btnWalletsWindow_Click(object sender, RoutedEventArgs e)
        {
            WalletsWindow WalletsWin = new WalletsWindow();

            WalletsWin.Owner = this;

            WalletsWin.ShowDialog();
        }

        private void btnDepositWindow_Click(object sender, RoutedEventArgs e)
        {
            DepositWindow DepositWin = new DepositWindow();

            DepositWin.Owner = this;

            DepositWin.ShowDialog();
        }

        private void btnWithdrawWindow_Click(object sender, RoutedEventArgs e)
        {
            WithdrawWindow WithdrawWin = new WithdrawWindow();

            WithdrawWin.Owner = this;

            WithdrawWin.ShowDialog();
        }

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

        private void btnAddCard_Click(object sender, RoutedEventArgs e)
        {
            Card newCard = new Card("Credit", DateTime.Now);
            Account.AddCard(newCard);
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
        }

        private void UpdateCardDetails()
        {
            TxtCardHolder.Content = Account.AccountHolder;
            TxtCardNumber.Content = Account.Cards[CurrentCard].CardNumberS1 + "-" + Account.Cards[CurrentCard].CardNumberS2 + "-" + Account.Cards[CurrentCard].CardNumberS3;
            TxtCVV.Content = Account.Cards[CurrentCard].CVV;
            TxtExpiryDate.Content = Account.Cards[CurrentCard].ExpiryDate.Month + "/" + Account.Cards[CurrentCard].ExpiryDate.Year;
            lblCardCount.Content = $"Card {CurrentCard + 1}/{Account.CardCount}";
            txtCardType.Content = Account.Cards[CurrentCard].CardType;
        }

        public BankAccount Account = new BankAccount("King Kyle");
        public int CurrentCard = 0;
    }
}
