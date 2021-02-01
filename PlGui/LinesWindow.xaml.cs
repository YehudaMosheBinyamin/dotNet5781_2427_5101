using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
        //public BO.Station selectedStation;
        //private BO.Line currentDisplayBusLine;
        //IEnumerable<BO.Line> linesCollection;
        //public BO.LineStation selectedLineStation;
        private PO.Line currentDisplayBusLine;
        public ObservableCollection<PO.Line> linesCollection;
        public PO.LineStation selectedLineStation;
        public PO.Station selectedStation;
        public ObservableCollection<PO.LineStation> poLineStationsOfLine;
        public event PropertyChangedEventHandler PropertyChanged;
        public LinesWindow(ObservableCollection<PO.Line> collectionOfLines)
        {
            InitializeComponent();
            bl = BlFactory.GetBl("1");
            poLineStationsOfLine = new ObservableCollection<PO.LineStation>();
            linesCollection = collectionOfLines;
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;

        }
        public void ShowBusLine(int lineNumber)
        {
            // currentDisplayBusLine = boLine;
            currentDisplayBusLine = linesCollection.FirstOrDefault(p => p.Code == lineNumber);
            UpGrid.DataContext = currentDisplayBusLine;
            poLineStationsOfLine = Utillities.Convert((from ls in bl.GetAllLineStationsByLine(currentDisplayBusLine.Id) select Utillities.LineStationBoPoAdapter(ls)).ToList());
            lbBusLineStations.DataContext = poLineStationsOfLine;
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((cbBusLines.SelectedValue as PO.Line) != null)
            {
                //ShowBusLine((cbBusLines.SelectedValue as PO.Line));
                ShowBusLine((cbBusLines.SelectedValue as PO.Line).Code);
            }
        }

        private void AddLineEvent(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow(linesCollection);
            addLineWindow.ShowDialog();
            //linesCollection = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
            linesCollection.Clear();
            ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
            temp = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
            foreach (PO.Line line in temp)
            {
                linesCollection.Add(line);
            }
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;
            MessageBox.Show("Line added successfully");
        }
        /// <summary>
        /// For details about station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbBusLineStations_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //selectedLineStation = lbBusLineStations.SelectedValue as BO.LineStation;
            selectedLineStation = lbBusLineStations.SelectedValue as PO.LineStation;
            selectedStation = Utillities.StationBoPoAdapter(bl.GetStation(selectedLineStation.Station));
            WindowStationDetails wsd = new WindowStationDetails(selectedStation);
            wsd.Show();
        }
        private void EditLineEvent(object sender, RoutedEventArgs e)
        {
            WindowEdit windowEdit = new WindowEdit(cbBusLines.SelectedValue as PO.Line);
            windowEdit.ShowDialog();
            linesCollection.Clear();
            ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
            temp = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
            foreach (PO.Line line in temp)
            {
                linesCollection.Add(line);
            }
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;
            MessageBox.Show("Line added successfully");
        }
        /// <summary>
        /// For deletion of line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PO.Line lineForDeletion = cbBusLines.SelectedValue as PO.Line;
            //linesCollection.Remove(lineForDeletion);
            bl.DeleteLine(lineForDeletion.Id);
            linesCollection.Clear();
            ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
            temp = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
            foreach(PO.Line line in temp) 
            {
                linesCollection.Add(line);
            }
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;
            MessageBox.Show("Line Deleted Successfully!");
        }
    }
}
