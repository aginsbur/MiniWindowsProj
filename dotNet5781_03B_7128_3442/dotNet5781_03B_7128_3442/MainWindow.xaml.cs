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
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_03B_7128_3442
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static public BusList listOfBuses = new BusList();//creats a collection of type BusList
        
       /// <summary>
       /// inserts 10 randome buses into the list box
       /// </summary>
       public MainWindow()
        {
            Random rnd = new Random();
            string license;//used as license in randomly generated buses
            for (int i = 0; i < 10; i++)//creates 10 random buses
            {
                license = (rnd.Next(1000000, 100000000)).ToString();
                Bus b = new Bus(license, RandomDay(license, rnd));
                listOfBuses.AddBus(b);

            }
            foreach (Bus item in listOfBuses)
            {
                item.T = rnd.Next(0, 1200);//randomly chooses the distance in each bus
                item.TT += item.T;
            }

            InitializeComponent();

            lbBuses.ItemsSource = listOfBuses;//sets listBoxes source as listOfBuses
            lbBuses.DataContext = listOfBuses;//displays buses in listofbuses
            lbBuses.DataContextChanged += new DependencyPropertyChangedEventHandler(lbBuses_Data_Change);
        }
        /// <summary>
        /// this method returns a random datetime between 1980 to today
        /// </summary>
        /// <param name="license"></param> the license that was randomly generated
        /// <returns></returns>
        DateTime RandomDay(string license, Random rnd)
        {
            DateTime start;
            int range;
            if (int.Parse(license) >= 10000000)//checks if recieved 8 digit license
            {
                start = new DateTime(2018, 1, 1);//start date is from 2018
                range = (DateTime.Today - start).Days;//calculates amount of days ranging from 2018 untill today
            }
            else// if recieved 7 digit license 
            {
                start = new DateTime(1980, 1, 1);//start date is from 1980
                range = (new DateTime(2017, 12, 31) - start).Days;//calculates amount of dates ranging from 1980 2018
            }
            return start.AddDays(rnd.Next(range));
        }
        #region***methods_on_bus***
        /// <summary>
        /// refuels bus
        /// </summary>
        /// <param name="myBus"></param>bus sent by the event
        public void refuel(Bus myBus)//refuels bus
        {
            BackgroundWorker worker = new BackgroundWorker();//initialize background worker for refueling
            worker.DoWork += WorkerF_DoWork;
            worker.ProgressChanged += WorkerF_ProgressChanged;//adds refuel workerprogress update event
            worker.RunWorkerCompleted += WorkerF_RunWorkerCompleted;//adds refuel workercompletion event
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(argument: myBus);//sends bus for refueling

        }
        /// <summary>
        /// maintains bus
        /// </summary>
        /// <param name="myBus"></param>bus sent by the event
        public void maintain(Bus myBus)//maintan bus
        {
            BackgroundWorker worker = new BackgroundWorker(); //initialize background  worker for maintenance
            worker.DoWork += WorkerM_DoWork;
            worker.ProgressChanged += WorkerM_ProgressChanged;
            worker.RunWorkerCompleted += WorkerM_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(argument: myBus);//sends bus for maintenance
        }
        /// <summary>
        /// takes the bus on a trip
        /// </summary>
        /// <param name="myBus"></param>bus sent by the event
        public void travel(Bus myBus)
        {
            BackgroundWorker worker = new BackgroundWorker();//initialize background worker for Travel
            worker.DoWork += WorkerT_DoWork;
            worker.ProgressChanged += WorkerT_ProgressChanged;
            worker.RunWorkerCompleted += WorkerT_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(argument: myBus);//sends bus for travel
        }
