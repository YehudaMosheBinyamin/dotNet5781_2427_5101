using dotNet5781_02_2427_5101;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace dotNet5781_03A_2427_5101
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BusLine currentDisplayBusLine;
        BusLineCollection busLineColl;
        public MainWindow()
        {
            InitializeComponent();
            int numOfBuses = 10;
            int numOfStations = 40;
            List<BusStop> busStops = new List<BusStop>();
            busLineColl = new BusLineCollection();
            //---------------------                                                                                                                                           
            for (int i = 0; i < numOfStations; i++)
            {
                busStops.Add(new BusLineStop());

            }
            List<BusLineStop> busStopsForLine = new List<BusLineStop>();
            BusLineStop bls1 = new BusLineStop();
            BusLineStop bls2 = new BusLineStop();
            BusLineStop bls3 = new BusLineStop();
            BusLineStop bls4 = new BusLineStop();
            int j = 0;
            for (int i = 0; i < numOfBuses; i++)
            {
                bls1 = busStops[j] as BusLineStop;
                bls2 = busStops[j + 1] as BusLineStop;
                bls3 = busStops[j + 2] as BusLineStop;
                bls4 = busStops[j + 3] as BusLineStop;
                j = j + 4;
                busStopsForLine.Add(bls1);
                busStopsForLine.Add(bls2);
                busStopsForLine.Add(bls3);
                busStopsForLine.Add(bls4);
                busLineColl.addBusLine(new BusLine(busStopsForLine));
                busStopsForLine.Clear();
            }
            //adding buses to lines so that 10 bus stops will have more than one line that goes past them
            j = 39;
            BusLineStop busLineStopExtra = new BusLineStop();
            foreach (BusLine bl in busLineColl)
            {
                int codeLast = bl.ListStations[bl.ListStations.Count - 1].BusStationKey;
                busLineStopExtra = busStops[j] as BusLineStop;
                BusLineStop beforeStop = null;
                foreach (BusLineStop bls in bl.ListStations)
                {
                    if (bls.BusStationKey == codeLast)
                    {
                        beforeStop = bls;
                    }
                }
                bl.AddBusStop(busLineStopExtra, beforeStop);
                j = j - 4;
            }
            cbBusLines.ItemsSource = busLineColl;
            cbBusLines.DisplayMemberPath = "BusNumber";
            cbBusLines.SelectedIndex = 0;


        }

        public void ShowBusLine(int busNumber)
        {
            currentDisplayBusLine = busLineColl[busNumber].First();
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.ListStations;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).BusNumber);
        }



    }
}
