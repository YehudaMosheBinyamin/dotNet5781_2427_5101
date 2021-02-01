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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BlApi;
namespace PlGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<PO.Line> linesCollection;
        IBL bl;
        public MainWindow()
        {InitializeComponent();
         bl = BlFactory.GetBl("1");
         linesCollection = new ObservableCollection<PO.Line>();
         linesCollection= Utillities.Convert(from line in bl.GetAllLines() select Utillities.LineBoPoAdapter(line));
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ManagerWindow managerWindow = new ManagerWindow(linesCollection);
            managerWindow.Show();
        }
    }
}
