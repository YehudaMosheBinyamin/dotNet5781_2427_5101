using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BlApi;

namespace PlGui
{
    /// <summary>
    /// Interaction logic for LinesWindow.xaml
    /// </summary>
    public partial class LinesWindow : Window
    {
        IBL bl;
        private PO.Line currentDisplayBusLine;
        public ObservableCollection<PO.Line> linesCollection;
        public PO.LineStation selectedLineStation;
        public PO.Station selectedStation;
        public ObservableCollection<PO.LineStation> poLineStationsOfLine;
        public ObservableCollection<PO.LineTrip> lineTripsOfLine;
        public LinesWindow(ObservableCollection<PO.Line> collectionOfLines)
        {
            InitializeComponent();
            bl = BlFactory.GetBl("1");
            lineTripsOfLine = new ObservableCollection<PO.LineTrip>();
            poLineStationsOfLine = new ObservableCollection<PO.LineStation>();
            linesCollection = collectionOfLines;
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;

        }
        /// <summary>
        /// To show details of line chosen in combobox
        /// </summary>
        /// <param name="lineNumber"></param>
        public void ShowBusLine(int lineId)
        {
            // currentDisplayBusLine = boLine;
            currentDisplayBusLine = linesCollection.FirstOrDefault(p => p.Id == lineId);
            UpGrid.DataContext = currentDisplayBusLine;
            poLineStationsOfLine = Utillities.Convert((from ls in bl.GetAllLineStationsByLine(currentDisplayBusLine.Id) select Utillities.LineStationBoPoAdapter(ls)).ToList());
            lbBusLineStations.DataContext = poLineStationsOfLine;
        }
        /// <summary>
        /// Event on change of selection of bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBusLines.SelectedIndex == -1)
            {
                cbBusLines.SelectedIndex = 0;
            }
            if ((cbBusLines.SelectedValue as PO.Line) != null)
            {
                ShowBusLine((cbBusLines.SelectedValue as PO.Line).Id);
            }
        }
        /// <summary>
        /// For updating distance between two stations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimeAndDistanceFromPrevChangedEvent(object sender, RoutedEventArgs e)
        {
            int index = cbBusLines.SelectedIndex;
            PO.LineStation poLineStation = lbBusLineStations.SelectedValue as PO.LineStation;
            LineStationUpdateWindow lineStationUpdateWindow = new LineStationUpdateWindow(poLineStation);
            
                lineStationUpdateWindow.ShowDialog();
            if (lineStationUpdateWindow.Updated == true)
            {
                linesCollection.Clear();
                ObservableCollection<PO.Line> temp = new ObservableCollection<PO.Line>();
                temp = Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
                foreach (PO.Line line in temp)
                {
                    linesCollection.Add(line);
                }
                cbBusLines.ItemsSource = linesCollection;
                cbBusLines.DisplayMemberPath = "Code";
                cbBusLines.SelectedIndex = index;
                MessageBox.Show("Time and distance between stations changed successfully");
            }
        }

        /// <summary>
        /// For addition of line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddLineEvent(object sender, RoutedEventArgs e)
        {
            AddLineWindow addLineWindow = new AddLineWindow(linesCollection);
            addLineWindow.ShowDialog();
            if (addLineWindow.Updated == true)
            {
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
        }
        /// <summary>
        /// For details about station
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayStationDetails(object sender, MouseButtonEventArgs e)
        {
            selectedLineStation = lbBusLineStations.SelectedValue as PO.LineStation;
            selectedStation = Utillities.StationBoPoAdapter(bl.GetStation(selectedLineStation.Station));
            WindowStationDetails wsd = new WindowStationDetails(selectedStation, linesCollection);
            wsd.Show();
        }
        /// <summary>
        /// For edit of line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditLineEvent(object sender, RoutedEventArgs e)
        {
            WindowEdit windowEdit = new WindowEdit(cbBusLines.SelectedValue as PO.Line);
            windowEdit.ShowDialog();
            if (windowEdit.Updated == true)
            {
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
                MessageBox.Show("Line edited successfully");
                int h = 4;
            }
        }
        /// <summary>
        /// For deletion of line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLine(object sender, RoutedEventArgs e)
        {
            PO.Line lineForDeletion = cbBusLines.SelectedValue as PO.Line;
            var dialogResult = MessageBox.Show($"Are you sure you want to delete line {lineForDeletion.Code}?", "Warning:Deletion with no option to undo", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (dialogResult == MessageBoxResult.Yes)
            {
                bl.DeleteLine(lineForDeletion.Id);
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
                MessageBox.Show("Line Deleted Successfully!");
            }
        }
        /// <summary>
        /// For click on timetable of line.Lets to see time table and edit it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bTimeTable_Click(object sender, RoutedEventArgs e)
        {
            TimeTableWindow timeTableWindow = new TimeTableWindow(cbBusLines.SelectedValue as PO.Line);
            PO.Line currentLine = cbBusLines.SelectedValue as PO.Line;
            timeTableWindow.ShowDialog();
            if (timeTableWindow.Updated == true)
            {
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
                MessageBox.Show("Line's timetable changed successfully!");
            }
        }
    }
}
