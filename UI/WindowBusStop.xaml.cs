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
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for BusStop.xaml
    /// </summary>
    public partial class WindowBusStop : Window
    {
        IBL bl;
        BO.BusStop curBS;
        /// <summary>
        /// window for displaying bus stop info
        /// </summary>
        /// <param name="bl"></param>
        public WindowBusStop(IBL _bl)//IBL bl1
        {
            InitializeComponent();
            bl = _bl;
            cbBusStops.DisplayMemberPath = "StationCode";//show only specific Property of object
            cbBusStops.SelectedValuePath = "StationCode";//selection return only specific Property of object
            cbBusStops.SelectedIndex = -1; //index of the object to be selected initially
            gridBusStop.IsEnabled = false; ;//will disable user from entering data
            buttonUpdate.IsEnabled = false;//enable user to update a bus stop
            RefreshBusStopsComboBox();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busStopViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busStopViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busStopViewSource.Source = [generic data source]

        }
        #region***RefreshDisplay***
        void RefreshBusStopsComboBox()
        {
             cbBusStops.DataContext = bl.getAllBusStops().ToList().OrderBy(x=>x.StationCode);
        }
        /// <summary>
        /// refreshes list of lines passing through current bus stop
        /// </summary>
        ///// <summary>
        ///will enable or disable the user from entering data about a bus stop
        /// </summary>
        void enableDisableBusStopGrid()
        {
            if (gridBusStop.IsEnabled == true)
                gridBusStop.IsEnabled = false;
            else
                gridBusStop.IsEnabled = true;
        }
        void RefreshAllBusLines()
        {
            LBbusLinesUsingStation.ItemsSource = curBS.BusLinesUsingStation;
            LBbusLinesUsingStation.DataContext = curBS.BusLinesUsingStation.ToList();
        }
        /// <summary>
        /// clear the bus stop currently being displayed
        /// </summary>
        void ClearBusStopData()
        {
            //clear the bus stop being displayed
            gridBusStop.DataContext = -1;//clear the bus stops info
            addressTextBox.Clear();
            latitudeTextBox.Clear();
            longitudeTextBox.Clear();
            stationCodeTextBox.Clear();
            LBbusLinesUsingStation.ItemsSource = null;//clear the bus lines info
            cbBusStops.SelectedIndex = -1;
            buttonUpdate.IsEnabled = false;//disable user to update a bus stop
        }

        /// <summary>
        /// will display or hide the ok and cancel button from the user
        /// </summary>
        void enableDisableOKCancel()
        {
            if (buttonOk.Visibility == Visibility.Visible)//if button ok is visible
            {
                buttonAdd.IsEnabled = true;//enable add button for adding a bus
                buttonOk.Visibility = Visibility.Collapsed;
                buttonCancel.Visibility = Visibility.Collapsed;
                cbBusStops.IsEnabled = true;
            }
            else//if button ok is not visible
            {
                buttonAdd.IsEnabled = false;//disable add button while adding a bus
                buttonOk.Visibility = Visibility.Visible;
                buttonCancel.Visibility = Visibility.Visible;
                cbBusStops.IsEnabled = false;
            }
        }
        #endregion***RefreshDisplay***
        #region***Events***
        /// <summary>
        /// updates bus stop that is being displayed in the window  based on the users selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusStops_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbBusStops.SelectedIndex != -1)
            {
                gridBusStop.IsEnabled = true;
                buttonDelete.IsEnabled = true;//enable user to delete a bus stop
                buttonUpdate.IsEnabled = true;//enable user to update a bus stop
                curBS = (cbBusStops.SelectedItem as BO.BusStop);
                curBS.BusLinesUsingStation = bl.getBusLinesInStation(curBS);
                gridBusStop.DataContext = curBS;//sets the grids data to the selected bus stop

                if (curBS != null)
                {
                    //list of bus lines of selected bus stop
                    RefreshAllBusLines();
                }

            }
            else
            {
                buttonDelete.IsEnabled = false;//disable user to delete a bus stop
                buttonUpdate.IsEnabled = false;//disable user to update a bus stop
                gridBusStop.IsEnabled = false;
            }
        }
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            enableDisableOKCancel();
            ClearBusStopData();//clear the bus stop that is being displayed
            gridBusStop.IsEnabled = true;

        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            enableDisableOKCancel();
            ClearBusStopData();//clear the bus stop that is being displayed
            gridBusStop.IsEnabled = false;
            buttonUpdate.IsEnabled = false;//enable user to update a bus stop

        }
        private void buttonOk_click(object sender, RoutedEventArgs e)
        {
            curBS = gridBusStop.DataContext as BO.BusStop;
            try
            {
                curBS = new BO.BusStop
                {
                    StationCode = Convert.ToInt32(stationCodeTextBox.Text),
                    Address = addressTextBox.Text,
                    Longitude = Convert.ToDouble(longitudeTextBox.Text),
                    Latitude = Convert.ToDouble(latitudeTextBox.Text)
                };
                addBusStop(curBS);
            }
            //disable update button until user enters data
            catch (FormatException)
            {
                MessageBox.Show($"Please fill out all fields with valid information!");
            }

        }
        private void buttonUpdate_click(object sender, RoutedEventArgs e)
        {
            curBS = gridBusStop.DataContext as BO.BusStop;//get the previous bus stop
            try
            {
                //create a new bus stop with the updated information
                BO.BusStop newBS = new BO.BusStop
                {
                    StationCode = Convert.ToInt32(stationCodeTextBox.Text),
                    Address = addressTextBox.Text,
                    Longitude = Convert.ToDouble(longitudeTextBox.Text),
                    Latitude = Convert.ToDouble(latitudeTextBox.Text)
                };
                bl.updateBusStop(newBS, curBS);
                RefreshBusStopsComboBox();
                RefreshAllBusLines();
                var lstItems = cbBusStops.Items.Cast<Object>().Select(item => item.ToString()).ToList();//create alist of
                                                                                                        //bus stops in the combo box
                cbBusStops.SelectedIndex = lstItems.IndexOf(newBS.ToString());//sets the combo box to display updated buses number
            }
            catch (FormatException)
            {
                MessageBox.Show($"Please fill out all fields with valid information!");
            }
            catch (BO.BadBusStopIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void buttonDeleteClick(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.deleteBusStop(gridBusStop.DataContext as BO.BusStop);
                RefreshBusStopsComboBox();
                LBbusLinesUsingStation.ItemsSource = null;
                gridBusStop.DataContext = -1;
            }
            catch (BO.BadBusStopIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(BO.BadBusLineToDelException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion***Events***
        #region***Methods***
        /// <summary>
        /// adds a bus to the data source
        /// </summary>
        /// <param name="myBusStop"></param>bus stop that contains data entered by user
        void addBusStop(BO.BusStop myBusStop)
        {
            try
            {
                bl.addBusStop(myBusStop);
                RefreshBusStopsComboBox();
                gridBusStop.IsEnabled = false;//disable user from changing data
                enableDisableOKCancel();//enables user to add another bus
                ClearBusStopData();
            }
            catch (BO.BadBusStopIdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        #endregion***Methods***
    }
}

