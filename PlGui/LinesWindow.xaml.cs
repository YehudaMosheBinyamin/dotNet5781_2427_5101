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
        ObservableCollection<BO.Line> linesCollection;
        public LinesWindow()
        { bl = BlFactory.GetBl("1");
            InitializeComponent();
            linesCollection = (ObservableCollection<BO.Line>)bl.GetAllLines();
            cbBusLines.ItemsSource = linesCollection;
            cbBusLines.DisplayMemberPath = "Code";
            cbBusLines.SelectedIndex = 0;
            //tbArea.Text = busLineColl;
            //tbArea.DisplayMemberPath = "Operating Area";

        }

        public void ShowBusLine(int lineId)
        {
            //[busNumber].First()

            BO.Line line=bl.GetLine(lineId);
            currentDisplayBusLine = line;
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = bl.GetAllLineStationsByLine(line.Id);
        }
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BO.Line).Id);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {

        }
    }
    }

