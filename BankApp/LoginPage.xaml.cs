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
        UserData db = new UserData();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;

            var loginQuery = (from p in db.Users
                             where p.Password == passwordInput.Text && p.Username == usernameInput.Text
                             select p.UserID).FirstOrDefault();

            if (loginQuery != 0)
            {
                txtError.Text = "";
                LoginPage.UserID = loginQuery;
                mainWindow.FullFrame.Content = null;
            }
            else
            {
                txtError.Text = "Incorrect Details Entered!";
            }
        }
    }
}
