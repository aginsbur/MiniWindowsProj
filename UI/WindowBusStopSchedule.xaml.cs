using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Diagnostics;
using System.ComponentModel;
using BL;

namespace UI
{

    /// <summary>
    /// Interaction logic for WindowBusStopSchedule.xaml
    /// </summary>
    public partial class WindowBusStopSchedule : Window
    {
        IBL bl;
        BO.BusStop currBS;
        TimeSpan tsStartTime;
        BackgroundWorker timerworker;
        Stopwatch stopWatch;
        bool isTimeRun;
        IEnumerable<BO.LineOnTrip> lineOnTrip;
        public WindowBusStopSchedule(IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            cbBusStops.DataContext = bl.getAllBusStops().ToList().OrderBy(x => x.StationCode);
            timerTextBlock.Visibility = Visibility.Collapsed;
        }
        private void cbBusStops_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currBS = cbBusStops.SelectedItem as BO.BusStop;
            currBS.BusLinesUsingStation = bl.getBusLinesInStation(currBS);
            lBusLinesInStation.DataContext =currBS.BusLinesUsingStation.ToList().OrderBy(x => x.ID);
            lineOnTrip= bl.getAllLinesOntripByBusStopAndHour((cbBusStops.SelectedItem as BO.BusStop).StationCode, DateTime.Now.TimeOfDay).ToList().OrderBy(x => x.TimeOfArrival);//sets listBoxes source as listOfBuses
            dgLineOnTrip.ItemsSource = lineOnTrip;//sets listBoxes source as listOfBuses
            buttonStartSimulator.IsEnabled = true;//allow user to start simulater
        }

       /// <summary>
       /// starts the simulater for the current bus stop
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void buttonSimulator_click(object sender, RoutedEventArgs e)
        {
            buttonEndSimulator.Visibility = Visibility.Visible;
            timerTextBlock.Visibility = Visibility.Visible;
            stopWatch = new Stopwatch();
            timerworker = new BackgroundWorker();
            timerworker.WorkerSupportsCancellation = true;
            timerworker.DoWork += Worker_DoWork;
            timerworker.ProgressChanged += Worker_ProgressChanged;
            timerworker.WorkerReportsProgress = true;
            tsStartTime = DateTime.Now.TimeOfDay;
            stopWatch.Restart();
            isTimeRun = true;
            timerworker.RunWorkerAsync();
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while(isTimeRun)
            {
                timerworker.ReportProgress(100);
                Thread.Sleep(1000);
            }
            timerworker.CancelAsync();
        }
        private void Worker_ProgressChanged(object sender,ProgressChangedEventArgs e)
        {
            TimeSpan tsCurrentTime = tsStartTime + stopWatch.Elapsed;
            string timerText = tsCurrentTime.ToString().Substring(0,8);
            this.timerTextBlock.Text = timerText;
            dgLineOnTrip.ItemsSource =lineOnTrip.ToList().FindAll(x=> (x.TimeOfArrival.Hours > tsCurrentTime.Hours) || ((x.TimeOfArrival.Hours >= tsCurrentTime.Hours) && (x.TimeOfArrival.Minutes > tsCurrentTime.Minutes)) || ((x.TimeOfArrival.Hours >= tsCurrentTime.Hours) && (x.TimeOfArrival.Minutes >= tsCurrentTime.Minutes) && (x.TimeOfArrival.Seconds >= tsCurrentTime.Seconds)));
        
        }

        private void buttonEbdSimulator_click(object sender, RoutedEventArgs e)
        {
            timerTextBlock.Visibility = Visibility.Collapsed;
            isTimeRun = false;
            buttonEndSimulator.Visibility = Visibility.Collapsed;
        }

        private void lBusLinesInStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
