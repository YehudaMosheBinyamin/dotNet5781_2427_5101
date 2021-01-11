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
using BlApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        IBL bl;
        private BO.Line currentDisplayBusLine;
        IEnumerable<BO.AdjacentStations> adjacentStationsInLine;
        IEnumerable<BO.Line> linesCollection;

        //from stackoverflow
       // public  ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        //{
        //    return new ObservableCollection<T>(original);
       // }
        public LinesWindow()
        { bl = BlFactory.GetBl("1");
            InitializeComponent();
            linesCollection = bl.GetAllLines();
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;
            
        }

        public void ShowBusLine(BO.Line boLine)
        {
            
            currentDisplayBusLine = boLine;
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = bl.GetAllLineStationsByLine(currentDisplayBusLine.Id);
            lbAdjacentStations.DataContext = bl.GetAllAdjacentsStationsInLine(boLine);
            
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BO.Line));
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }
    }
    }

