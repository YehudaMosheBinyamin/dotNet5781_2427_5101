﻿using System;
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
using dotNet5781_01_2427_5101;
namespace dotNet5781_03B_2427_5101
{
    /// <summary>
    /// Interaction logic for WindowBusDetails.xaml
    /// </summary>
    public partial class WindowBusDetails : Window
    {public Bus currentBus;
        public WindowBusDetails(Bus bus)
        {
            currentBus = bus;
            InitializeComponent();
            myGrid.DataContext = currentBus;
        }

        private void btTreat_Click(object sender, RoutedEventArgs e)
        {
            currentBus.KmPossible = 1200;
            currentBus.LastTreated = DateTime.Now;
            currentBus.KmSinceTreated = 0;
            MessageBox.Show("Treatment Complete");
        }

        private void btRefuel_Click(object sender, RoutedEventArgs e)
        {
            currentBus.KmPossible = 1200;
            MessageBox.Show("Refuel complete");
        }
    }
}