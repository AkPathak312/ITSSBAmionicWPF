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

namespace ITSSBTraining
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        AmionicEntity db;
        public LoginPage()
        {
            InitializeComponent();
            db= new AmionicEntity();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user= db.Users.Where(x => x.Email == txtUserName.Text.ToString() && x.Password == txtPassword.Password.ToString()).FirstOrDefault();
            if (user != null)
            {
                if ((bool)user.Active)
                {
                    if (user.RoleID==1)
                    {
                        this.NavigationService.Navigate(new AdminPage());
                    }
                    else
                    {
                        NavigationService.Navigate(new UserPage());
                    }
                }
                else
                {
                    MessageBoxResult msgBox = MessageBox.Show("User is Disabled", "Login Error");
                }
               
            }
            else
            {
                MessageBoxResult msgBox = MessageBox.Show("Invalid Credentials", "Login Error");
            }
            
        }
    }
}