#endregion
        #region***Evenets_on_main_window***
        /// <summary>
        /// adds a bus to the listbox displaying the buses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_add_click(object sender, RoutedEventArgs e)
        {
            Window2 window2 = new Window2();
            window2.ShowDialog();//displays window2
            lbBuses.Items.Refresh();
        }
        /// <summary>
        /// sends the chosen bus for refueling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_refuel_click(object sender, RoutedEventArgs e)
        {
            Bus bus = ((sender as Button).DataContext as Bus);//casts button pressed on as a bus
            refuel(bus);
            lbBuses.Items.Refresh();
        }
        /// <summary>
        /// Chooses a bus to make a trip, opens a new window to allow the user to enter the travel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_travel_click(object sender, RoutedEventArgs e)
        {
            Bus bus = ((sender as Button).DataContext as Bus);//casts object clicked on as a bus
            Window3 window3 = new Window3(bus);//opens window3 with the current buses data
            window3.ShowDialog();//displays window3
           if(bus.KM>0)//if a distance for traveling was entered
            travel(bus);
            lbBuses.Items.Refresh();
        }

        /// <summary>
        /// opens a new window that displays details of chosen bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlerOnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Bus bus = (Bus)lbBuses.SelectedItem;//casts object clicked on as a bus
            Window2 window2 = new Window2(bus);//opens window2 with the current buses data
            window2.ShowDialog();//displays window2
            if (bus.choice == "Refuel")//if refuel was chosen in windowBusDetails
            {
                bus.choice = "";//resets choice to an empty string
                refuel(bus); 
            }
            if (bus.choice == "Maintain")//if maintain was chosen in windowBusDetails
            {
                bus.choice = "";//resets choice to an empty string
                maintain(bus); }
            lbBuses.Items.Refresh();
        }

        private void lbBuses_Data_Change(object sender, DependencyPropertyChangedEventArgs e)
        {
            lbBuses.DataContext = lbBuses;
        }
        /// <summary>
        /// not used in program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ////
        }
        #endregion
        #region***Events_Travel_background_Worker***

        private void WorkerT_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus bus = (e.Argument as Bus);//casts the object into the bus variable
            try
            {
                if (bus.checkTrip())//checks if distance requested is valid and sends on a trip
                {
                    bus.ST = status.MIDOFRIDE;//sends bus on trip
                    for (int i = 1; i < 100; i++)
                    {
                        Thread.Sleep((bus.KM * 450)/100);//A bus set for travel is in use for 45kmh*distance traveling
                        (sender as BackgroundWorker).ReportProgress(i, (object)bus);//send the amount of progress to update ,and current bus
                    }
                    bus.T += bus.KM;//adds distance traveled
                    bus.TT += bus.KM;//adds distance traveled to total distance
                    e.Result = bus;//sends the uptated bus to the completed stage of the background worker
                }
            }
            catch (ExceptionCantGoOnTrip ex)
            {
                MessageBox.Show(ex.Message);//shows exception in a messageBox
                e.Result = bus;
            }

        }

        /// <summary>
        /// BackgroundWorker event handeler for Travel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerT_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;//gets amount time progressed
            Bus bus = (e.UserState as Bus);
            bus.progress = progress;//resets progress in  progress bar

        }
        /// <summary>
        /// BackgroundWorker event handeler for Travel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerT_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //(sender as TextBlock).Visibility = Visibility.Hidden;//hides timer from view
            if (e.Error != null)// handles the case where an exception was thrown.
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                (e.Result as Bus).updateStatus();//updates status of bus
                (e.Result as Bus).KM=0;//resets distance for travel to 0
                lbBuses.Items.Refresh();
            }
        }
        #endregion
        #region***Event_Refuel_background_Worker***
        /// <summary>
        /// BackgroundWorker event handeler for refueling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerF_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus bus = (e.Argument as Bus);//casts object clicked on as a bus
            bus.ST = status.REFUELING;//resets status of bus to refueling
            for (int i=1; i < 100; i++)
            {
                Thread.Sleep(1200);//bus set for refueling will wait for a total of "2 hours"
                (sender as BackgroundWorker).ReportProgress(i, (object)bus);//send the amount of progress to update ,and current bus
            } 
            bus.T = 0;//sets travel to 0
            e.Result = (bus);
        }

        /// <summary>
        /// BackgroundWorker event handeler for refueling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerF_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int progress = e.ProgressPercentage;//gets amount time progressed
            Bus bus = (e.UserState as Bus);
            bus.progress=progress;//resets progress in  bar
        }
        /// <summary>
        /// BackgroundWorker event handeler for refueling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)// handles the case where an exception was thrown.
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                (e.Result as Bus).updateStatus();//updates status of bus
                lbBuses.Items.Refresh(); //  (e.Result as Bus).Progress = 0;//sets progress bar to
            }
        }
        #endregion
       #region***Events_Maintanence_background_worker***
        private void WorkerM_DoWork(object sender, DoWorkEventArgs e)
        {
            Bus bus = (e.Argument as Bus);//casts object clicked on as a bus
            bus.ST = status.BEINGSERV;//changes bus status to being serviced
            for (int i = 0; i < 14400; i++)
            {
                Thread.Sleep(100);//bus being serviced is delayed for "24 hours of real time"
                (sender as BackgroundWorker).ReportProgress(i/100, (object)bus);//send the amount of progress to update ,and current bus
            }
            e.Result = bus;

        }
        /// <summary>
        /// BackgroundWorker event handeler for Maintenance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerM_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            double progress = e.ProgressPercentage;//gets amount time progressed
            Bus bus = (e.UserState as Bus);
            bus.progress = progress;//resets progress in  progress bar
        }
        /// <summary>
        /// BackgroundWorker event handeler for maintenance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerM_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)// handles the case where an exception was thrown.
            {
                MessageBox.Show(e.Error.Message);
            }
            else
            {
                (e.Result as Bus).D = DateTime.Now;//updates buses last maintenance date to today
               
                (e.Result as Bus).updateStatus();//updates status of bus
                lbBuses.Items.Refresh();
            }
        }
        #endregion
    }
}


