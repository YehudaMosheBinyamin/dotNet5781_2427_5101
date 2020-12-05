using dotNet5781_01_2427_5101;
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
using System.Windows.Threading;
using System.Threading;
using System.Diagnostics;

namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for WindowTravel.xaml
    /// </summary>
    public partial class WindowTravel : Window
    {
        Stopwatch stopWatch;
        Thread travelThread;
        private Bus currentBus;
        //constructor
        public WindowTravel(Bus bus)
        {
            stopWatch = new Stopwatch();
            currentBus = bus;
            InitializeComponent();
        }

     //dispatch function to send travel operation back to main thread
        private void travelDispatch(int km)
        {
            if (!CheckAccess())
            {
                Action<int> d = travelDispatch;
                Dispatcher.BeginInvoke(d, km);
            }
            else
            {
                currentBus.KmPossible -= Convert.ToInt32(tbDistance.Text);
                currentBus.KmSinceTreated += Convert.ToInt32(tbDistance.Text);
                currentBus.State = Status.Ready;
                MessageBox.Show("Finished Journey");
                this.Close();
                
            }
        }
        //makes travel take the travel operation end after it arrives to destination after timeJourney
        private void travel(float timeJourney)
        {
            stopWatch.Reset();
            Thread.Sleep(6000*Convert.ToInt32(timeJourney));
            //travelDispatch(Convert.ToInt32(tbDistance.Text));
            travelDispatch(100);
        }
        //on bTravel the bus travels the distance requested by operator.In case of impossible request an appropriate warning is sent to operator
        private void Button_Click(object sender, RoutedEventArgs e)
        {   int possibleDistance = currentBus.KmPossible;
            int distance= Convert.ToInt32(tbDistance.Text);
            int limitBeforeTreatment = 20000;
            if ((currentBus.State == Status.Ready)&&(!currentBus.IsDangerous))
            {
                if (distance > possibleDistance)
                {
                    MessageBox.Show("Not enough gas for journey");
                    this.Close();
                }
                else if (currentBus.KmSinceTreated + distance > limitBeforeTreatment)
                {
                    MessageBox.Show("Bus must be treated before such a  journey");
                    this.Close();
                }
                else
                {   
                    currentBus.State = Status.Transit;
                    Random random = new Random(DateTime.Now.Millisecond);
                    int speed = random.Next(20, 50);
                    float time = float.Parse(tbDistance.Text) / speed;
                    string notification = String.Format("The journey has started.Estimated Time of arrival: {0}", time);
                    MessageBox.Show(notification);
                    travelThread = new Thread(() => travel(time));
                    travelThread.Start();

                }
            }
            else
            {
                MainWindow.ShowImpossibleRequest(currentBus, currentBus.State,Status.Transit);
            }
        }

        
    }
}
