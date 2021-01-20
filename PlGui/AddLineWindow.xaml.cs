﻿using BlApi;
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
using System.Linq;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        List<PO.Areas> areasList;
        ObservableCollection<int> cLineStation;
        List<PO.LineStation> lineStationsOfLine;
        ObservableCollection<PO.Line> lineCollection;
        public AddLineWindow(ObservableCollection<PO.Line>collectionLines)
        {
            InitializeComponent();
            lineStationsOfLine = new List<PO.LineStation>();
            areasList = new List<PO.Areas>();
            areasList.Add(PO.Areas.Center);
            areasList.Add(PO.Areas.General);
            areasList.Add(PO.Areas.Jerusalem);
            areasList.Add(PO.Areas.North);
            areasList.Add(PO.Areas.South);
            cbArea.ItemsSource = areasList;
            cbArea.SelectedIndex = 0;
            lineCollection = collectionLines;
            //lbLineStations.DataContext = cLineStation;
            lbLineStations.IsEnabled = true;

            
        }
        //on click of add button to list of stations
        private void AddStationToLine(object sender, RoutedEventArgs e)
        {
            lbLineStations.ItemsSource = null;
            lbLineStations.Items.Add(tbNewStationCode.Text);
            //lbLineStations.IsEnabled = false;
          
            //lbLineStations.ItemsSource = cLineStation;
        }

        private void AddLineEvent(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            cLineStation = Utillities.Convert((from i in lbLineStations.Items.OfType<string>().ToList() select int.Parse(i)).ToList());
            if (cLineStation.Count < 2)
            {
                MessageBox.Show("You must add at least two stations", "Wrong Line Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                PO.Areas area= (PO.Areas)cbArea.SelectedItem;
                int codeLine = string.IsNullOrEmpty(tbCode.Text) ? 0 : int.Parse(tbCode.Text);
                //int codeLine = Int32.Parse(tbCode.Text);
                //string areaString = tbArea.Text;
                //bool toArea = Enum.TryParse(areaString, out area);

               // if (toArea == false)
                //{
               //     MessageBox.Show("Area must be either Jerusalem North South Center or Jerusalem", "Wrong Area Error", MessageBoxButton.OK, MessageBoxImage.Error);
               // }
               // else

               // {
                    string lastStationName = bl.GetStation(cLineStation.Last()).Name;
                    int lineId = 0;
                    /////////////////////////////////////////////////////////////////////////////
                    float randomDistance = bl.GetRandomDistance();
                    int firstStationCode = cLineStation.First();
                    PO.LineStation firstLineStation = new PO.LineStation() { LineStationIndex = 0, NextStation = cLineStation.ElementAt(1), PrevStation = 00000, DistanceFromPreviousStation = randomDistance, TimeFromPreviousStation = bl.GetMinutesOfTravel(randomDistance), InService = true, LineId = 0, LastStationName = lastStationName, Station = firstStationCode, Name = bl.GetStation(firstStationCode).Name };
                    lineStationsOfLine.Add(firstLineStation);
                    lineStationsOfLine.AddRange((from stationCode in cLineStation
                                                 where cLineStation.IndexOf(stationCode) > 0 && cLineStation.IndexOf(stationCode) < cLineStation.Count - 1
                                                 let distance = bl.GetRandomDistance()
                                                 select new PO.LineStation
                                                 {
                                                     LineStationIndex = cLineStation.IndexOf(stationCode),
                                                     NextStation = cLineStation.ElementAt(cLineStation.IndexOf(stationCode) + 1),
                                                     PrevStation = cLineStation.ElementAt(cLineStation.IndexOf(stationCode) - 1),
                                                     DistanceFromPreviousStation = distance,
                                                     TimeFromPreviousStation = bl.GetMinutesOfTravel(distance),
                                                     InService = true,
                                                     Station = stationCode,
                                                     LineId = lineId,
                                                     LastStationName = lastStationName,
                                                     Name = bl.GetStation(stationCode).Name
                                                 }).ToList());
                    PO.LineStation secondLastStation = lineStationsOfLine.Last();
                    float distanceFromSecondLast = bl.GetRandomDistance();
                    int lastStationCode = cLineStation.Last();
                    PO.LineStation lastStation = new PO.LineStation() { LineStationIndex = cLineStation.Count - 1, DistanceFromPreviousStation = distanceFromSecondLast, NextStation = 00000, PrevStation = cLineStation.ElementAt(cLineStation.Count - 2), TimeFromPreviousStation = bl.GetMinutesOfTravel(distanceFromSecondLast), InService = true, LastStationName = lastStationName, LineId = 0, Name = lastStationName, Station = lastStationCode };
                    lineStationsOfLine.Add(lastStation);
                    bool lineInService = true;
                    //string lastStationName = lineStationsOfLine.Last().Name;
                    PO.Line newLine = new PO.Line
                    {
                        Area = area,
                        Code = codeLine,
                        Id = lineId,
                        LastStationName = lastStationName,
                        stationsInLine = Utillities.Convert(lineStationsOfLine),
                        InService = lineInService

                    };
                    bl.AddLine(Utillities.LinePoBoAdapter(newLine));
                    lineCollection.Add(newLine);
                    this.Close();
                }
           // }
    }
    }
}
