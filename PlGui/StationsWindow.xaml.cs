using BlApi;
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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for StationsWindow.xaml
    /// </summary>
    public partial class StationsWindow : Window
    {
        private IEnumerable<BO.Station> boStationList;
        private IEnumerable<BO.AdjacentStations> boAdjStatList;
        IBL bl;
        public BO.Station selectedStation;
        public StationsWindow()
        {
            InitializeComponent();
            bl = BlFactory.GetBl("1");
            boStationList = bl.GetAllStations();
            lbStations.ItemsSource = boStationList;
            lbStations.SelectedIndex = 0;
            boAdjStatList = bl.GetAllAdjacentStations();
            lbAdjacentStations.ItemsSource = boAdjStatList;
            lbAdjacentStations.SelectedIndex = 0;
        }
        private void lbStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedStation = lbStations.SelectedValue as BO.Station;
            WindowStationDetails wsd = new WindowStationDetails(selectedStation);
            wsd.Show();
        }
    }
}
