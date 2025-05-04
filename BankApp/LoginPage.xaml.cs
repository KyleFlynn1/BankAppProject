using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    /// 
    public partial class LoginPage : Page
    {
        public static int UserID;
        public bool CreatingAccount = false;
        UserData db = new UserData();

        public LoginPage()
        {
            InitializeComponent();
            ShowLoginUI();
        }

        /// <summary>
        /// When login clicked it checks if the user is already registered and if so it logs them in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            if (CreatingAccount == false)
            {
                var loginQuery = (from p in db.Users
                                  where p.Password == passwordInput.Text && p.Username == usernameInput.Text
                                  select p.UserID).FirstOrDefault();

                if (loginQuery != 0)
                {
                    txtError.Text = "";
                    LoginPage.UserID = loginQuery;
                    mainWindow.FullFrame.Content = null;
                    mainWindow.MainFrame.Navigate(new Uri("DashboardPage.xaml", UriKind.Relative));
                }
                else
                {
                    txtError.Text = "Incorrect Details Entered!";
                }
            }
            // If the user is creating an account, it checks if the information is complete and then creates a new account and updates UI to get relevant info
            else if ( CreatingAccount == true)
            {
                if(accountHolderInput.Text != "" && usernameInput.Text != "" && passwordInput.Text != "")
                {
                    //Make New Classes with information to add to database and be used
                    UserAccount a1 = new UserAccount() { Username = usernameInput.Text, Password = passwordInput.Text };
                    BankAccount b1 = new BankAccount(accountHolderInput.Text) { UserAccount = a1 };
                    Card c1 = new Card("Debit", DateTime.Now) { BankAccount = b1 };
                    b1.Cards.Add(c1);
                    b1.CardCount = 1;
                    a1.BankAccounts.Add(b1);
                    b1.AccountType = "Current";

                    //Add Data to DataBase and Save
                    db.Users.Add(a1);
                    db.BankAccounts.Add(b1);
                    db.Cards.Add(c1);
                    db.SaveChanges();

                    var loginQuery = (from p in db.Users
                                      where p.Password == passwordInput.Text && p.Username == usernameInput.Text
                                      select p.UserID).FirstOrDefault();

                    if (loginQuery != 0)
                    {
                        txtError.Text = "";
                        CreatingAccount = false;
                        LoginPage.UserID = loginQuery;
                        mainWindow.FullFrame.Content = null;
                        mainWindow.MainFrame.Navigate(new Uri("DashboardPage.xaml", UriKind.Relative));
                    }
                }
                else
                {
                    txtError.Text = "Information Not Complete";
                }
            }
        }

        /// <summary>
        /// When create account button is clicked it changes the UI to show the create account UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createAccountBTN_Click(object sender, RoutedEventArgs e)
        {
            ShowCreateAccountUI();
        }

        /// <summary>
        /// When login account button is clicked it changes the UI to show the login account UI
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginAccountBTN_Click(object sender, RoutedEventArgs e)
        {
            ShowLoginUI();
        }

        /// <summary>
        /// Show Login Ui
        /// </summary>
        public void ShowLoginUI()
        {
            txtAccountHolder.Visibility = Visibility.Hidden;
            accountHolderInput.Visibility = Visibility.Hidden;
            txtUserName.Text = "Username";
            txtPassword.Text = "Password";
            submitBTN.Content = "Login";
            createAccountBTN.Visibility = Visibility.Visible;
            loginAccountBTN.Visibility = Visibility.Hidden;
            CreatingAccount = false;
        }

        /// <summary>
        /// Show Create Account UI
        /// </summary>
        public void ShowCreateAccountUI()
        {
            txtAccountHolder.Visibility = Visibility.Visible;
            accountHolderInput.Visibility = Visibility.Visible;
            txtUserName.Text = "Create Username";
            txtPassword.Text = "Create Password";
            submitBTN.Content = "Create Account";
            loginAccountBTN.Visibility= Visibility.Visible;
            createAccountBTN.Visibility = Visibility.Hidden;
            CreatingAccount = true;
        }
    }
}
