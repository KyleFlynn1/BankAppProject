using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
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
    /// Interaction logic for TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage : Page
    {
        UserData db = new UserData();
        public TransactionsPage()
        {
            InitializeComponent();
            UpdateTransactionList();
            UpadateMonthlyStatTile();
        }

        public void UpdateTransactionList()
        {
            List<Transaction> Transactions = new List<Transaction>();
            Transactions = DashboardPage.Account.Transactions;
            TransactionGrid.ItemsSource = Transactions;
            var DateColumn = TransactionGrid.Columns.FirstOrDefault(c => c.Header.ToString() == "Date");
            if(DateColumn != null)
            {
                DateColumn.SortDirection = System.ComponentModel.ListSortDirection.Descending;
                TransactionGrid.Items.SortDescriptions.Clear();
                TransactionGrid.Items.SortDescriptions.Add(new SortDescription(DateColumn.SortMemberPath, ListSortDirection.Descending));
                TransactionGrid.Items.Refresh();
            }
        }

        public void UpadateMonthlyStatTile()
        {

            //Update This Month Statistics for In and Out Amounts
            var CurrentMonth = DateTime.Now.Month;
            var CurrentYear = DateTime.Now.Year;
            var thisMonthTransactions = (from t in db.Transactions
                                         where t.BankAccount.BankID == DashboardPage.Account.BankID && t.TransactionDate.Month == CurrentMonth && t.TransactionDate.Year == CurrentYear
                                         select t).ToList();
            //Get Total Amount In 
            decimal totalIn = thisMonthTransactions
                .Where(t => t.TransactionType == "Deposit" || t.TransactionType == "CryptoS")
                .Sum(t => t.TransactionAmount);

            //Get Total Amount Out
            decimal totalOut = thisMonthTransactions
                .Where(t => t.TransactionType == "Withdraw" || t.TransactionType == "CryptoB")
                .Sum(t => t.TransactionAmount);

            //Update Total In and Out Text Boxes
            totalInTXT.Text = $"{totalIn:c}";
            totalOutTxt.Text = $"{totalOut:c}";

            //Get Deposit List
            var Deposits = thisMonthTransactions
                .Where(t => t.TransactionType == "Deposit" || t.TransactionType == "CryptoS");

            //Get Withdrawals List
            var Withdrawals = thisMonthTransactions
                .Where(t => t.TransactionType == "Withdraw" || t.TransactionType == "CryptoB");

            //Get Average Amount In
            decimal averageDeposit = Deposits.Any() ? Deposits.Average(t => t.TransactionAmount) : 0;

            //Get Average Amount Out
            decimal averageWithdraw = Withdrawals.Any() ? Withdrawals.Average(t => t.TransactionAmount) : 0;

            //Update Average Deposit and Withdrawals Text Boxes
            averageInTxt.Text = $"{averageDeposit:c}";
            averageOutTxt.Text = $"{averageWithdraw:c}";

            //Get Largest Deposit
            decimal largestDeposit = Deposits.Any() ? Deposits.Max(t => t.TransactionAmount) : 0;

            //Get Largest Withdrawal
            decimal largestWithdraw = Withdrawals.Any() ? Withdrawals.Max(t => t.TransactionAmount) : 0;

            //Update Largest Deposit and Largest Withdrawal Text Boxes
            largestDepositTxt.Text = $"{largestDeposit:c}";
            largestWithdrawalTxt.Text = $"{largestWithdraw:c}";
        }
    }
}

