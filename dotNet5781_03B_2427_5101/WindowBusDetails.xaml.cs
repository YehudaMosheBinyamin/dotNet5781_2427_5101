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

        private void btTreat_Click(object sender, RoutedEventArgs e)
        {
            
            treatmentThread = new Thread(RefillUpdate);
            treatmentThread.Start();

        }
        
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
                currentBus.KmSinceTreated = 0;
                MessageBox.Show("Treatment Complete");
            }
        }
        private void TreatmentUpdate()
        {
            Thread.Sleep(144000);
            treatDispatch(1200) ;
        }

        private void btRefuel_Click(object sender, RoutedEventArgs e)
        {
            
            refillUpdateThread = new Thread(RefillUpdate);
            refillUpdateThread.Start();
            
        }
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
                MessageBox.Show("Refilled");
            }
        }


        private void RefillUpdate()
        {
            Thread.Sleep(12000);
            kmDispatch(1200);
        }

       
        
    }
}
