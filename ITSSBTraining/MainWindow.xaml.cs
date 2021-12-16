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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AmionicEntity db;
        public MainWindow()
        {
            InitializeComponent();
            db=new AmionicEntity();
            var officeList = db.Offices.ToList();
            cmbOffice.ItemsSource = officeList;
            cmbOffice.DisplayMemberPath = "Title";
            cmbOffice.SelectedValuePath = "ID";
            dtgGrid.ItemsSource = db.Users.ToList().Select(x=> new
            {
                Name=x.FirstName,
                Lastname=x.LastName,
                Email=x.Email,
                Age= (DateTime.Now.Month>x.Birthdate.Value.Month)?DateTime.Now.Year-x.Birthdate.Value.Year: DateTime.Now.Year - x.Birthdate.Value.Year+1,
                Role=x.Role.Title,
                Office=x.Office.Title
            });
        }

        private void cmbOffice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Office office = (Office) cmbOffice.SelectedItem;
            dtgGrid.ItemsSource= db.Users.ToList().Where(x=> x.OfficeID==office.ID).Select(x => new
            {
                Name = x.FirstName,
                Lastname = x.LastName,
                Email = x.Email,
                Age = (DateTime.Now.Month > x.Birthdate.Value.Month) ? DateTime.Now.Year - x.Birthdate.Value.Year : DateTime.Now.Year - x.Birthdate.Value.Year + 1,
                Role = x.Role.Title,
                Office = x.Office.Title
            });
        }
    }
}
