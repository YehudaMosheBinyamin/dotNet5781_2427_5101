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

namespace PlGui
{
    /// <summary>
    /// Interaction logic for AddLineWindow.xaml
    /// </summary>
    public partial class AddLineWindow : Window
    {
        ObservableCollection<PO.Line> collectionLines;
        public AddLineWindow(ObservableCollection<PO.Line>collectionLines)
        {
            InitializeComponent();
        }
        //on click of add button to list of stations
        private void AddStationToLine(object sender, RoutedEventArgs e)
        {
            lbLineStations.Items.Add(tbNewStationCode.Text);
        }

        private void AddLineEvent(object sender, RoutedEventArgs e)
        {
           /** List<int> result = new List<int>();
            foreach (ListItem li in lbLineStations.Items)
            {
                
                    // get the value of the item in your loop
                    result.Add(li.);
                
            }**/
            
           
            /**PO.Line newLine=new PO.Line{Area=tbArea.Text,Code=Int32.Parse(tbCode.Text),InService=true,stationsInLine=lbLineStations.Items }
                 public ObservableCollection<LineStation> stationsInLine { get; set; }
        //public IEnumerable<LineTrip> lineExits {get; set;}
        public int Id { get; set; }
        public int Code { get; set; }
        public Areas Area { get; set; }
        public bool InService;
        public string LastStationName { get; set; }**/
    }
    }
}
