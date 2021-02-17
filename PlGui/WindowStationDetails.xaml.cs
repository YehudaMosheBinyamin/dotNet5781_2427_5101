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
    /// Interaction logic for WindowStationDetails.xaml
    /// </summary>
    public partial class WindowStationDetails : Window
    {
        IBL bl;
        //private BO.Station boStation;
        private PO.Station poStation;
        public PO.Line lineSelected;
        public ObservableCollection<PO.Line> linesByStation;
        public WindowStationDetails(PO.Station station)
        {
            bl = BlFactory.GetBl("1");
            //boStation = station;
            poStation = station;
            InitializeComponent();
            //myGrid.DataContext = boStation;
            myGrid.DataContext = poStation;
            linesByStation = Utillities.Convert(from line in bl.GetAllLinesByStation(poStation.Code) select Utillities.LineBoPoAdapter(line));
           // lineStationsByStation = Utillities.Convert(station.LineStationsOfStation);
            gridLines.DataContext = linesByStation;
            lbLinesBy.ItemsSource = linesByStation;
            //gridLines.DataContext = lineStationsByStation;
            //lbLinesBy.ItemsSource = lineStationsByStation;
            }
       /// <summary>
        /// For deletion of line from station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bDelLine_Click(object sender, RoutedEventArgs e)
        {
            //IBL bl = BlFactory.GetBl("1");
            //lineSelected=lbLinesBy.SelectedValue as PO.Line;

        }
        /// <summary>
        /// To edit line that goes by station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bEdit_Click(object sender, RoutedEventArgs e)
        {
            PO.Line line = lbLinesBy.SelectedValue as PO.Line;
            WindowEdit windowEdit = new WindowEdit(line);
            windowEdit.ShowDialog();
            MessageBox.Show("Line edited successfully");
        }
    }
    }

