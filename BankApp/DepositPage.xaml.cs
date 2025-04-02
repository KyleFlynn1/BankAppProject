using System;
using System.Collections.Generic;
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
    /// Interaction logic for DepositPage.xaml
    /// </summary>
    public partial class DepositPage : Page
    {
        UserData db = new UserData();
        static decimal DepositAmount = 0.0m;
        static decimal QuickDepositAmount;
        static decimal InputtedAmount;
        static string DepositDescription;
        static string MethodName;
        static bool quickAddType = false;
        public DepositPage()
        {
            InitializeComponent();
            UpdateBalanceText();
            ChangeQuickAddType();
        }

        private void quickAddBTN10_Click(object sender, RoutedEventArgs e)
        {
            QuickAdd(10.00m);
            UpdateDepositAmountBox();
        }

        private void quickAddBTN25_Click(object sender, RoutedEventArgs e)
        {
            QuickAdd(25.00m); 
            UpdateDepositAmountBox();
        }

        private void quickAddBTN50_Click(object sender, RoutedEventArgs e)
        {
            QuickAdd(50.00m);
            UpdateDepositAmountBox();
        }

        private void quickAddBTN100_Click(object sender, RoutedEventArgs e)
        {
            QuickAdd(100.0m);
            UpdateDepositAmountBox();
        }

        private void quickAddBTN250_Click(object sender, RoutedEventArgs e)
        {
            QuickAdd(250.00m);
            UpdateDepositAmountBox();
        }

        private void depositBTN_Click(object sender, RoutedEventArgs e)
        {
            UpdateDepositAmountBox();
            if (DepositAmount <= 0.0m || MethodName == "")
            {
                txtError.Foreground = new SolidColorBrush(Colors.Red);
                txtError.Text = "Invalid Amount entered!";
            }
            else if ( DepositAmount > 0.0m && MethodName != "")
            {
                var bankAccountQuery = (from b in db.BankAccounts
                                        where b.BankID == AccountsPage.CurrentBankID
                                        select b).FirstOrDefault();

                GetDescription();

                if (bankAccountQuery != null)
                {
                    txtError.Foreground = new SolidColorBrush(Colors.Green);
                    txtError.Text = $"Succesful Deposit of + {DepositAmount:c}";
                    bankAccountQuery.Deposit(DepositAmount);
                    Transaction t1 = new Transaction(DepositAmount, DepositDescription, MethodName, DateTime.Now, 0.55m, "Deposit");
                    bankAccountQuery.Transactions.Add(t1);
                    db.Transactions.Add(t1);
                    db.SaveChanges();
                    DashboardPage.Account = bankAccountQuery;
                    UpdateBalanceText();
                    depositAmountInput.Text = "";
                }
            }
        }

        public void UpdateDepositAmountBox()
        {
            if (decimal.TryParse(depositAmountInput.Text, out InputtedAmount))
            {
                DepositAmount = InputtedAmount + QuickDepositAmount;
                QuickDepositAmount = 0.0m;
                depositAmountInput.Text = DepositAmount.ToString();
            }
            else if (depositAmountInput.Text == "" || depositAmountInput.Text == "Amount")
            {
                DepositAmount = QuickDepositAmount;
                QuickDepositAmount = 0.0m;
                depositAmountInput.Text = DepositAmount.ToString();
            }
            

        }

        public void UpdateBalanceText()
        {
            txtAccountBalance.Content = $"{DashboardPage.Account.AccountBalance:c}";
        }

        public void GetDescription()
        {
            if (depositNotesInput.Text == "" || depositNotesInput.Text == "Deposit Notes/Description")
            {
                DepositDescription = $"Succesful Deposit from {MethodName}";
            }
            else
            {
                DepositDescription = depositNotesInput.Text;
            }
        }

        private void methodPaypalBTN_Checked(object sender, RoutedEventArgs e)
        {
            MethodName = "Paypal";
        }

        private void methodRevolutBTN_Checked(object sender, RoutedEventArgs e)
        {
            MethodName = "Revolut";
        }

        private void methodStripeBTN_Checked(object sender, RoutedEventArgs e)
        {
            MethodName = "Stripe";
        }

        private void methodHyperWalletBTN_Checked(object sender, RoutedEventArgs e)
        {
            MethodName = "HyperWallet";
        }

        private void methodBankTransferBTN_Checked(object sender, RoutedEventArgs e)
        {
            MethodName = "Bank Transfer";
        }

        private void methodChequeBTN_Checked(object sender, RoutedEventArgs e)
        {
            MethodName = "Cheque";
        }

        private void quickAddTypeBTN_Click(object sender, RoutedEventArgs e)
        {
            if (quickAddType == true)
            {
                quickAddType = false;
            }
            else if (quickAddType == false)
            {
                quickAddType = true;
            }
            ChangeQuickAddType();
        }

        public void ChangeQuickAddType()
        {

            if (quickAddType == true)
            {
                quickAddTypeBTN.Content = "+";
                quickAddBTN10.Content = "-10";
                quickAddBTN25.Content = "-25";
                quickAddBTN50.Content = "-50";
                quickAddBTN100.Content = "-100";
                quickAddBTN250.Content = "-250";
            }
            else if (quickAddType == false)
            {
                quickAddTypeBTN.Content = "-";
                quickAddBTN10.Content = "+10";
                quickAddBTN25.Content = "+25";
                quickAddBTN50.Content = "+50";
                quickAddBTN100.Content = "+100";
                quickAddBTN250.Content = "+250";
            }
        }

        public void QuickAdd(decimal amount)
        {
            if (quickAddType == true)
            {
                if (DepositAmount >= amount)
                {
                    QuickDepositAmount -= amount;
                }
                else
                {
                    QuickDepositAmount -= DepositAmount;
                }
            }
            else if (quickAddType == false)
            {
                QuickDepositAmount += amount;
            }
        }
    }
}
