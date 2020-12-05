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
using System.Timers;
namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ///The decision to use Thread instead of BackgroundWorker is that BackgroundWorker is 
        ///for improving operation time of processes while here the choice to send refueling 
        ///operation to thread is in order to have it take longer in background while the rest
        ///of the processes can continue in the meantime.This consideration has been applied to other 
        ///time-consuming processes of travel and treatment
        Thread refillUpdateThread;
        public string licensePlate;
        public Bus selectedBus;
        public ObservableCollection<Bus> activeBusList;
        public  ObservableCollection<string> licenseNotPossible;
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
                newBus.KmPossible = 1200;
                activeBusList.Add(newBus);
            }
            activeBusList[7].LastTreated = activeBusList[7].Start.AddYears(-2);
            activeBusList[8].KmSinceTreated = 19900;
            activeBusList[9].KmPossible = 1;
            lbBuses.ItemsSource= activeBusList;
            lbBuses.SelectedIndex = 0; 
        }
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            WindowAddBus window = new WindowAddBus(activeBusList);
            window.Show();   
        }
        //double click shows the details of bus
        private void dc_ShowInfo(object sender,RoutedEventArgs e)
        {
            selectedBus = lbBuses.SelectedValue as Bus;
            WindowBusDetails windowBusDetails = new WindowBusDetails(selectedBus);
            windowBusDetails.Show();

        }
        //on trigger of bTravel pressed
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Bus selectedBus = lbBuses.SelectedValue as Bus;
            if ((selectedBus.State == Status.Ready)&&(!selectedBus.IsDangerous))
            {
                WindowTravel windowTravel = new WindowTravel(selectedBus);
                windowTravel.Show();
            }
            else
            {
                ShowImpossibleRequest(selectedBus,selectedBus.State,Status.Transit);
            }
            

        }
        //prints details of why the operation is mpossible at the moment
        public static void ShowImpossibleRequest(Bus busyBus, Status stateNow,Status wantedStatus)
        {
            String requestedStatus = wantedStatus.ToString();
            if (busyBus.IsDangerous)
            {
                MessageBox.Show("Cannot"+requestedStatus+  "since bus is dangerous.Treat this bus immediately", "Dangerous Bus Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            if (stateNow == Status.Refilling)
            {
                MessageBox.Show(requestedStatus+ "impossible because bus is refilling", "Can't fulfill request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (stateNow == Status.Transit)
            {
                MessageBox.Show(requestedStatus+" impossible because bus is busy transit now", "Can't fulfill request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show(requestedStatus+" impossible because bus is in treatment now", "Can't fulfill request", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //on trigger of double click the details of the bus are shown in new window
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {   selectedBus = lbBuses.SelectedValue as Bus;
            WindowBusDetails windowBusDetails = new WindowBusDetails(selectedBus);
            windowBusDetails.Show();
        }
        //dispatch function to send process of refueling to main thread
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
                selectedBus.State = Status.Ready;
                lbBuses.Items.Refresh();
                
                MessageBox.Show("Refilled");
            }
        }
        
        //function to delay the completion of refilling by two hours(simulated) and afterwards sends refilling process back to main thread via kmDispatch
        private  void RefillUpdate()
        {   
            Thread.Sleep(12000);
            kmDispatch(1200);
        }
       
        //trigger for refueling bus
        private void Button_Click_3(object sender, RoutedEventArgs e)
        { 
            selectedBus = lbBuses.SelectedValue as Bus;
            Status stateNow = selectedBus.State;
            if (stateNow == Status.Ready&&selectedBus.KmPossible<1200)
            {
                //MessageBox.Show("refilling...");
                selectedBus.State = Status.Refilling;
                refillUpdateThread = new Thread(RefillUpdate);
                refillUpdateThread.Start();
            }
            else if (selectedBus.KmPossible == 1200)
            {
                MessageBox.Show("It is full already", "Can't refill this bus", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ShowImpossibleRequest(selectedBus,stateNow,Status.Refilling);
            }
        }
    } }
