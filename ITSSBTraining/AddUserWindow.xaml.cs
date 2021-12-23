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
using System.Windows.Shapes;

namespace ITSSBTraining
{
    /// <summary>
    /// Interaction logic for AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        AmionicEntity db;
        public AddUserWindow()
        {
            InitializeComponent();
            db=new AmionicEntity();
            cmbOffice.ItemsSource = db.Offices.ToList();
            cmbOffice.DisplayMemberPath = "Title";
            cmbOffice.SelectedValuePath = "ID";
            cmbOffice.SelectedIndex = 0;
            dtPicker.SelectedDate = DateTime.Now;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtEmail.Text.ToString() == "")
            {
                txtEmail.BorderBrush = new SolidColorBrush(Colors.Red);
                return;
            }
            User user=new User();
            user.ID = db.Users.Count() + 1;
            user.RoleID = 2;
            user.Active = true;
            user.Email = txtEmail.Text.ToString();
            user.FirstName=txtFName.Text.ToString();
            user.LastName=txtLName.Text.ToString();
            user.OfficeID = (int?)cmbOffice.SelectedValue;
            user.Password = txtPassword.Password.ToString();
            user.Birthdate = dtPicker.SelectedDate;
            db.Users.Add(user);
            db.SaveChanges();
        }
    }
}
