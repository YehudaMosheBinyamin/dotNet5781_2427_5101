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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for WindowEdit.xaml
    /// </summary>
    public partial class WindowEdit : Window
    {
        public PO.Line lineToBeEdited;
        public PO.Line newLine;//the line after editing
        ObservableCollection<PO.Station> listOfStations;
        ObservableCollection<PO.LineStation> stationsInLine;
        public WindowEdit(PO.Line lineEdited)
        {
            InitializeComponent();
            IBL bl = BlFactory.GetBl("1");
            listOfStations = Utillities.Convert((from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station)).ToList());
            lineToBeEdited = lineEdited;
            cbStations.ItemsSource = listOfStations;
            stationsInLine = lineToBeEdited.stationsInLine;
            lbLineStations.ItemsSource = stationsInLine;
            newLine = lineEdited;
            cbStations.SelectedIndex = 0;
            lbLineStations.SelectedIndex = 0;

        }
        /// <summary>
        /// For finishing operation of updating line and closing window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateLineEvent(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            bl.UpdateLine(lineToBeEdited.Id, Utillities.LinePoBoAdapter(newLine));
            Close();
            
        }
        /// <summary>
        /// For deletion of station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDeleteStation_Click(object sender, RoutedEventArgs e)
        {
            int indexOfDeletion = lbLineStations.SelectedIndex;
            PO.LineStation lineStationForDeletion = lbLineStations.SelectedValue as PO.LineStation;
            stationsInLine.Remove(lineStationForDeletion);
            newLine.stationsInLine = stationsInLine;
        }
        /// <summary>
        /// An update of the station-we swap the station at a given index with another one as per the request of the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bChangeStation_Click(object sender,RoutedEventArgs e) 
        {  
            int indexOfChangedStation = lbLineStations.SelectedIndex; 
            PO.Station selectedStation = cbStations.SelectedItem as PO.Station;
            PO.LineStation lineStationForDeletion = lbLineStations.SelectedValue as PO.LineStation;
            IBL bl = BlFactory.GetBl("1");
            if (indexOfChangedStation > 0 && indexOfChangedStation < stationsInLine.Count - 1)
            {   float distanceFromPrevious = bl.GetRandomDistance();
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = indexOfChangedStation,
                    NextStation = stationsInLine.ElementAt(indexOfChangedStation + 1).Station,
                    PrevStation = stationsInLine.ElementAt(indexOfChangedStation - 1).Station,
                    DistanceFromPreviousStation = distanceFromPrevious,
                    TimeFromPreviousStation = bl.GetMinutesOfTravel(distanceFromPrevious),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId =0,
                    LastStationName = lineToBeEdited.LastStationName,
                    Name = selectedStation.Name
                };
                distanceFromPrevious = bl.GetRandomDistance();
                TimeSpan timeFromPrevious = bl.GetMinutesOfTravel(distanceFromPrevious);
                //we'll update the time and distance of the next station because the previous station changed
                //the newly added station is the next station of the previous station...
                stationsInLine.ElementAt(indexOfChangedStation-1).NextStation = selectedStation.Code;
                stationsInLine.ElementAt(indexOfChangedStation + 1).DistanceFromPreviousStation = distanceFromPrevious;
                stationsInLine.ElementAt(indexOfChangedStation + 1).TimeFromPreviousStation = timeFromPrevious;
                stationsInLine.Remove(lineStationForDeletion);
                stationsInLine.Insert(indexOfChangedStation, newLineStation);
                lbLineStations.Items.Refresh();
            }
            else if (indexOfChangedStation == 0)
            {
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = indexOfChangedStation,
                    NextStation = stationsInLine.ElementAt(indexOfChangedStation + 1).Station,
                    PrevStation = selectedStation.Code,
                    DistanceFromPreviousStation = 0f,
                    TimeFromPreviousStation = new TimeSpan(0,0,0),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = 0,
                    LastStationName = lineToBeEdited.LastStationName,
                    Name = selectedStation.Name
                };
                float distanceFromPrevious = bl.GetRandomDistance();
                TimeSpan timeFromPrevious = bl.GetMinutesOfTravel(distanceFromPrevious);
                //we'll update the time and distance of the next station because the previous station changed
                stationsInLine.ElementAt(indexOfChangedStation + 1).DistanceFromPreviousStation = distanceFromPrevious;
                stationsInLine.ElementAt(indexOfChangedStation + 1).TimeFromPreviousStation = timeFromPrevious;
                stationsInLine.Remove(lineStationForDeletion);
                stationsInLine.Insert(indexOfChangedStation, newLineStation);
                lbLineStations.Items.Refresh();
            }
            else if(indexOfChangedStation== stationsInLine.Count - 1)
            {
                float distanceFromPrevious = bl.GetRandomDistance();
                TimeSpan timeFromPrevious = bl.GetMinutesOfTravel(distanceFromPrevious);
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = indexOfChangedStation,
                    NextStation = selectedStation.Code,
                    PrevStation = stationsInLine.ElementAt(indexOfChangedStation - 1).Station,
                    DistanceFromPreviousStation = distanceFromPrevious,
                    TimeFromPreviousStation = timeFromPrevious,
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = 0,
                    LastStationName = selectedStation.Name,
                    Name = selectedStation.Name
                };
                
                //the newly added station is the next station of the previous station...
                stationsInLine.ElementAt(indexOfChangedStation-1).NextStation = selectedStation.Code;
                stationsInLine.Remove(lineStationForDeletion); 
                stationsInLine.Insert(indexOfChangedStation, newLineStation);
                lbLineStations.Items.Refresh();
            }
            newLine = new PO.Line()
            {
                Id =0,
                stationsInLine = stationsInLine,
                Area = lineToBeEdited.Area,
                Code = lineToBeEdited.Code,
                InService = true,
                LastStationName = (stationsInLine.ElementAt(stationsInLine.Count - 1)).Name
            };
  
        }
        /// <summary>
        /// For addition of station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAddStation_Click(object sender, RoutedEventArgs e)
        { PO.Station selectedStation = cbStations.SelectedItem as PO.Station;
          IBL bl= BlFactory.GetBl("1");
            float distanceFromPrevious;
            if (stationsInLine.Count > 0)
            { distanceFromPrevious= bl.GetRandomDistance();
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = lbLineStations.Items.Count,
                    NextStation = selectedStation.Code,
                    PrevStation = (lbLineStations.Items[lbLineStations.Items.Count - 1] as PO.LineStation).Station,
                    DistanceFromPreviousStation = distanceFromPrevious,
                    TimeFromPreviousStation = bl.GetMinutesOfTravel(distanceFromPrevious),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = lineToBeEdited.Id,
                    LastStationName = lineToBeEdited.LastStationName,
                    Name = selectedStation.Name
                };

                PO.LineStation stationBefore = lbLineStations.Items[lbLineStations.Items.Count - 1] as PO.LineStation;
                stationBefore.NextStation = newLineStation.Station;
                stationsInLine.Add(newLineStation);
            }
            else
            {
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = lbLineStations.Items.Count,
                    NextStation = selectedStation.Code,
                    PrevStation = selectedStation.Code,
                    DistanceFromPreviousStation = 0f,
                    TimeFromPreviousStation = new TimeSpan(0, 0, 0),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = lineToBeEdited.Id,
                    LastStationName = lineToBeEdited.LastStationName,
                    Name = selectedStation.Name
                };
                stationsInLine.Add(newLineStation);
            }
            //lbLineStations.ItemsSource = listOfStations;
            lbLineStations.Items.Refresh();
        }

    } }
