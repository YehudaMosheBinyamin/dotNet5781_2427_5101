using BlApi;
using System;
using System.Windows;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for LineStationUpdateWindow.xaml
    /// </summary>
    public partial class LineStationUpdateWindow : Window
    {
        public bool Updated { get; set; }
        public PO.LineStation lineStationForEditing;
        public LineStationUpdateWindow(PO.LineStation lineStationForEdit)
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
                    Updated = true;
            }

            Close();
        }
    }
}
