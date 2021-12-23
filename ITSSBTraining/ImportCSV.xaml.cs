using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ImportCSV.xaml
    /// </summary>
    public partial class ImportCSV : Window
    {
        AmionicEntity db;
        public ImportCSV()
        {
            InitializeComponent();
            db=new AmionicEntity();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int errors=0;
            OpenFileDialog openFileDialog= new OpenFileDialog();
            bool result = (bool)openFileDialog.ShowDialog();
            if (result)
            {
                txtFileName.Text = openFileDialog.FileName;
                StreamReader reader=new StreamReader(openFileDialog.FileName);
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        if (line != null)
                        {
                            var values = line.Split(',');
                            if (values[0] == "ADD")
                            {
                                DateTime date = DateTime.Parse(values[1]);
                                TimeSpan timeSpan = TimeSpan.Parse(values[2]);
                                int flightNumber = Convert.ToInt32(values[3]);
                                int aircraftCode = Convert.ToInt32(values[6]);
                                decimal economyPrice = Convert.ToDecimal(values[7]);
                                bool confirmed = true;
                                string fromAirport = values[4];
                                string toAirport = values[5];
                                int routeId = db.Routes.Where(x => x.Airport.IATACode == fromAirport && x.Airport1.IATACode == toAirport).FirstOrDefault().ID;
                                Schedule schedule = new Schedule();
                                schedule.Date = date;
                                schedule.Time = timeSpan;
                                schedule.FlightNumber = flightNumber.ToString();
                                schedule.AircraftID = aircraftCode;
                                schedule.RouteID = routeId;
                                schedule.Confirmed = confirmed;
                                schedule.EconomyPrice = economyPrice;
                                db.Schedules.Add(schedule);
                                db.SaveChanges();
                              //  txtData.Text = txtData.Text.ToString() + routeId + "\n";
                            }
                        }
                    }catch(Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        errors++;
                        continue;
                    }
                    txtData.Text = "Failed Records: " + errors;
                }
                
            }
        }
    }
}
