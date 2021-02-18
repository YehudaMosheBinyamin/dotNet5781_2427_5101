using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {

        ObservableCollection<PO.Line> poLineCollection;
        public ManagerWindow(ObservableCollection<PO.Line> poLineCollection)
        {
            InitializeComponent();
            this.poLineCollection = poLineCollection;


        }
        /// <summary>
        /// Updates UI time based on running clock of simulation
        /// </summary>
        /// <param name="newTime"></param>
        private void UpdateUITimeFunc(TimeSpan newTime)
        {
            tbHour.IsEnabled = true;
            tbMinutes.IsEnabled = true;
            tbSeconds.IsEnabled = true;
            tbHour.Text = Convert.ToString(newTime.Hours);
            tbMinutes.Text = Convert.ToString(newTime.Minutes);
            tbSeconds.Text = Convert.ToString(newTime.Seconds);

        }

        /// <summary>
        /// To display lines window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LinesWindow linesWindow = new LinesWindow(poLineCollection);
            linesWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StationsWindow stationsWindow = new StationsWindow(poLineCollection);
            stationsWindow.Show();
        }
        /// <summary>
        /// On click of start button of simulation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if ((string)btStart.Content == "Start")
            {
                tbHour.IsEnabled = false;
                tbMinutes.IsEnabled = false;
                tbSeconds.IsEnabled = false;
                tbSpeed.IsEnabled = false;
                btStart.Content = "STOP";
                int hours = Convert.ToInt32(tbHour.Text);
                int minutes = Convert.ToInt32(tbMinutes.Text);
                int seconds = Convert.ToInt32(tbSeconds.Text);
                int speed = Convert.ToInt32(tbSpeed.Text);
                TimeSpan startTime = new TimeSpan(hours, minutes, seconds);
                Action<TimeSpan> updateUITimeFunction = UpdateUITimeFunc;
                IBL bl = BlFactory.GetBl("1");
                bl.StartSimulator(startTime, speed, updateUITimeFunction);
                
            }
            else
            {
                IBL bl = BlFactory.GetBl("1");
                bl.StopSimulator();
                tbHour.IsEnabled = true;
                tbMinutes.IsEnabled = true;
                tbSeconds.IsEnabled = true;
                tbSpeed.IsEnabled = true;
                btStart.IsEnabled = true;
                btStart.Content = "Start"; 
            }
        }
    }
}
