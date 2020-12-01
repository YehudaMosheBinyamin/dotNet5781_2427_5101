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
        public Bus newBus;
        public WindowAddBus(ObservableCollection<Bus> busList)
        {
            bl = busList;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             newBus = new Bus(tbLicense.Text,dpStart.DisplayDate);
            bl.Add(newBus);
            this.Close();
        }
    }
}
