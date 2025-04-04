﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for DashboardPage.xaml
    /// </summary>
    public partial class DashboardPage : Page
    {
        //Public and Static Variables
        public static BankAccount Account;
        static decimal TransferAmount;
        static string TransferBankNumber;
        public int CurrentCard = 0;
        UserData db = new UserData();
        public DashboardPage()
        {
            InitializeComponent();

            LoadData();

            transferCancelBTN.Visibility = Visibility.Hidden;
            transferConfirmBTN.Visibility = Visibility.Hidden;
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

            if (CurrentCard + 1 == Account.CardCount || CurrentCard + 1 == 5)
            {
                if (Account.CardCount >= 5)
                {
                    btnCardRight.Visibility = Visibility.Hidden;
                }
                else if (Account.AccountType == "Savings" && Account.CardCount <= 2)
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
            Card card = new Card("Credit", DateTime.Now) { BankAccount = Account };
            Account.AppendCard(card);
            db.Cards.Add(card);
            Account.CardCount++;
            db.SaveChanges();
            btnAddCard.Visibility = Visibility.Hidden;
            btnCardRight.Visibility = Visibility.Visible;
            if (lblCardCount.Visibility == Visibility.Hidden)
            {
                lblCardCount.Visibility = Visibility.Visible;
            }
            lblCardCount.Content = $"Card {CurrentCard + 1}/{Account.CardCount}";
            btnCardRight_Click(sender, e);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            btnCardLeft.Visibility = Visibility.Hidden;
            btnCardRight.Visibility = Visibility.Hidden;
            lblCardCount.Visibility = Visibility.Hidden;

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

        public void LoadData()
        {
            //Get UserID from the login page from the associated user logged in
            int UserID = LoginPage.UserID;

            //Query to get the bank account from the database that matches the useriD
            var bankAccountQuery = (from b in db.BankAccounts
                                    where b.UserID == UserID
                                    select b).FirstOrDefault();

            if (AccountsPage.CurrentBankID != bankAccountQuery.BankID && AccountsPage.CurrentBankID != 0)
            {
                var bankAccount = (from b in db.BankAccounts
                                   where b.BankID == AccountsPage.CurrentBankID
                                   select b).FirstOrDefault();
                if (bankAccount != null)
                {
                    Account = bankAccount;
                    AccountsPage.CurrentBankID = bankAccount.BankID;
                    Account.CardCount = bankAccount.Cards.Count();
                    UpdateCardDetails();
                    txtAccountBalance.Content = $"{Account.AccountBalance:c}";
                    if (Account.CardCount > 1)
                    {
                        btnCardLeft.Visibility = Visibility.Hidden;
                        btnAddCard.Visibility = Visibility.Hidden;
                        btnCardRight.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        btnCardLeft.Visibility= Visibility.Hidden;
                        btnAddCard.Visibility= Visibility.Visible;
                        btnCardRight.Visibility= Visibility.Hidden;
                    }
                }
                else
                {
                    txtAccountBalance.Content = UserID;
                }
            }
            else
            {
                if (bankAccountQuery != null)
                {
                    Account = bankAccountQuery;
                    AccountsPage.CurrentBankID = bankAccountQuery.BankID;
                    Account.CardCount = bankAccountQuery.Cards.Count();
                    UpdateCardDetails();
                    txtAccountBalance.Content = $"{Account.AccountBalance:c}";
                    if (Account.CardCount > 1)
                    {
                        btnCardLeft.Visibility = Visibility.Hidden;
                        btnAddCard.Visibility = Visibility.Hidden;
                        btnCardRight.Visibility = Visibility.Visible;
                        }
                    else
                    {
                        btnCardLeft.Visibility= Visibility.Hidden;
                        btnAddCard.Visibility= Visibility.Visible;
                        btnCardRight.Visibility= Visibility.Hidden;
                    }
                }
                else
                {
                    txtAccountBalance.Content = UserID;
                }
            }

        }

        //Transfer Tile Methods
        private void transferBTN_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(transferAmountInput.Text, out TransferAmount))
            {
                TransferBankNumber = transferBankIDInput.Text;
                if (TransferAmount <= 0.0m)
                {
                    transferMSGTxt.Foreground = new SolidColorBrush(Colors.Red);
                    transferMSGTxt.Content = "Invalid Amount entered!";
                }
                else if (TransferAmount > 0.0m)
                {
                    var bankAccountQuery = (from b in db.BankAccounts
                                            where b.AccountNumber == TransferBankNumber
                                            select b).FirstOrDefault();

                    if (bankAccountQuery != null)
                    {
                        transferConfirmBTN.Visibility = Visibility.Visible;
                        transferCancelBTN.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        transferMSGTxt.Foreground = new SolidColorBrush(Colors.Red);
                        transferMSGTxt.Content = "Invalid Bank Account Number  entered!";
                    }
                }
            }
            else
            {
                transferMSGTxt.Foreground = new SolidColorBrush(Colors.Red);
                transferMSGTxt.Content = "Invalid Amount entered!";
            }
            
        }

        private void transferCancelBTN_Click(object sender, RoutedEventArgs e)
        {
            transferConfirmBTN.Visibility= Visibility.Hidden;
            transferCancelBTN.Visibility = Visibility.Hidden;
            transferBTN.Visibility = Visibility.Visible;
        }

        private void transferConfirmBTN_Click(object sender, RoutedEventArgs e)
        {
            var bankAccountQuery = (from b in db.BankAccounts
                                    where b.AccountNumber == TransferBankNumber
                                    select b).FirstOrDefault();

            if (bankAccountQuery != null)
            {
                transferMSGTxt.Foreground = new SolidColorBrush(Colors.Green);
                transferMSGTxt.Content = $"Succesful Transfer of + {TransferAmount:c} to {bankAccountQuery.AccountHolder}";
                bankAccountQuery.Deposit(TransferAmount);
                Transaction t1 = new Transaction(TransferAmount, "Succesful Transfer In", Account.AccountNumber, DateTime.Now, 0.55m, "Deposit");
                bankAccountQuery.Transactions.Add(t1);
                Account.Withdraw(TransferAmount);
                Transaction t2 = new Transaction(TransferAmount, "Succesful Transfer Out", bankAccountQuery.AccountNumber, DateTime.Now, 0.55m, "Withdraw");
                Account.Transactions.Add(t2);
                db.Transactions.Add(t1);
                db.Transactions.Add(t2);
                db.SaveChanges();
                transferBankIDInput.Text = "";
                transferAmountInput.Text = "";
                TransferAmount = 0;
                TransferBankNumber = "";
                transferConfirmBTN.Visibility = Visibility.Hidden;
                transferCancelBTN.Visibility = Visibility.Hidden;
                transferBTN.Visibility = Visibility.Visible;
                LoadData();
            }
            else
            {
                transferMSGTxt.Foreground = new SolidColorBrush(Colors.Red);
                transferMSGTxt.Content = $"Transfer Failed not a valid Bank";
            }
        }
        //End of Transfer Tile Methods
    }
}
