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

            BankAccount Account = new BankAccount("King Kyle");

            Card cardOne = new Card("Debit", DateTime.Now);

            Account.AddCard(cardOne);

            TxtCardHolder.Content = Account.AccountHolder;
            TxtCardNumber.Content = cardOne.CardNumberS1 + "-" + cardOne.CardNumberS2 + "-" + cardOne.CardNumberS3;
            TxtCVV.Content = cardOne.CVV;
            TxtExpiryDate.Content = cardOne.ExpiryDate.Month + "/" + cardOne.ExpiryDate.Year;
        }
    }
}
