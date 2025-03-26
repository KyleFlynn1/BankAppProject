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
    /// Interaction logic for AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : Page
    {
        UserData db = new UserData();
        static string AccountType;
        public static int CurrentBankID = 0;
        public AccountsPage()
        {
            //Run Methods on page load to ensure data is loaded correctly and UI Elements are hidden and shown where needed
            InitializeComponent();
            HideUI();
            SwitchButtonsShow();
        }

        private void newAccBTN1_Click(object sender, RoutedEventArgs e)
        {
            newAccBTN1.Visibility = Visibility.Hidden;
            savingSelection1.Visibility = Visibility.Visible;
            businessSelection1.Visibility = Visibility.Visible;
            cancelBTN1.Visibility = Visibility.Visible;
        }

        private void newAccBTN2_Click(object sender, RoutedEventArgs e)
        {
            var bankAccounts = db.BankAccounts
                .Where(b => b.UserID == LoginPage.UserID)
                .OrderBy(b => b.BankID) 
                .ToList();

            var secondBankAccount = bankAccounts.ElementAtOrDefault(1); 
            newAccBTN2.Visibility = Visibility.Hidden;
            if (secondBankAccount.AccountType == "Savings")
            {
                businessSelection2.Visibility = Visibility.Visible;
            }
            else
            {
                savingSelection2.Visibility = Visibility.Visible;
            }
            cancelBTN2.Visibility = Visibility.Visible;
        }

        private void savingSelection1_Checked(object sender, RoutedEventArgs e)
        {
            AccountType = "Savings";
            confirmBTN1.Visibility = Visibility.Visible;
        }

        private void businessSelection1_Checked(object sender, RoutedEventArgs e)
        {
            AccountType = "Business";
            confirmBTN1.Visibility = Visibility.Visible;
        }

        private void savingSelection2_Checked(object sender, RoutedEventArgs e)
        {
            AccountType = "Savings";
            confirmBTN2.Visibility = Visibility.Visible;
        }

        private void businessSelection2_Checked(object sender, RoutedEventArgs e)
        {
            AccountType = "Business";
            confirmBTN2.Visibility = Visibility.Visible;
        }

        private void cancelBTN1_Click(object sender, RoutedEventArgs e)
        {
            HideUI();
        }

        private void cancelBTN2_Click(object sender, RoutedEventArgs e)
        {
            HideUI();
        }

        private void confirmBTN1_Click(object sender, RoutedEventArgs e)
        {
            var userAccountQuery = (from b in db.Users
                                    where b.UserID == LoginPage.UserID
                                    select b).FirstOrDefault();
            if (AccountType == "Savings" || AccountType == "Business")
            {
                //Make the new bank and add it to the user account and tables in the Database
                BankAccount b1 = new BankAccount(DashboardPage.Account.AccountHolder);
                Card c1 = new Card("Debit", DateTime.Now) { BankAccount = b1 };
                b1.AccountType = AccountType;
                b1.Cards.Add(c1);
                userAccountQuery.BankAccounts.Add(b1);
                db.BankAccounts.Add(b1);
                db.Cards.Add(c1);
                db.SaveChanges();
                //Update the Grid to show it and hide old Ui
                HideUI();

            }
        }

        public void HideUI()
        {
            var bankAccountQueryCount = (from b in db.BankAccounts
                                    where b.UserID == LoginPage.UserID
                                    select b).Count();

            var bankAccounts = db.BankAccounts
                .Where(b => b.UserID == LoginPage.UserID)
                .OrderBy(b => b.BankID) 
                .ToList();

            var defaultBankAccount = bankAccounts.ElementAtOrDefault(0);
            var secondBankAccount = bankAccounts.ElementAtOrDefault(1); 
            var thirdBankAccount = bankAccounts.ElementAtOrDefault(2);


            confirmBTN3.Visibility = Visibility.Hidden;
            cancelBTN3.Visibility = Visibility.Hidden;
            switch (bankAccountQueryCount)
            {
                case 1:
                    //Hide Elements to Make new Account and Show Relevant Button
                    newAccBTN1.Visibility = Visibility.Visible;
                    savingSelection1.Visibility = Visibility.Hidden;
                    businessSelection1.Visibility = Visibility.Hidden;
                    cancelBTN1.Visibility = Visibility.Hidden;
                    confirmBTN1.Visibility = Visibility.Hidden;
                    newAccBTN2.Visibility = Visibility.Hidden;
                    savingSelection2.Visibility = Visibility.Hidden;
                    businessSelection2.Visibility = Visibility.Hidden;
                    cancelBTN2.Visibility = Visibility.Hidden;
                    confirmBTN2.Visibility = Visibility.Hidden;
                    account2Grid.Visibility = Visibility.Hidden;
                    account3Grid.Visibility = Visibility.Hidden;

                    SwitchButtonsShow();

                    //Display Default Account Details at the top box
                    accountHolderTXT1.Content = $"| Account Holder - {defaultBankAccount.AccountHolder}";
                    accountNumberTXT1.Content = $"| Account Number - {defaultBankAccount.AccountNumber}";
                    accountBalanceTXT1.Content = $"| Account Balance - {defaultBankAccount.AccountBalance:c}";
                    cardCount1.Content = $"| Cards - {defaultBankAccount.CardCount}";
                    accountTypeTXT1.Content = $"{defaultBankAccount.AccountType} Account";
                    break;
                case 2:
                    //Hide Elements
                    defaultAccountSwitchBTN.Visibility = Visibility.Hidden;
                    newAccBTN1.Visibility = Visibility.Hidden;
                    savingSelection1.Visibility = Visibility.Hidden;
                    businessSelection1.Visibility = Visibility.Hidden;
                    cancelBTN1.Visibility = Visibility.Hidden;
                    confirmBTN1.Visibility = Visibility.Hidden;
                    newAccBTN2.Visibility = Visibility.Hidden;
                    savingSelection2.Visibility = Visibility.Hidden;
                    businessSelection2.Visibility = Visibility.Hidden;
                    cancelBTN2.Visibility = Visibility.Hidden;
                    confirmBTN2.Visibility = Visibility.Hidden;
                    account2Grid.Visibility = Visibility.Visible;
                    account3Grid.Visibility = Visibility.Hidden;


                    //Show Next Account Elements and Switch Option
                    newAccBTN2.Visibility = Visibility.Visible;
                    SwitchButtonsShow();

                    //Update Account Details
                    accountHolderTXT1.Content = $"| Account Holder - {defaultBankAccount.AccountHolder}";
                    accountNumberTXT1.Content = $"| Account Number - {defaultBankAccount.AccountNumber}";
                    accountBalanceTXT1.Content = $"| Account Balance - {defaultBankAccount.AccountBalance:c}";
                    cardCount1.Content = $"| Cards - {defaultBankAccount.CardCount}";
                    accountTypeTXT1.Content = $"{defaultBankAccount.AccountType} Account";

                    accountHolderTXT2.Content = $"| Account Holder - {secondBankAccount.AccountHolder}";
                    accountNumberTXT2.Content = $"| Account Number - {secondBankAccount.AccountNumber}";
                    accountBalanceTXT2.Content = $"| Account Balance - {secondBankAccount.AccountBalance:c}";
                    cardCount2.Content = $"| Cards - {secondBankAccount.CardCount}";
                    accountTypeTXT2.Content = $"{secondBankAccount.AccountType} Account";
                    break;
                case 3:
                    //Hide Elements
                    defaultAccountSwitchBTN.Visibility = Visibility.Hidden;
                    newAccBTN1.Visibility = Visibility.Hidden;
                    savingSelection1.Visibility = Visibility.Hidden;
                    businessSelection1.Visibility = Visibility.Hidden;
                    cancelBTN1.Visibility = Visibility.Hidden;
                    confirmBTN1.Visibility = Visibility.Hidden;
                    newAccBTN2.Visibility = Visibility.Hidden;
                    savingSelection2.Visibility = Visibility.Hidden;
                    businessSelection2.Visibility = Visibility.Hidden;
                    cancelBTN2.Visibility = Visibility.Hidden;
                    confirmBTN2.Visibility = Visibility.Hidden;
                    account3Grid.Visibility = Visibility.Visible;

                    //Show Next Account Elements and Switch Option
                    SwitchButtonsShow();

                    //Update Account Details
                    accountHolderTXT1.Content = $"| Account Holder - {defaultBankAccount.AccountHolder}";
                    accountNumberTXT1.Content = $"| Account Number - {defaultBankAccount.AccountNumber}";
                    accountBalanceTXT1.Content = $"| Account Balance - {defaultBankAccount.AccountBalance:c}";
                    cardCount1.Content = $"| Cards - {defaultBankAccount.CardCount}";
                    accountTypeTXT1.Content = $"{defaultBankAccount.AccountType} Account";

                    accountHolderTXT2.Content = $"| Account Holder - {secondBankAccount.AccountHolder}";
                    accountNumberTXT2.Content = $"| Account Number - {secondBankAccount.AccountNumber}";
                    accountBalanceTXT2.Content = $"| Account Balance - {secondBankAccount.AccountBalance:c}";
                    cardCount2.Content = $"| Cards - {secondBankAccount.CardCount}";
                    accountTypeTXT2.Content = $"{secondBankAccount.AccountType} Account";

                    accountHolderTXT3.Content = $"| Account Holder - {thirdBankAccount.AccountHolder}";
                    accountNumberTXT3.Content = $"| Account Number - {thirdBankAccount.AccountNumber}";
                    accountBalanceTXT3.Content = $"| Account Balance - {thirdBankAccount.AccountBalance:c}";
                    cardCount3.Content = $"| Cards - {thirdBankAccount.CardCount}";
                    accountTypeTXT3.Content = $"{thirdBankAccount.AccountType} Account";
                    break;
                default:
                    break;
            }
        }

        private void defaultAccountSwitchBTN_Click(object sender, RoutedEventArgs e)
        {
            var bankAccounts = db.BankAccounts
                .Where(b => b.UserID == LoginPage.UserID)
                .OrderBy(b => b.BankID) 
                .ToList();

            var defaultBankAccount = bankAccounts.ElementAtOrDefault(0);
            DashboardPage.Account = defaultBankAccount;
            CurrentBankID = defaultBankAccount.BankID;
            //Show and Hide Switch Buttons
            SwitchButtonsShow();
        }

        private void secondAccountSwitchBTN_Click(object sender, RoutedEventArgs e)
        {
            var bankAccounts = db.BankAccounts
                .Where(b => b.UserID == LoginPage.UserID)
                .OrderBy(b => b.BankID) 
                .ToList();

            var secondBankAccount = bankAccounts.ElementAtOrDefault(1); 
            DashboardPage.Account = secondBankAccount;
            CurrentBankID = secondBankAccount.BankID;
            //Show and Hide Switch Buttons
            SwitchButtonsShow();
        }

        private void thirdAccountSwitchBTN_Click(object sender, RoutedEventArgs e)
        {
            var bankAccounts = db.BankAccounts
                .Where(b => b.UserID == LoginPage.UserID)
                .OrderBy(b => b.BankID)
                .ToList();

            var thirdBankAccount = bankAccounts.ElementAtOrDefault(2);
            DashboardPage.Account = thirdBankAccount;
            CurrentBankID = thirdBankAccount.BankID;
            //Show and Hide Switch Buttons
            SwitchButtonsShow();
        }

        public void SwitchButtonsShow()
        {
            var bankAccounts = db.BankAccounts
                .Where(b => b.UserID == LoginPage.UserID)
                .OrderBy(b => b.BankID)
                .ToList();

            var defaultBankAccount = bankAccounts.ElementAtOrDefault(0);
            var secondBankAccount = bankAccounts.ElementAtOrDefault(1);
            var thirdBankAccount = bankAccounts.ElementAtOrDefault(2);

            if (defaultBankAccount != null)
            {
                if (CurrentBankID != defaultBankAccount.BankID)
                {
                    defaultAccountSwitchBTN.Visibility = Visibility.Visible;
                    defaultBankSelectedTXT.Visibility = Visibility.Hidden;
                }
                else if (CurrentBankID == defaultBankAccount.BankID)
                {
                    defaultAccountSwitchBTN.Visibility = Visibility.Hidden;
                    defaultBankSelectedTXT.Visibility = Visibility.Visible;
                }
            }

            if (secondBankAccount != null && secondBankAccount.BankID != defaultBankAccount.BankID)
            {
                if (CurrentBankID != secondBankAccount.BankID)
                {
                    secondAccountSwitchBTN.Visibility = Visibility.Visible;
                    secondBankSelectedTXT.Visibility = Visibility.Hidden;
                }
                else if (CurrentBankID == secondBankAccount.BankID)
                {
                    secondAccountSwitchBTN.Visibility = Visibility.Hidden;
                    secondBankSelectedTXT.Visibility = Visibility.Visible;
                }
            }
            else
            {
                secondAccountSwitchBTN.Visibility = Visibility.Hidden;
                secondBankSelectedTXT.Visibility = Visibility.Hidden;
            }
            if (thirdBankAccount != null && thirdBankAccount.BankID != defaultBankAccount.BankID)
            {
                if (CurrentBankID != thirdBankAccount.BankID)
                {
                    thirdAccountSwitchBTN.Visibility = Visibility.Visible;
                    thirdBankSelectedTXT.Visibility = Visibility.Hidden;
                }
                else if (CurrentBankID == thirdBankAccount.BankID)
                {
                    thirdAccountSwitchBTN.Visibility = Visibility.Hidden;
                    thirdBankSelectedTXT.Visibility = Visibility.Visible;
                }
            }
            else
            {
                thirdAccountSwitchBTN.Visibility = Visibility.Hidden;
                thirdBankSelectedTXT.Visibility = Visibility.Hidden;
            }
        }

        private void confirmBTN3_Click(object sender, RoutedEventArgs e)
        {
            //Change Full Frame to LoginPage until a new or same account is logged into
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.FullFrame.Navigate(new Uri("LoginPage.xaml", UriKind.Relative));
            //Reset Current BankID to 0 so DashboardPage start normally preventing previous users Bank ID messing with LoadData()
            CurrentBankID = 0;
        }

        private void cancelBTN3_Click(object sender, RoutedEventArgs e)
        {
            //Change Buttons Visibilitys to only show relevant
            logoutBTN.Visibility = Visibility.Visible;
            confirmBTN3.Visibility = Visibility.Hidden;
            cancelBTN3.Visibility = Visibility.Hidden;
        }

        private void logoutBTN_Click(object sender, RoutedEventArgs e)
        {
            //Change Buttons Visibilitys to only show relevant
            confirmBTN3.Visibility = Visibility.Visible;
            cancelBTN3.Visibility = Visibility.Visible;
            logoutBTN.Visibility = Visibility.Hidden;
        }
    }
}
