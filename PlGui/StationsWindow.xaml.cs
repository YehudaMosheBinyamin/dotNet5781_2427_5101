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
        public ObservableCollection<PO.Line> allLines;
        public ObservableCollection<Station> poStationCollection;
        public ObservableCollection<AdjacentStations> adjStatCollection;
        IBL bl;
        public PO.Station selectedStation;
        public StationsWindow(ObservableCollection<PO.Line> lineCollection)
        {
            InitializeComponent();
            bl = BlFactory.GetBl("1");
            poStationCollection = Utillities.Convert(from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station));
            lbStations.ItemsSource = poStationCollection;
            lbStations.SelectedIndex = 0;
            adjStatCollection = Utillities.Convert(from adjacentStation in bl.GetAllAdjacentStations() select Utillities.AdjacentStationsBoPoAdapter(adjacentStation));
            lbAdjacent.ItemsSource = adjStatCollection;
            lbAdjacent.SelectedIndex = 0;
            allLines = lineCollection;
        }
        /// <summary>
        /// To change time and distance between adjacent stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeAndDistanceFromPrevChangedEvent(object sender,EventArgs e)
        {
            int index = lbAdjacent.SelectedIndex;
            PO.AdjacentStations poAdjStat = lbAdjacent.SelectedValue as PO.AdjacentStations;
            UpdateTimeDistance updateTimeDistance = new UpdateTimeDistance(poAdjStat);
            updateTimeDistance.ShowDialog();
            if (updateTimeDistance.Updated == true)
            {
                allLines.Clear();
                ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
                temp = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
                foreach (PO.Line line in temp)
                {
                    allLines.Add(line);
                }
                poStationCollection = Utillities.Convert(from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station));
                lbStations.ItemsSource = poStationCollection;
                lbStations.SelectedIndex = 0;
                adjStatCollection = Utillities.Convert(from adjacentStation in bl.GetAllAdjacentStations() select Utillities.AdjacentStationsBoPoAdapter(adjacentStation));
                lbAdjacent.ItemsSource = adjStatCollection;
                lbAdjacent.SelectedIndex = 0;
                MessageBox.Show("Time and distance between adjacent stations changed successfully");
            }
        }
        /// <summary>
        /// For more details about station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedStation = lbStations.SelectedValue as PO.Station;
            WindowStationDetails wsd = new WindowStationDetails(selectedStation, allLines);
            wsd.Show();
        }
        /// <summary>
        /// To add station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStationEvent(object sender, RoutedEventArgs e)
        {
            AddStationWindow addStationWindow = new AddStationWindow(poStationCollection);
            addStationWindow.ShowDialog();
                if (addStationWindow.Updated == true)
                {
                    allLines.Clear();
                    ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
                    temp = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
                    foreach (PO.Line line in temp)
                    {
                        allLines.Add(line);
                    }
                    poStationCollection = Utillities.Convert(from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station));
                    lbStations.ItemsSource = poStationCollection;
                    lbStations.SelectedIndex = 0;
                    adjStatCollection = Utillities.Convert(from adjacentStation in bl.GetAllAdjacentStations() select Utillities.AdjacentStationsBoPoAdapter(adjacentStation));
                    lbAdjacent.ItemsSource = adjStatCollection;
                    lbAdjacent.SelectedIndex = 0;

                    MessageBox.Show("Station added successfully");
            }
        }
    }
}
