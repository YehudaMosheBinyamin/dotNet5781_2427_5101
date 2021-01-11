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

using BlApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for WindowStationDetails.xaml
    /// </summary>
    public partial class WindowStationDetails : Window
    {
        IBL bl;
        private BO.Station boStation;
        private IEnumerable<BO.Line> linesByStation;
        public WindowStationDetails(BO.Station station)
        {
            bl = BlFactory.GetBl("1");
            boStation = station;
            InitializeComponent();
            myGrid.DataContext = boStation;
            linesByStation = bl.GetAllLinesByStation(boStation.Code);
            gridLines.DataContext = linesByStation;
            lbLinesBy.ItemsSource = linesByStation;
            }

        }
    }

