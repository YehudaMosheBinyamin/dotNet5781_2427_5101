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
        public AddStationWindow(ObservableCollection<PO.Station> stationsCollection)
        {
            InitializeComponent();
            stationCollection = stationsCollection;
            IBL bl = BlFactory.GetBl("1");
            IEnumerable<PO.Line> linesList = from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line);
            cbLines.ItemsSource= linesList;
            cbLines.DisplayMemberPath = "Code";
            cbLines.SelectedIndex = 0;
        }
        /// <summary>
        /// Event for adding lineStation to list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLineStation(object sender, RoutedEventArgs e)
        { IBL bl = BlFactory.GetBl("1");
            PO.Line line = cbLines.SelectedItem as PO.Line;
            float distance = bl.GetRandomDistance();
            PO.LineStation poLineStation = new PO.LineStation()
            {
                LineStationIndex = line.stationsInLine.Count,
                NextStation = 00000,
                PrevStation = line.stationsInLine.ElementAt(line.stationsInLine.Count - 1).Station,
                DistanceFromPreviousStation = distance,
                TimeFromPreviousStation = bl.GetMinutesOfTravel(distance),
                InService = true,
                Station = int.Parse(tbCode.Text),
                LineId = line.Id,
                LastStationName = tbName.Text,
                Name = tbName.Text
            };
            lbLineStations.Items.Add(poLineStation);
        }

        private void AddStation(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            PO.Station poStation = new PO.Station();
            int code;
            float latitude;
            float longtitude;
            bool codeConverted = int.TryParse(tbCode.Text, out code);
            if (codeConverted == false)
            {
                MessageBox.Show("Fill the code field and try again");
                return;
            }
            bool latitudeConverted = float.TryParse(tbLatitude.Text, out latitude);
            if (latitudeConverted == false)
            {
                MessageBox.Show("Fill the latitude field and try again");
                return;
            }
            poStation.Code = code;
            bool longtitudeConverted = float.TryParse(tbLongtitude.Text, out longtitude);
            if (longtitudeConverted == false)
            {
                MessageBox.Show("Fill the longtitude field and try again");
                return;
            }
            poStation.Latitude = latitude;
            poStation.LineStationsOfStation = Utillities.Convert(lbLineStations.Items.Cast<PO.LineStation>().ToList());
            poStation.Longtitude = longtitude;
            poStation.Name = tbName.Text;
            bl.AddStation(Utillities.StationPoBoAdapter(poStation));
            stationCollection.Add(poStation);
            bl.GetStation(code);
        }
    }
    }

