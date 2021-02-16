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
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        ObservableCollection<PO.Line> poLineCollection;
        public ManagerWindow(ObservableCollection<PO.Line> poLineCollection)
        {
            InitializeComponent();
            this.poLineCollection = poLineCollection;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LinesWindow linesWindow = new LinesWindow(poLineCollection);
            linesWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StationsWindow stationsWindow = new StationsWindow(poLineCollection);
            stationsWindow.Show();
        }
    }
}
