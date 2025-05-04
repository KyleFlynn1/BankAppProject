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
        UserData db = new UserData();
        public MainWindow()
        {
            InitializeComponent();
            FullFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
        }
        //All buttons on sidebar that navigate to different pages
        private void btnDashBoardWindow_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("DashboardPage.xaml", UriKind.Relative));
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
    }
}
