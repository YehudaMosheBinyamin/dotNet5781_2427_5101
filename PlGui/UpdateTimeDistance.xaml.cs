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
    {  public PO.LineStation lineStationForEditing;
        public UpdateTimeDistance(PO.LineStation lineStationForEdit)
        {
            InitializeComponent();
            lineStationForEditing = lineStationForEdit;
            mainGrid.DataContext = lineStationForEditing;

        }
        /// <summary>
        /// An event when finishing to update time and distance between two stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishUpdate(object sender, RoutedEventArgs e)
        {
            tbDistance.IsEnabled = false;
            tbTime.IsEnabled = false;
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
            bl.UpdateAdjacentStations(lineStationForEditing.PrevStation, lineStationForEditing.Station,newDistance, newTime);
            Close();
        }
    }
}
