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
    /// Interaction logic for UpdateTimeDistance.xaml
    /// </summary>
    public partial class UpdateTimeDistance : Window
    {
        public PO.LineStation lineStationForEditing;
        public PO.AdjacentStations adjForUpdate;
        bool isAdj;//we are editing adjacent stations from an instance of AdjacentStations
        public UpdateTimeDistance(PO.AdjacentStations adjForUpdate)
        {
            InitializeComponent();
            isAdj = true;
            this.adjForUpdate = adjForUpdate;
            mainGrid.DataContext = this.adjForUpdate;
            tbTime.IsEnabled = false;
            tbDistance.IsEnabled = false;
        }
        public UpdateTimeDistance(PO.LineStation lineStationForEdit)
        {
            InitializeComponent();
            lineStationForEditing = lineStationForEdit;
            mainGrid.DataContext = lineStationForEditing;
            tbDistAdj.IsEnabled = false;
            tbTimeAdj.IsEnabled = false;

        }
        /// <summary>
        /// An event when finishing to update time and distance between two stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishUpdate(object sender, RoutedEventArgs e)
        {
            if (isAdj == false)
            {
                float newDistance;
                TimeSpan newTime;
                bool conversionTime = TimeSpan.TryParse(tbTime.Text, out newTime);
                if (conversionTime == false)
                {
                    MessageBox.Show("Refill the time with a valid time of format HH:MM:SS and try again");
                    return;
                }
                bool conversionDistance = float.TryParse(tbDistance.Text, out newDistance);
                if (conversionDistance == false)
                {
                    MessageBox.Show("Refill the distance with a valid number and try again");
                    return;
                }
                newDistance = (float)Math.Round(newDistance, 2);
                IBL bl = BlFactory.GetBl("1");
                if (lineStationForEditing != null)
                {
                    bl.UpdateAdjacentStations(lineStationForEditing.PrevStation, lineStationForEditing.Station, newDistance, newTime);
                }
               
                Close();
            }
            else
            {
                float newDistance;
                TimeSpan newTime;
                bool conversionTime = TimeSpan.TryParse(tbTimeAdj.Text, out newTime);
                if (conversionTime == false)
                {
                    MessageBox.Show("Refill the time with a valid time of format HH:MM:SS and try again");
                    return;
                }
                bool conversionDistance = float.TryParse(tbDistAdj.Text, out newDistance);
                if (conversionDistance == false)
                {
                    MessageBox.Show("Refill the distance with a valid number and try again");
                    return;
                }
                newDistance = (float)Math.Round(newDistance, 2);
                IBL bl = BlFactory.GetBl("1");
                bl.UpdateAdjacentStations(adjForUpdate.Station1, adjForUpdate.Station2, newDistance, newTime);
                Close();
            }
        }
    }
}
