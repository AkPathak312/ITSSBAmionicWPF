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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        AmionicEntity db;
        UserData user;
        public AdminPage()
        {
            InitializeComponent();
            

            db = new AmionicEntity();
            var officeList = db.Offices.ToList();
            cmbOffice.ItemsSource = officeList;
            cmbOffice.DisplayMemberPath = "Title";
            cmbOffice.SelectedValuePath = "ID";
            dtgGrid.ItemsSource = db.Users.ToList().Select(x => new UserData(
                x.FirstName,
                x.LastName,
                x.Email,
                (DateTime.Now.Month > x.Birthdate.Value.Month) ? DateTime.Now.Year - x.Birthdate.Value.Year : DateTime.Now.Year - x.Birthdate.Value.Year + 1,
                x.Role.Title,
                x.Office.Title
            ));
        }
        private void cmbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Office office = (Office)cmbOffice.SelectedItem;
            dtgGrid.ItemsSource = db.Users.ToList().Where(x=>x.OfficeID==office.ID).Select(x => new UserData(
               x.FirstName,
               x.LastName,
               x.Email,
               (DateTime.Now.Month > x.Birthdate.Value.Month) ? DateTime.Now.Year - x.Birthdate.Value.Year : DateTime.Now.Year - x.Birthdate.Value.Year + 1,
               x.Role.Title,
               x.Office.Title
           ));
        }

        private void dtgGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            UserData userData = (UserData)e.Row.DataContext;
            if (userData != null)
            {
                bool active = (bool)db.Users.Where(x => x.Email == userData.Email).FirstOrDefault().Active;
                if (!active)
                {
                    e.Row.Background = new SolidColorBrush(Colors.Red);
                }
            }
        }

        private void dtgGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            user = (UserData)dtgGrid.SelectedItem;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (user != null)
            {
                User dbUser=db.Users.Where(x=>x.Email==user.Email).FirstOrDefault();
                if ((bool)dbUser.Active)
                {
                    dbUser.Active = false;
                }
                else
                {
                    dbUser.Active = true;
                }
                db.SaveChanges();
                dtgGrid.ItemsSource = db.Users.ToList().Select(x => new UserData(
                      x.FirstName,
                      x.LastName,
                      x.Email,
                      (DateTime.Now.Month > x.Birthdate.Value.Month) ? DateTime.Now.Year - x.Birthdate.Value.Year : DateTime.Now.Year - x.Birthdate.Value.Year + 1,
                      x.Role.Title,
                      x.Office.Title
          ));
               
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow=new AddUserWindow();
            addUserWindow.Show();
        }
    }

    internal class UserData
    {
        public string Name { get; }
        public string Lastname { get; }
        public string Email { get; }
        public int Age { get; }
        public string Role { get; }
        public string Office { get; }

        public UserData(string name, string lastname, string email, int age, string role, string office)
        {
            Name = name;
            Lastname = lastname;
            Email = email;
            Age = age;
            Role = role;
            Office = office;
        }

        public override bool Equals(object obj)
        {
            return obj is UserData other &&
                   Name == other.Name &&
                   Lastname == other.Lastname &&
                   Email == other.Email &&
                   Age == other.Age &&
                   Role == other.Role &&
                   Office == other.Office;
        }

        public override int GetHashCode()
        {
            int hashCode = 216928037;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Lastname);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Email);
            hashCode = hashCode * -1521134295 + Age.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Role);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Office);
            return hashCode;
        }
    }
}
