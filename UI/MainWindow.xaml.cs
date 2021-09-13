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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //readonly IBL bl;
       IBL bl;
        public MainWindow(IBL bl1)
        {
            bl = bl1;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            //System.Windows.Data.CollectionViewSource busStopViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStopViewSource")));
            //// Load data by setting the CollectionViewSource.Source property:
            //// busStopViewSource.Source = [generic data source]
        }

        private void buttonBusLineInfo_Click(object sender, RoutedEventArgs e)
        {
            WindowBusLine windoBusLine= new WindowBusLine(bl);
            windoBusLine.ShowDialog();
        }

        private void buttonBusStop_Click(object sender, RoutedEventArgs e)
        {
            WindowBusStop windoBusStop = new WindowBusStop(bl);
            windoBusStop.ShowDialog();
        }
       private void buttonBusFleet_Click(object sender, RoutedEventArgs e)
        {
            WindowBus windoBus = new WindowBus(bl);
            windoBus.ShowDialog();
        }
        private void buttonBusSchedule_Click(object sender, RoutedEventArgs e)
        {
            WindowBusStopSchedule windowSchedule = new WindowBusStopSchedule(bl);
            windowSchedule.ShowDialog();
        }
    }
}
