using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddStationWindow.xaml
    /// </summary>
    public partial class AddStationWindow : Window
    { ObservableCollection<PO.Station> stationCollection;
        public bool Updated { get; set; }
        public AddStationWindow(ObservableCollection<PO.Station> stationsCollection)
        {
            InitializeComponent();
            stationCollection = stationsCollection;
            IBL bl = BlFactory.GetBl("1");
        }
        private void AddStation(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            PO.Station poStation = new PO.Station();
            float latitude;
            float longtitude;
            bool latitudeConverted = float.TryParse(tbLatitude.Text, out latitude);
            if (latitudeConverted == false)
            {
                MessageBox.Show("Fill the latitude field and try again");
                return;
            }
            bool longtitudeConverted = float.TryParse(tbLongtitude.Text, out longtitude);
            if (longtitudeConverted == false)
            {
                MessageBox.Show("Fill the longtitude field and try again");
                return;
            }
            poStation.Latitude = latitude;
            poStation.LineStationsOfStation =new ObservableCollection<PO.LineStation>();
            poStation.Longtitude = longtitude;
            poStation.Name = tbName.Text;
            poStation.Code = -1;
            stationCollection.Add(poStation);
            bl.AddStation(Utillities.StationPoBoAdapter(poStation));
            Updated = true;
            Close();
        }


        private void CancelStation(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
    }

