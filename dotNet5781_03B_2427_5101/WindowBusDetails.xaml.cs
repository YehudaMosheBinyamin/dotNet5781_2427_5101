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
using System.Threading;
using System.Windows.Threading;
using dotNet5781_01_2427_5101;
namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for WindowBusDetails.xaml
    /// </summary>
    public partial class WindowBusDetails : Window
    {public Bus currentBus;
        Thread refillUpdateThread;
        Thread treatmentThread;
        public WindowBusDetails(Bus bus)
        {
            currentBus = bus;
            InitializeComponent();
            myGrid.DataContext = currentBus;
        }
        //on trigger by click on bTreat the bus is sent for treatment(if impossible,warning is sent)
        private void btTreat_Click(object sender, RoutedEventArgs e)
        {
            Bus selectedBus = currentBus;
            Status stateNow = selectedBus.State;
            if ((stateNow == Status.Ready)&&(!(selectedBus.IsDangerous)))
            {
                btTreat.IsEnabled = false;
                MessageBox.Show("Treatment started.24 Hours to finish");
                selectedBus.State = Status.Treatment;
                treatmentThread = new Thread(TreatmentUpdate);
                treatmentThread.Start();
            }
            else
            {
                MainWindow.ShowImpossibleRequest(currentBus, currentBus.State,Status.Treatment);
            }
        }
        //dispatch function to send operation of treatment back to main thread
        private void treatDispatch(int kmPossible)
        {
            if (!CheckAccess())
            {
                Action<int> d = treatDispatch;
                Dispatcher.BeginInvoke(d, kmPossible);
            }
            else
            {
                currentBus.KmPossible = 1200;
                currentBus.LastTreated = DateTime.Now;
                currentBus.State = Status.Ready;
                currentBus.KmSinceTreated = 0;
                tbLastTreated.Text = currentBus.LastTreated.ToString();
                tbKmPossible.Text = currentBus.KmPossible.ToString();
                tbKmLastTreated.Text = currentBus.KmSinceTreated.ToString();
                MessageBox.Show("Treatment Complete");
                btTreat.IsEnabled = true;
            }
        }
        //function that makes process of treatment 24 hours long(simulated) and sends to process to dispatcher to end it
        private void TreatmentUpdate()
        {
            Thread.Sleep(144000);
            treatDispatch(1200) ;
        }
        //on trigger by click on refill button the bus is sent for refueling(if impossible,warning is sent)
        private void btRefuel_Click(object sender, RoutedEventArgs e)
        {
            Bus selectedBus = currentBus;
            Status stateNow = selectedBus.State;
            if ((stateNow == Status.Ready )&&(selectedBus.KmPossible<1200)&&(!(selectedBus.IsDangerous)))
            {
                btRefuel.IsEnabled = false;
                MessageBox.Show("Refuel started.2 Hours to finish");
                selectedBus.State = Status.Refilling;
                refillUpdateThread = new Thread(RefillUpdate);
                refillUpdateThread.Start();
            }
            else if (selectedBus.KmPossible == 1200)
            {
                MessageBox.Show("It is full already", "Can't refill this bus",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            else
            {
                MainWindow.ShowImpossibleRequest(currentBus, currentBus.State,Status.Refilling);
            }
            
        }
        //dispatch function to send operation of refilling back to main thread
        private void kmDispatch(int km)
        {
            if (!CheckAccess())
            {
                Action<int> d = kmDispatch;
                Dispatcher.BeginInvoke(d, km);
            }
            else
            {
                currentBus.KmPossible = 1200;
                currentBus.State = Status.Ready;
                tbKmPossible.Text = currentBus.KmPossible.ToString();
                MessageBox.Show("Refilled");
                btRefuel.IsEnabled = true;
            }
        }

        //function that makes process of refueling 2 hours long(simulated) and sends to process to dispatcher to end it
        private void RefillUpdate()
        {
            Thread.Sleep(12000);
            kmDispatch(1200);
        }
 
    }
}
