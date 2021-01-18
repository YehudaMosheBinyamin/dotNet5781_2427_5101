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
        //private BO.Line currentDisplayBusLine;
        private PO.Line currentDisplayBusLine;
        //IEnumerable<BO.Line> linesCollection;
        public ObservableCollection<PO.Line> linesCollection;
        //public BO.LineStation selectedLineStation;
        public PO.LineStation selectedLineStation;
        //public BO.Station selectedStation;
        public PO.Station selectedStation;

        //from stackoverflow
       // public  ObservableCollection<T> Convert<T>(IEnumerable<T> original)
        //{
        //    return new ObservableCollection<T>(original);
       // }
        public LinesWindow()
        { bl = BlFactory.GetBl("1");
            InitializeComponent();
            linesCollection = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;
            
        }

        //public void ShowBusLine(BO.Line boLine)
        public void ShowBusLine(PO.Line poLine)
        {

            // currentDisplayBusLine = boLine;
            currentDisplayBusLine = poLine;
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = from ls in bl.GetAllLineStationsByLine(currentDisplayBusLine.Id) select Utillities.LineStationBoPoAdapter(ls);
   
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ShowBusLine((cbBusLines.SelectedValue as BO.Line));
            ShowBusLine((cbBusLines.SelectedValue as PO.Line));
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }

        private void lbBusLineStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //selectedLineStation = lbBusLineStations.SelectedValue as BO.LineStation;
            selectedLineStation = lbBusLineStations.SelectedValue as PO.LineStation;
            selectedStation = Utillities.StationBoPoAdapter(bl.GetStation(selectedLineStation.Station));
            WindowStationDetails wsd = new WindowStationDetails(selectedStation);
            wsd.Show();
        }
    }
    }

