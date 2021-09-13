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
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using BL;
namespace UI
{
    /// <summary>
    /// Interaction logic for WindowBus.xaml
    /// </summary>
    public partial class WindowBus : Window
    {
        IBL bl;
        public WindowBus(IBL _bl)
        {
            bl = _bl;
            InitializeComponent();
            refreshLbBusses();
        }
        #region***Events***
        /// <summary>
        /// adds a bus to the listbox displaying the buses
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_add_click(object sender, RoutedEventArgs e)
        {
            bl.AddBus();
            refreshLbBusses();
        }
        /// <summary>
        /// opens a new window that displays details of chosen bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lbBuses_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.Bus bus = (BO.Bus)lbBuses.SelectedItem;//casts object clicked on as a bus
            WindowBusDetails windowBusDet = new WindowBusDetails(bus,bl);
            windowBusDet.ShowDialog();//displays bus details in a new window
            refreshLbBusses();
        }
        /// <summary>
        /// Chooses a bus to make a trip, opens a new window to allow the user to enter the travel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_travel_click(object sender, RoutedEventArgs e)
        {
            BO.Bus bus = ((sender as Button).DataContext as BO.Bus);//casts object clicked on as a bus
            WindowDistance windowd = new WindowDistance(bus,"Enter distance to travel in KM: ");//opens windowd with the current buses data
            windowd.ShowDialog();//displays windowd
            if (bus.KM > 0)//if a distance for traveling was entered
                 travel(bus);
            refreshLbBusses();
        }
        #endregion***Events***
      
        #region***Refresh***
        void refreshLbBusses()
        {   
            lbBuses.ItemsSource = bl.getAllBusses().ToList().OrderBy(x=>x.LicenseNum);//sets listBoxes source as listOfBuses
        }
        #endregion***Refresh***

        #region***Methods***
        /// <summary>
        /// takes the bus on a trip
        /// </summary>
        /// <param name="myBus"></param>bus sent by the event
        public void travel(BO.Bus bus)
        {
            try
            { 
            bl.UpdateBus(bus);//checks if distance requested is valid and sends on a trip 
            }
            catch(BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion
    }
}
