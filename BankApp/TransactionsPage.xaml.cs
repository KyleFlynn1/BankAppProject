﻿using System;
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
    /// Interaction logic for TransactionsPage.xaml
    /// </summary>
    public partial class TransactionsPage : Page
    {
        static public bool IsListReversed = false;
        public TransactionsPage()
        {
            InitializeComponent();
            UpdateTransactionList();
        }

        public void UpdateTransactionList()
        {
            List<Transaction> ReversedTransactions = new List<Transaction>();
            ReversedTransactions = DashboardPage.Account.Transactions;
            if(IsListReversed == false)
            {
                ReversedTransactions.Reverse();
                IsListReversed = true;
            }
            lbxTransactions.ItemsSource = ReversedTransactions;
        }


    }
}

