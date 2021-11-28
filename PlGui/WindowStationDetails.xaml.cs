using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for WindowStationDetails.xaml
    /// </summary>
    public partial class WindowStationDetails : Window
    {
        IBL bl;
        BackgroundWorker backgroundWorker;
        //private BO.Station boStation;
        private PO.Station poStation;
        public PO.Line lineSelected;
        public ObservableCollection<PO.Line> linesByStation;
        public ObservableCollection<PO.LineTiming> arrivalCollection;
        public ObservableCollection<PO.LineTiming> arrivedCollection;
        public PO.LineTiming closestToStation;
        ObservableCollection<PO.Line> allLines;
        public bool Updated { get; set; }
        public WindowStationDetails(PO.Station station, ObservableCollection<PO.Line> linesCollection)
        {
            linesByStation = new ObservableCollection<PO.Line>();
            closestToStation = new PO.LineTiming();
            bl = BlFactory.GetBl("1");
            poStation = station;
            this.allLines = linesCollection;
            InitializeComponent();
            myGrid.DataContext = poStation;
            linesByStation = Utillities.Convert(from line in bl.GetAllLinesByStation(poStation.Code) select Utillities.LineBoPoAdapter(line));
            gridLines.DataContext = linesByStation;
            lbLinesBy.ItemsSource = linesByStation;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
        }

        /// <summary>
        /// To start tracking by sending a request to BL layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
            int station = poStation.Code;
            Action<BO.LineTiming, int> action = updateArrivalsPanel;
            bl.SetStationPanel(station, action);
        }

        /// <summary>
        /// To edit line that goes by station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            PO.Line line = lbLinesBy.SelectedValue as PO.Line;
            WindowEdit windowEdit = new WindowEdit(line);
            windowEdit.ShowDialog();
            allLines.Clear();
            ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
            temp = Utillities.Convert(from l in bl.GetAllLines() select Utillities.LineBoPoAdapter(l));
            foreach (PO.Line l in temp)
            {
                allLines.Add(l);
            }
            if (windowEdit.Updated == true)
            {
                MessageBox.Show("Line edited successfully");
            }
        }
        /// <summary>
        /// Update lbLastThere to display closest bus to station, and the list lbArrivals of buses approaching station
        /// </summary>
        /// <param name="lineTiming"></param>
        /// <param name="station"></param>
        private void updateArrivalsPanel(BO.LineTiming lineTiming, int station)
        {
            arrivalCollection = new ObservableCollection<PO.LineTiming>();
            arrivedCollection = new ObservableCollection<PO.LineTiming>();
            if (station != -1)
            {
                Dispatcher.Invoke(() =>
              {
                  PO.LineTiming currentLineApproaching = arrivalCollection.FirstOrDefault(p => p.LineId == lineTiming.LineId);
                  //The line doesn't appear yet in approaching lines
                  if (currentLineApproaching == null)
                  {
                      arrivalCollection.Add(Utillities.LineTimingBoPoAdapter(lineTiming));
                  }
                  else//Bus already is in list
                  {
                      int indexCurrentInCollection = arrivalCollection.IndexOf(currentLineApproaching);
                      arrivalCollection[indexCurrentInCollection].WaitingTime = lineTiming.EstWaitingTime;//update waiting time for line.
                  }
                  //Credit: https://stackoverflow.com/questions/19112922/sort-observablecollectionstring-through-c-sharp
                  arrivalCollection = new ObservableCollection<PO.LineTiming>(arrivalCollection.OrderBy(arrival => arrival.WaitingTime));
                  closestToStation = arrivalCollection.ElementAt(0);
                  //If a line has arrived, or is predicted to be already at the station remove the line from the arrivalCollection and display it in lbLastThere
                  if (closestToStation.WaitingTime == TimeSpan.Zero)
                  {
                      arrivalCollection.RemoveAt(0);
                      arrivedCollection.Clear();
                      arrivedCollection.Add(closestToStation);
                      lbLastThere.ItemsSource = arrivedCollection;
                  }
                  lbArrivals.ItemsSource = arrivalCollection;
              });
            }
        }


        /// <summary>
        /// Start running tracking thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startTrackStation(object sender, RoutedEventArgs e)
        {
            if (btTrack.Content.ToString() == "Track")
            {
                IBL bl = BlFactory.GetBl("1");
                bool isClockOn = bl.IsClockOn();
                if (!isClockOn)
                {
                    MessageBox.Show("Run clock in ManagerWindow, and try again");
                    return;
                }
                backgroundWorker.RunWorkerAsync();
                btTrack.Content = "Stop Track";
              
            }
            else
            {
                IBL bl = BlFactory.GetBl("1");
                bl.StopSimulator();  
                if (arrivalCollection != null)
                { 
                    arrivalCollection.Clear(); 
                }
                if (arrivedCollection != null)
                { 
                    arrivedCollection.Clear(); 
                }
                btTrack.Content = "Track";
            }

        }
    }
}

