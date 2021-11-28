using BlApi;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;


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
        public bool Updated { get; set; }
        public WindowEdit(PO.Line lineEdited)
        {
            InitializeComponent();
            IBL bl = BlFactory.GetBl("1");
            listOfStations = Utillities.Convert((from station in bl.GetAllStations() select Utillities.StationBoPoAdapter(station)).ToList());
            lineToBeEdited = lineEdited;
            cbStations.ItemsSource = listOfStations;
            stationsInLine = lineToBeEdited.stationsInLine;
            lbLineStations.ItemsSource = stationsInLine;
            newLine = new PO.Line()
            {
                Area = lineEdited.Area,
                Code = lineEdited.Code,
                Id = 0,
                InService = true,
                LastStationName = lineEdited.LastStationName,
                stationsInLine = lineEdited.stationsInLine,
                lineExits = lineEdited.lineExits
            };

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
            Updated = true;
            Close();
        }
        /// <summary>
        /// For deletion of line station from list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDeleteStation_Click(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            int indexOfDeletion = lbLineStations.SelectedIndex;
            PO.LineStation lineStationForDeletion = lbLineStations.SelectedValue as PO.LineStation;
            bool adjacentStationsExist = true;
            //The first station will be deleted
            if (indexOfDeletion == 0)
            {
                PO.LineStation newFirstStation = stationsInLine.ElementAt(1);
                newFirstStation.PrevStation = newFirstStation.Station;
                newFirstStation.TimeFromPreviousStation = new TimeSpan(0, 0, 0);
                newFirstStation.DistanceFromPreviousStation = 0f;
            }
            //If the last station is to be deleted
            else if (indexOfDeletion == stationsInLine.Count - 1)
            {
                PO.LineStation newLastStop = stationsInLine.ElementAt(indexOfDeletion - 1);
                newLastStop.NextStation = newLastStop.Station;
                //Change all previous stations' last station to name of second last station
                foreach (PO.LineStation ls in stationsInLine)
                {
                    ls.LastStationName = newLastStop.Name;
                }
            }
            else
            {
                PO.LineStation stopBeforeDeletion = stationsInLine.ElementAt(indexOfDeletion - 1);
                PO.LineStation stopAfterDeletion = stationsInLine.ElementAt(indexOfDeletion - 1);
                stopBeforeDeletion.NextStation = stopAfterDeletion.Station;
                adjacentStationsExist = bl.AdjacentStationsExists(stopAfterDeletion.Station, stopAfterDeletion.Station);
                if (!adjacentStationsExist)
                {
                    float distance = bl.GetRandomDistance();
                    TimeSpan waitingTime = bl.GetMinutesOfTravel(distance);
                    stopAfterDeletion.DistanceFromPreviousStation = distance;
                    stopAfterDeletion.TimeFromPreviousStation = waitingTime;
                    bl.AddAdjacentStations(stopBeforeDeletion.Station, stopAfterDeletion.Station, distance, waitingTime);
                }


            }
            stationsInLine.Remove(lineStationForDeletion);
            foreach (PO.LineStation ls in stationsInLine)
            {
                ls.LineStationIndex = stationsInLine.IndexOf(ls);
            }
            newLine.stationsInLine = stationsInLine;
            lbLineStations.Items.Refresh();
        }
        /// <summary>
        /// An update of the station-we swap the station at a given index with another one as per the request of the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwapStation(object sender, RoutedEventArgs e)
        {
            int indexOfChangedStation = lbLineStations.SelectedIndex;
            PO.Station selectedStation = cbStations.SelectedItem as PO.Station;
            PO.LineStation lineStationForDeletion = lbLineStations.SelectedValue as PO.LineStation;
            IBL bl = BlFactory.GetBl("1");
            //If the replacement is for a line station isn't for the first or last station
            if (indexOfChangedStation > 0 && indexOfChangedStation < stationsInLine.Count - 1)
            {
                float distanceFromPrevious = bl.GetRandomDistance();
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = indexOfChangedStation,
                    NextStation = stationsInLine.ElementAt(indexOfChangedStation + 1).Station,
                    PrevStation = stationsInLine.ElementAt(indexOfChangedStation - 1).Station,
                    DistanceFromPreviousStation = distanceFromPrevious,
                    TimeFromPreviousStation = bl.GetMinutesOfTravel(distanceFromPrevious),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = 0,
                    LastStationName = lineToBeEdited.LastStationName,
                    Name = selectedStation.Name
                };
                distanceFromPrevious = bl.GetRandomDistance();
                TimeSpan timeFromPrevious = bl.GetMinutesOfTravel(distanceFromPrevious);
                //we'll update the time and distance of the next station because the previous station changed
                //the newly added station is the next station of the previous station...
                stationsInLine.ElementAt(indexOfChangedStation - 1).NextStation = selectedStation.Code;//The next staion is the new station
                stationsInLine.ElementAt(indexOfChangedStation + 1).DistanceFromPreviousStation = distanceFromPrevious;//The distance to station after replaced station is different than before
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
                    TimeFromPreviousStation = new TimeSpan(0, 0, 0),
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
            else if (indexOfChangedStation == stationsInLine.Count - 1)
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
                stationsInLine.ElementAt(indexOfChangedStation - 1).NextStation = selectedStation.Code;
                stationsInLine.Remove(lineStationForDeletion);
                stationsInLine.Insert(indexOfChangedStation, newLineStation);
                lbLineStations.Items.Refresh();
            }

        }
        /// <summary>
        /// For addition of station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAddStation_Click(object sender, RoutedEventArgs e)
        {
            PO.Station selectedStation = cbStations.SelectedItem as PO.Station;
            IBL bl = BlFactory.GetBl("1");
            float distanceFromPrevious = bl.GetRandomDistance();
            PO.LineStation stationBefore = lbLineStations.Items[lbLineStations.Items.Count - 1] as PO.LineStation;
            bool adjacentStationsExists = bl.AdjacentStationsExists(stationBefore.Station, selectedStation.Code);
            float distance = bl.GetRandomDistance();
            TimeSpan timeFromPrevious = bl.GetMinutesOfTravel(distance);
            if (stationsInLine.Count > 0 && adjacentStationsExists == false)
            {
                distanceFromPrevious = bl.GetRandomDistance();
                PO.LineStation newLineStation = new PO.LineStation()
                {
                    LineStationIndex = lbLineStations.Items.Count,
                    NextStation = selectedStation.Code,
                    PrevStation = stationBefore.Station,
                    DistanceFromPreviousStation = distanceFromPrevious,
                    TimeFromPreviousStation = bl.GetMinutesOfTravel(distanceFromPrevious),
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = lineToBeEdited.Id,
                    LastStationName = selectedStation.Name,
                    Name = selectedStation.Name
                };
                stationBefore.NextStation = newLineStation.Station;
                stationsInLine.Add(newLineStation);
                bl.AddAdjacentStations(stationBefore.Station, selectedStation.Code, distance, timeFromPrevious);
            }
            else if (stationsInLine.Count > 0 && adjacentStationsExists == true)
            {
                PO.AdjacentStations adjStat = Utillities.AdjacentStationsBoPoAdapter(bl.GetAdjacentStations(lineToBeEdited.stationsInLine.ElementAt(lineToBeEdited.stationsInLine.Count - 1).Station, selectedStation.Code));
                PO.LineStation poLineStation = new PO.LineStation()
                {
                    LineStationIndex = lineToBeEdited.stationsInLine.Count,
                    NextStation = selectedStation.Code,
                    PrevStation = lineToBeEdited.stationsInLine.ElementAt(lineToBeEdited.stationsInLine.Count - 1).Station,
                    DistanceFromPreviousStation = adjStat.Distance,
                    TimeFromPreviousStation = adjStat.Time,
                    InService = true,
                    Station = selectedStation.Code,
                    LineId = lineToBeEdited.Id,
                    LastStationName = selectedStation.Name,
                    Name = selectedStation.Name
                };
                stationBefore.NextStation = poLineStation.Station;
                stationsInLine.Add(poLineStation);
            }
            else//this is going to be first stop in line
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
            lbLineStations.Items.Refresh();
        }

    }
}
