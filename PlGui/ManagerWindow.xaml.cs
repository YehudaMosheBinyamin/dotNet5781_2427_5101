using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
        BackgroundWorker backgroundWorker;
        ObservableCollection<PO.Line> poLineCollection;
        public ManagerWindow(ObservableCollection<PO.Line> poLineCollection)
        {
            InitializeComponent();
            this.poLineCollection = poLineCollection;
            //wb.Navigate("http://www.google.com/maps/@32.226743,34.747009,9z?hl=iw");
            //dynamic activeX = this.wb.GetType().InvokeMember("ActiveXInstance",BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.NonPublic,null, this.wb, new object[] { });//Carol from https://stackoverflow.com/questions/1298255/how-do-i-suppress-script-errors-when-using-the-wpf-webbrowser-control
            //activeX.Silent = true;
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            //backgroundWorker.WorkerReportsProgress = true;
            //backgroundWorker.WorkerSupportsCancellation = true;
        }
        /// <summary>
        /// Sends time, speed and updateUIAction(An action delegate that receives a new time of the screen from the observable Clock in BL and updates it) to bl layer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> list = e.Argument as List<object>;
            int speed = (int)list.ElementAt(0);
            TimeSpan time= (TimeSpan)list.ElementAt(1);
            Action<TimeSpan> updateUIAction = (Action<TimeSpan>)list.ElementAt(2);
            IBL bl = BlFactory.GetBl("1");
            bl.StartSimulator(time, speed, updateUIAction);
        }

        /// <summary>
        /// To display lines window on click of btLines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LinesWindow linesWindow = new LinesWindow(poLineCollection);
            linesWindow.Show();
        }
        /// <summary>
        /// For Displaying StationsWindow on click of btStations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StationsWindow stationsWindow = new StationsWindow(poLineCollection);
            stationsWindow.Show();
        }
        /// <summary>
        /// Updates time shown on the screen- in tbHour, tbMinutes and tbSeconds
        /// </summary>
        /// <param name="newTime"></param>
        void updateTime(TimeSpan newTime)
        {
            Dispatcher.Invoke(() =>
            {
                tbHours.Text = newTime.Hours.ToString();
                tbMinutes.Text = newTime.Minutes.ToString();
                tbSeconds.Text = newTime.Seconds.ToString();

            });
        }
        /// <summary>
        /// Function to start and end btStopWatch. Sends request to bl in a BackgroundWorker thread.
        /// Note that since btStopwatch is operating on a separate thread it measures time inaccurately- there are Thread.sleep calls for 100ms each time a new time is calculated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartStopStopwatch(object sender, RoutedEventArgs e)
        {
            if (btStopwatch.Content.ToString() == "Go")
            {
                int hours;
                int minutes;
                int seconds;
                bool convertHours = Int32.TryParse(tbHours.Text, out hours);
                if (convertHours == false || hours > 24 || hours < 0)
                {
                    MessageBox.Show("Hours must be a number between 0 to 24");
                    tbHours.Text = "";
                    return;
                }
                if (tbHours.Text.Length < 2)
                {
                    tbHours.Text = "0" + tbHours.Text;
                }
                if (tbHours.Text.Length > 2)
                {
                    tbHours.Text = tbHours.Text.TrimStart(' ', '0');
                }
                bool convertMinutes = Int32.TryParse(tbMinutes.Text, out minutes);
                if (convertMinutes == false || minutes > 59 || minutes < 0)
                {
                    MessageBox.Show("Minutes must be a number between 0 to 59");
                    tbMinutes.Text = "";
                    return;
                }
                if (tbMinutes.Text.Length < 2)
                {
                    tbHours.Text = "0" + tbMinutes.Text;
                }
                if (tbMinutes.Text.Length > 2)
                {
                    tbMinutes.Text = tbMinutes.Text.TrimStart(' ', '0');
                }
                bool convertSeconds = Int32.TryParse(tbSeconds.Text, out seconds);
                if (convertSeconds == false || seconds > 59 || seconds < 0)
                {
                    MessageBox.Show("Seconds must be a number between 0 to 59");
                    tbSeconds.Text = "";
                    return;
                }
                if (tbMinutes.Text.Length < 2)
                {
                    tbSeconds.Text = "0" + tbSeconds.Text;
                }
                if (tbSeconds.Text.Length > 2)
                {
                    tbSeconds.Text = tbSeconds.Text.TrimStart(' ', '0');
                }
                int speed;
                bool convertSpeed = Int32.TryParse(tbSpeed.Text, out speed);
                if (convertSpeed == false || speed < 0)
                {
                    MessageBox.Show("Speed must be a positive number");
                    tbSpeed.Text = "";
                    return;
                }
                tbSpeed.IsEnabled = false;
                tbHours.IsEnabled = false;
                tbSeconds.IsEnabled = false;
                tbMinutes.IsEnabled = false;
                btStopwatch.Content = "Stop";
                IBL bl = BlFactory.GetBl("1");
                TimeSpan startTime = new TimeSpan(hours, minutes, seconds);
                Action<TimeSpan> action = updateTime;
                //Credit for sending list of objects: https://social.msdn.microsoft.com/Forums/vstudio/en-US/d7c0ba24-29b7-4fc9-86ef-92fb8cd5e17a/sending-multiple-arguments-to-background-worker?forum=csharpgeneral
                List<object> backgroundWorkerList = new List<object>();
                backgroundWorkerList.Add(speed);
                backgroundWorkerList.Add(startTime);
                backgroundWorkerList.Add(action);
                backgroundWorker.RunWorkerAsync(backgroundWorkerList);
            }
            else
            {
                IBL bl = BlFactory.GetBl("1");
                bl.StopSimulator();
                tbSpeed.IsEnabled = true;
                tbHours.IsEnabled = true;
                tbSeconds.IsEnabled = true;
                tbMinutes.IsEnabled = true;
                btStopwatch.Content = "Go";
            }
        }
    }
}
