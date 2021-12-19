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
using System.Windows.Threading;

namespace ITSSBTraining
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        AmionicEntity db;
        int count = 0;
        DispatcherTimer dispatchTimer;
        int time = 10;
        public LoginPage()
        {
            InitializeComponent();
            db= new AmionicEntity();
            dispatchTimer=new DispatcherTimer();
            dispatchTimer.Interval = new TimeSpan(0, 0, 1);
            dispatchTimer.Tick += new EventHandler(timeTick);
        }

        private void timeTick(object sender, EventArgs e)
        {
            txtTimer.Visibility = Visibility.Visible;
            txtTimer.Text = "You can login in " + time + " Seconds!";
            time -= 1;
            if (time == 0)
            {
                btnLogin.IsEnabled = true;
                txtTimer.Visibility = Visibility.Hidden;
                dispatchTimer.Stop();
                time = 10;
            }
            
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
                count = count + 1;
                MessageBoxResult msgBox = MessageBox.Show("Invalid Credentials", "Login Error");
            }
            if (count == 3)
            {
                btnLogin.IsEnabled = false;
                dispatchTimer.Start();
                count = 0;
            }
            
        }
    }
}
