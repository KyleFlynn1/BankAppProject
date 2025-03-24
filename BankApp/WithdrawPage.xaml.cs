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
    /// Interaction logic for WithdrawPage.xaml
    /// </summary>
    public partial class WithdrawPage : Page
    {
        UserData db = new UserData();
        static decimal WithdrawAmount = 0.0m;
        static decimal QuickWithdrawAmount;
        static decimal InputtedAmount;
        static string WithdrawDescription;
        static string MethodName;
        static bool quickAddType = false;
        public WithdrawPage()
        {
            InitializeComponent();
            UpdateBalanceText();
            ChangeQuickAddType();
        }

        private void quickAddBTN10_Click(object sender, RoutedEventArgs e)
        {
            if (quickAddType == true)
            {
                if (WithdrawAmount >= 10.0m)
                {
                    QuickWithdrawAmount -= 10.0m;
                }
                else
                {
                    QuickWithdrawAmount -= WithdrawAmount;
                }
            }
            else if (quickAddType == false)
            {
                QuickWithdrawAmount += 10.0m;
            }
            UpdateWithdrawtAmountBox();
        }

        private void quickAddBTN25_Click(object sender, RoutedEventArgs e)
        {
            if (quickAddType == true)
            {
                if (WithdrawAmount >= 25.0m)
                {
                    QuickWithdrawAmount -= 25.0m;
                }
                else
                {
                    QuickWithdrawAmount -= WithdrawAmount;
                }
            }
            else if (quickAddType == false)
            {
                QuickWithdrawAmount += 25.0m;
            }
            UpdateWithdrawtAmountBox();
        }

        private void quickAddBTN50_Click(object sender, RoutedEventArgs e)
        {
            if (quickAddType == true)
            {
                if (WithdrawAmount >= 50.0m)
                {
                    QuickWithdrawAmount -= 50.0m;
                }
                else
                {
                    QuickWithdrawAmount -= WithdrawAmount;
                }
            }
            else if (quickAddType == false)
            {
                QuickWithdrawAmount += 50.0m;
            }
            UpdateWithdrawtAmountBox();
        }

        private void quickAddBTN100_Click(object sender, RoutedEventArgs e)
        {
            if (quickAddType == true)
            {
                if (WithdrawAmount >= 100.0m)
                {
                    QuickWithdrawAmount -= 100.0m;
                }
                else
                {
                    QuickWithdrawAmount -= WithdrawAmount;
                }
            }
            else if (quickAddType == false)
            {
                QuickWithdrawAmount += 100.0m;
            }
            UpdateWithdrawtAmountBox();
        }

        private void quickAddBTN250_Click(object sender, RoutedEventArgs e)
        {
            if (quickAddType == true)
            {
                if (QuickWithdrawAmount >= 250.0m)
                {
                    QuickWithdrawAmount -= 250.0m;
                }
                else
                {
                    QuickWithdrawAmount -= WithdrawAmount;
                }
            }
            else if (quickAddType == false)
            {
                QuickWithdrawAmount += 250.0m;
            }
            UpdateWithdrawtAmountBox();
        }
        private void withdrawBTN_Click(object sender, RoutedEventArgs e)
        {
            UpdateWithdrawtAmountBox();
            if (WithdrawAmount <= 0.0m || MethodName == "")
            {
                txtError.Foreground = new SolidColorBrush(Colors.Red);
                txtError.Text = "Invalid Amount entered!";
            }
            else if (WithdrawAmount > 0.0m && MethodName != "")
            {
                var bankAccountQuery = (from b in db.BankAccounts
                                        where b.UserID == LoginPage.UserID
                                        select b).FirstOrDefault();

                GetDescription();

                if (bankAccountQuery != null)
                {
                    if (bankAccountQuery.AccountBalance >= WithdrawAmount)
                    {
                        txtError.Foreground = new SolidColorBrush(Colors.Green);
                        txtError.Text = $"Succesful Withdrawal of - {WithdrawAmount:c}";
                        bankAccountQuery.Withdraw(WithdrawAmount);
                        Transaction t1 = new Transaction(WithdrawAmount, WithdrawDescription, MethodName, DateTime.Now, 0.55m, "Withdraw");
                        bankAccountQuery.Transactions.Add(t1);
                        db.Transactions.Add(t1);
                        db.SaveChanges();
                        DashboardPage.Account = bankAccountQuery;
                        UpdateBalanceText();
                        withdrawAmountInput.Text = "";
                    }
                    else
                    {
                        txtError.Foreground = new SolidColorBrush(Colors.Red);
                        txtError.Text = "Not Enough Funds!";
                    }
                }
            }
        }

        public void UpdateWithdrawtAmountBox()
        {
            if (decimal.TryParse(withdrawAmountInput.Text, out InputtedAmount))
            {
                WithdrawAmount = InputtedAmount + QuickWithdrawAmount;
                QuickWithdrawAmount = 0.0m;
                withdrawAmountInput.Text = WithdrawAmount.ToString();
            }
            else if (withdrawAmountInput.Text == "" || withdrawAmountInput.Text == "Amount")
            {
                WithdrawAmount = QuickWithdrawAmount;
                QuickWithdrawAmount = 0.0m;
                withdrawAmountInput.Text = WithdrawAmount.ToString();
            }


        }

        public void UpdateBalanceText()
        {
            txtAccountBalance.Content = $"{DashboardPage.Account.AccountBalance:c}";
        }

        public void GetDescription()
        {
            if (withdrawNotesInput.Text == "" || withdrawNotesInput.Text == "Deposit Notes/Description")
            {
                WithdrawDescription = $"Succesful Withdrawal from {MethodName}";
            }
            else
            {
                WithdrawDescription = withdrawNotesInput.Text;
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
    }
}
