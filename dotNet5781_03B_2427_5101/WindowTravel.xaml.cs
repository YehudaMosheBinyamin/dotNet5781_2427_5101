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
namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for WindowTravel.xaml
    /// </summary>
    public partial class WindowTravel : Window
    {
        Thread travelThread;
        private Bus currentBus;
        public WindowTravel(Bus bus)
        {
            currentBus = bus;
            InitializeComponent();
        }

     
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
                MessageBox.Show("Finished Journey");
                this.Close();
                
            }
        }

        private void travel(float timeJourney)
        {
            Thread.Sleep(6000*Convert.ToInt32(timeJourney));
            //travelDispatch(Convert.ToInt32(tbDistance.Text));
            travelDispatch(100);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {   int possibleDistance = currentBus.KmPossible;
            int distance= Convert.ToInt32(tbDistance.Text);
            int limitBeforeTreatment = 20000;
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
            else if (currentBus.IsDangerous)
            {
                MessageBox.Show("Bus is dangerous and bus be treated immediately");
                this.Close();
            }
            else
            {
                Random random = new Random(DateTime.Now.Millisecond);
                int speed = random.Next(20, 50);
                float time = float.Parse(tbDistance.Text) / speed;
                travelThread = new Thread(()=> travel(time));
                travelThread.Start();
               
            }
        }

        
    }
}
