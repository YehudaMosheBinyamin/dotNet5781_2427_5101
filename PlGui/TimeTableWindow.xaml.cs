using BlApi;
using System;
using System.Collections.ObjectModel;
using System.Windows;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for TimeTableWindow.xaml
    /// </summary>
    public partial class TimeTableWindow : Window
    {   public bool Updated { get; set; }
        private PO.Line previousLine;//line before edit
        ObservableCollection<PO.LineTrip> lineExits;
        private PO.Line updatedLine;//line after edit
        public TimeTableWindow(PO.Line currentLine)
        {
            InitializeComponent();
            previousLine = currentLine; 
            lineExits = Utillities.Convert(previousLine.lineExits);
            updatedLine = new PO.Line()
            {
                Area = previousLine.Area,
                Code = previousLine.Code,
                Id = 0,
                InService = true,
                LastStationName = previousLine.LastStationName,
                stationsInLine = previousLine.stationsInLine,
                lineExits = lineExits
            };
            lbLineExits.ItemsSource = lineExits;
        }
        /// <summary>
        /// To delete line exit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDeleteLineTrip_Click(object sender, RoutedEventArgs e)
        {
            PO.LineTrip lineTripForRemoval = lbLineExits.SelectedItem as PO.LineTrip;
            lineExits.Remove(lineTripForRemoval);
            updatedLine.lineExits = lineExits;
        }
        /// <summary>
        /// To add a line trip to list of line trips
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bAddLineTrip_Click(object sender,RoutedEventArgs e)
        {
            int indexBefore = lbLineExits.SelectedIndex;//the index before adding line exit
            int indexForAddition = indexBefore + 1;
            TimeSpan newExitTime;
            bool ans = TimeSpan.TryParse(tbTime.Text, out newExitTime);
            if (ans == false)
            {
                MessageBox.Show("Time format error.Re-enter time and try again");
                return;
            }
            PO.LineTrip newLineExit = new PO.LineTrip()
            {
                Id = 0,
                InService = true,
                LineId = previousLine.Id,
                StartAt = newExitTime
            };
            lineExits.Insert(indexForAddition, newLineExit);

        }
        /// <summary>
        /// To finish operation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeTableFinishEvent(object sender, RoutedEventArgs e)
        {
            IBL bl = BlFactory.GetBl("1");
                updatedLine.lineExits = lineExits;
                bl.UpdateLine(previousLine.Id, Utillities.LinePoBoAdapter(updatedLine));
                Updated = true;
            Close();
        }
        /// <summary>
        /// To add a time before all other times
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            int indexForAddition = 0;
            TimeSpan newExitTime;
            bool ans = TimeSpan.TryParse(tbTime.Text, out newExitTime);
            if (ans == false)
            {
                MessageBox.Show("Time format error.Re-enter time and try again");
                return;
            }
            PO.LineTrip newLineExit = new PO.LineTrip()
            {
                Id = 0,
                InService = true,
                LineId = previousLine.Id,
                StartAt = newExitTime
            };
            lineExits.Insert(indexForAddition, newLineExit);
            updatedLine.lineExits = lineExits;
        }
    }
  
}
