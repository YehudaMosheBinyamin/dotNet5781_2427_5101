using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
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
        ObservableCollection<PO.Line> allLines;

        public WindowStationDetails(PO.Station station,ObservableCollection<PO.Line> linesCollection)
        {
            bl = BlFactory.GetBl("1");
            poStation = station;
            this.allLines = linesCollection;
            InitializeComponent();
            myGrid.DataContext = poStation;
            linesByStation = Utillities.Convert(from line in bl.GetAllLinesByStation(poStation.Code) select Utillities.LineBoPoAdapter(line));
            gridLines.DataContext = linesByStation;
            lbLinesBy.ItemsSource = linesByStation;
         }
     /**
        private void bDelLine_Click(object sender, RoutedEventArgs e)
        {
            //IBL bl = BlFactory.GetBl("1");
            //lineSelected=lbLinesBy.SelectedValue as PO.Line;

        }**/
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
            allLines.Clear();
            ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
            temp = Utillities.Convert(from l in bl.GetAllLines() select Utillities.LineBoPoAdapter(l));
            foreach (PO.Line l in temp)
            {
                allLines.Add(l);
            }
            MessageBox.Show("Line edited successfully");
        }
    }
    }

