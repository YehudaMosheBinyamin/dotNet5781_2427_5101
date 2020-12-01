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

namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for WindowTravel.xaml
    /// </summary>
    public partial class WindowTravel : Window
    {
        private Bus currentBus;
        public WindowTravel(Bus bus)
        {
            currentBus = bus;
            InitializeComponent();
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
                currentBus.KmPossible -= distance;
                currentBus.KmSinceTreated += distance;
                this.Close();
            }
        }
    }
}
