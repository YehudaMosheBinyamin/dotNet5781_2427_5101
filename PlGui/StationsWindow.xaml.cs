using BlApi;
using PO;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        //private IEnumerable<BO.Station> boStationList;
        //private IEnumerable<BO.AdjacentStations> boAdjStatList;
        public ObservableCollection<Station> poStationCollection;
        public ObservableCollection<AdjacentStations> adjStatCollection;
        IBL bl;
        //public BO.Station selectedStation;
        public PO.Station selectedStation;
        public StationsWindow()
        {
            InitializeComponent();
            bl = BlFactory.GetBl("1");
            //boStationList = bl.GetAllStations();
            poStationCollection = Utillities.Convert(from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station));
            //lbStations.ItemsSource = boStationList;
            lbStations.ItemsSource = poStationCollection;
            lbStations.SelectedIndex = 0;
            //boAdjStatList = bl.GetAllAdjacentStations();
            adjStatCollection = Utillities.Convert(from adjacentStation in bl.GetAllAdjacentStations() select Utillities.AdjacentStationsBoPoAdapter(adjacentStation));
            //lbAdjacentStations.ItemsSource = boAdjStatList;
            lbAdjacentStations.ItemsSource = adjStatCollection;
            lbAdjacentStations.SelectedIndex = 0;
        }
       
        private void lbStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //selectedStation = lbStations.SelectedValue as BO.Station;
            selectedStation = lbStations.SelectedValue as PO.Station;
            WindowStationDetails wsd = new WindowStationDetails(selectedStation);
            wsd.Show();
        }

        private void AddStationEvent(object sender, RoutedEventArgs e)
        {
            AddStationWindow addStationWindow = new AddStationWindow(poStationCollection);
            addStationWindow.ShowDialog();
        }
    }
}
