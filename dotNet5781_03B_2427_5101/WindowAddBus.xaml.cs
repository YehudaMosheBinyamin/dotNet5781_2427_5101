using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using dotNet5781_01_2427_5101;
namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for WindowAddBus.xaml
    /// </summary>
    public partial class WindowAddBus : Window
    {
        private ObservableCollection<Bus> bl;
        public Bus newBus;//bus to be added
        public WindowAddBus(ObservableCollection<Bus> busList)
        {
            bl = busList;
            InitializeComponent();
        }
        //on trigger of bAdd-button for addition add a new bus with details given by operator
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime reform = new DateTime(2018, 1, 1, 0, 0, 0);
            DateTime dateChosen = dpStart.DisplayDate;
            String licenseNumber = tbLicense.Text;
            bool formatOk = true;//if bus has compatible start date and license number length so it's possible to add bus to list
            if (dateChosen < reform)
            {
                if (licenseNumber.Length != 7)
                {
                    formatOk = false;
                    MessageBox.Show("License Numbers of buses for dates before 1/1/2018 must be 7 digits long", "Wrong License Length", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            if (dateChosen >= reform)
            {
                if (licenseNumber.Length != 8)
                {
                    formatOk = false;
                    MessageBox.Show("License Numbers of buses for dates after 1/1/2018 must be 8 digits long", "Wrong License Length", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            if (formatOk == true)
            {
                newBus = new Bus(tbLicense.Text, dpStart.DisplayDate);
                bl.Add(newBus);
                this.Close();
            }
        }
    }
}
