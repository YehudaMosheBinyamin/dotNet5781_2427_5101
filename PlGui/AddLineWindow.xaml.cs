using BlApi;
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
using System.Threading;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        List<PO.Areas> areasList;
        //ObservableCollection<int> cLineStation;
        ObservableCollection<PO.LineStation> lineStationsOfLine;
        ObservableCollection<PO.Line> lineCollection;
        public AddLineWindow(ObservableCollection<PO.Line>collectionLines)
        {
            IBL bl = BlFactory.GetBl("1");
            InitializeComponent();
            lineStationsOfLine = new ObservableCollection<PO.LineStation>();
            areasList = new List<PO.Areas>();
            areasList.Add(PO.Areas.Center);
            areasList.Add(PO.Areas.General);
            areasList.Add(PO.Areas.Jerusalem);
            areasList.Add(PO.Areas.North);
            areasList.Add(PO.Areas.South);
            cbArea.ItemsSource = areasList;
            cbArea.SelectedIndex = 0;
            lineCollection = collectionLines;
            lbLineStations.ItemsSource = lineStationsOfLine;
            cbStations.ItemsSource = Utillities.Convert((from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station)).ToList());
            cbStations.SelectedIndex = 0;
            lbLineStations.SelectedIndex = 0;
        }
        /// <summary>
        /// on click of add button to list of stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddStationToLine(object sender, RoutedEventArgs e)
        {
            PO.Station selectedStation = cbStations.SelectedItem as PO.Station;
            IBL bl = BlFactory.GetBl("1");
            float distanceFromPrevious;  
            //if there is already a station existing,the new station has a previous station to take it's distance and time from
            if (lineStationsOfLine.Count > 0)
            {   distanceFromPrevious=bl.GetRandomDistance();
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = lbLineStations.Items.Count,
                    NextStation = selectedStation.Code,
                    PrevStation = (lbLineStations.Items[lbLineStations.Items.Count - 1] as PO.LineStation).Station,
                    DistanceFromPreviousStation = distanceFromPrevious,
                    TimeFromPreviousStation = bl.GetMinutesOfTravel(distanceFromPrevious),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = 0,
                    LastStationName = lineStationsOfLine.Last().Name,
                    Name = selectedStation.Name
                };
                    PO.LineStation stationBefore = lbLineStations.Items[lbLineStations.Items.Count - 1] as PO.LineStation;
                    stationBefore.NextStation = newLineStation.Station;
                    lineStationsOfLine.Add(newLineStation);
            }
            else
            {
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = lbLineStations.Items.Count,
                    NextStation = selectedStation.Code,
                    PrevStation = selectedStation.Code,
                    DistanceFromPreviousStation = 0f,
                    TimeFromPreviousStation = new TimeSpan(0,0,0),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = 0,
                    LastStationName =selectedStation.Name,
                    Name = selectedStation.Name
                };    
                
                lineStationsOfLine.Add(newLineStation);
            }
        
            //lbLineStations.ItemsSource = listOfStations;
            lbLineStations.Items.Refresh();
        }
        /// <summary>
        /// To complete addition of line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLineEvent(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            if (lineStationsOfLine.Count < 2)
            {
                MessageBox.Show("You must add at least two stations", "Wrong Line Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                    PO.Areas area= (PO.Areas)cbArea.SelectedItem;
                    int codeLine = string.IsNullOrEmpty(tbCode.Text) ? 0 : int.Parse(tbCode.Text);
                    string lastStationName = lineStationsOfLine.Last().Name;
                    int lineId = 0;
                    float randomDistance = bl.GetRandomDistance();
                ObservableCollection<PO.LineTrip> poLineTrips = new ObservableCollection<PO.LineTrip>()
                {
                    new PO.LineTrip{Id=0,InService=true,LineId=0,StartAt=new TimeSpan(8,0,0) }
                };

                    PO.Line newLine = new PO.Line
                    {
                        Area = area,
                        Code = codeLine,
                        Id = lineId,
                        LastStationName = lastStationName,
                        stationsInLine = lineStationsOfLine,
                        InService = true,
                        lineExits=poLineTrips

                    };
                bl.AddLine(Utillities.LinePoBoAdapter(newLine));
                this.Close();
                }
           
    }
    }
}
