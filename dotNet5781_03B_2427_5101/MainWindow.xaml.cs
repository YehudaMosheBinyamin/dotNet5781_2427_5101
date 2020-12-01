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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Threading;
using System.Threading;
namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Thread refillThread;
        Thread refillUpdateThread;
        bool continueTreatment;
        public string licensePlate;
        public Bus selectedBus;
        public ObservableCollection<Bus> activeBusList;
        public  ObservableCollection<string> licenseNotPossible;
        bool continueRefilling;
        
        public MainWindow()
        {
            InitializeComponent();
            int licenseLength;
            licenseNotPossible = new ObservableCollection<string>();
            activeBusList = new ObservableCollection<Bus>();
            Bus newBus;
            DateTime date;
            string licenseNumber;
            DateTime reform = new DateTime(2018, 1, 1, 0, 0, 0);
            for (int i = 0; i < 10; i++)
            {
                date = Functions.RandomDate(DateTime.Now);

                if (date < reform) 
                { 
                    licenseLength = 7; 
                }
                else 
                { 
                    licenseLength = 8; 
                }
                licenseNumber = Functions.RandomLicense(licenseNotPossible,licenseLength);
                licenseNotPossible.Add(licenseNumber);
                newBus = new Bus(licenseNumber, date);
                activeBusList.Add(newBus);
            }
            activeBusList[7].LastTreated = activeBusList[7].Start.AddYears(-2);
            activeBusList[8].KmSinceTreated = 19900;
            activeBusList[9].KmPossible = 1;
            lbBuses.ItemsSource= activeBusList;
            lbBuses.SelectedIndex = 0;
            continueRefilling = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            WindowAddBus window = new WindowAddBus(activeBusList);
            window.Show();   
        }
        private void lbBuses_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowTravel windowTravel = new WindowTravel(lbBuses.SelectedValue as Bus);
            windowTravel.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            selectedBus = lbBuses.SelectedValue as Bus;
            WindowBusDetails windowBusDetails = new WindowBusDetails(selectedBus);
            windowBusDetails.Show();
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
                selectedBus.KmPossible = 1200;
                lbBuses.Items.Refresh();
                MessageBox.Show("Refilled");
            }
        }
        private void waitRefill()
        {
            Thread.Sleep(12000);
        }
        //after refill is complete
        private void RefillUpdate()
        {
            kmDispatch(1200);
        }
    
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {   
            selectedBus = lbBuses.SelectedValue as Bus;
            refillThread = new Thread(waitRefill);
            refillThread.Start();
            refillUpdateThread = new Thread(RefillUpdate);
            refillUpdateThread.Start();
            refillUpdateThread.Join();  
        }
    } }
