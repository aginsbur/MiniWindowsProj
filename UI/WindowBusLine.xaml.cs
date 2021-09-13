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
    /// Interaction logic for WindowBusLine.xaml
    /// </summary>
    public partial class WindowBusLine : Window
    {
        IBL bl;
        BO.BusLine curBL;
        public WindowBusLine(IBL _bl)
        {
            bl = _bl;
            InitializeComponent();
            cbBusLines.DisplayMemberPath = "ID";//show only specific Property of object
            cbBusLines.SelectedValuePath = "ID";//selection return only specific Property of object
            cbBusLines.SelectedIndex = -1;
            DGStopsInLine.SelectedIndex = -1;
            buttonUpdate.IsEnabled = false;
            refreshBusLineComboBox();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource busLineViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("busLineViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            // busLineViewSource.Source = [generic data source]
        }
        #region***refresh***
        /// <summary>
        /// resets list box of current lines bus stops
        /// </summary>
        void refreshLBbusStops()
        {
            curBL.StopsInLine = bl.getBusStopsInLine(curBL).ToList().OrderBy(x => x.IndexInLine);
            DGStopsInLine.ItemsSource = curBL.StopsInLine;
            DGStopsInLine.DataContext = curBL.StopsInLine.ToList();
        }
       /// <summary>
       /// resets bus line selection in combo box
       /// </summary>
       void refreshBusLineComboBox()
        {
            cbBusLines.DataContext = bl.getAllBusLines().ToList().OrderBy(x => x.ID);//ObserListOfBusLines, sorted in order od iD
            cbBusLines.SelectedIndex = -1; //index of the object to be selected initially

        }
       /// <summary>
       /// resets the bus stops passing through the bus line
       /// </summary>
       /// <param name="myBusLine"></param>
       void refreshBusStopsInLine(BO.BusLine myBusLine)
        {
            myBusLine.StopsInLine = bl.getBusStopsInLine(curBL);
        }
     /// <summary>
     /// resets bus stops selection  that can be added to the bus line
     /// </summary>
     /// <param name="myBusLine"></param>busline that is being added to
       void refreshBusStopSelection(BO.BusLine myBusLine)
        {
            lbBusStopSelection.DataContext = bl.getBusStopsNotInLine(myBusLine).OrderBy(x=>x.StationCode);
        }
      /// <summary>
      /// resets the display of a bus line
      /// </summary>
      void refreshGridBusLine()
        {
            gridBusLine.DataContext = curBL;
        }
        /// <summary>
        /// clears the current line being displayed
        /// </summary>
        void ClearBusLineData()
        {
            gridBusLine.DataContext = -1;//clear the bus lines info
            DGStopsInLine.ItemsSource = null;//clear the bus stops info
            cbBusLines.SelectedIndex = -1;//clear combo box selection
            txtBiD.Clear();
            txtBbegStopNum.Clear();
            txtBlineNumber.Clear();
            txtBendStopNum.Clear();

        }
        #endregion***refresh***
        #region***Enable/Disable***
        /// <summary>
        /// enables or disables the Add, ok and cancel button
        /// </summary>
        void enableDisableOKCancel()
        {
            if (buttonOK.Visibility == Visibility.Visible)//if button ok is visible
            {
                buttonAdd.Visibility = Visibility.Visible;//enable add button for adding a bus
                buttonUpdate.Visibility = Visibility.Visible;
                buttonDelete.Visibility = Visibility.Visible;
                buttonOK.Visibility = Visibility.Collapsed;
                buttonCancel.Visibility = Visibility.Collapsed;
                cbBusLines.IsEnabled = true;
            }
            else//if button ok is not visible
            {
                buttonAdd.Visibility = Visibility.Collapsed;//disable add button while adding a bus
                buttonUpdate.Visibility = Visibility.Collapsed;
                buttonDelete.Visibility = Visibility.Collapsed;
                buttonOK.Visibility = Visibility.Visible;
                buttonCancel.Visibility = Visibility.Visible;
                cbBusLines.IsEnabled = false;
            }
        }
        /// <summary>
        /// enable or disables the finish button
        /// </summary>
        void enableDisableFinish()
        {
            if (buttonFinish.Visibility == Visibility.Visible)//if button finish is visible
            {
                buttonAdd.Visibility = Visibility.Visible;//enable add button for adding a bus
                buttonUpdate.Visibility = Visibility.Visible;
                buttonDelete.Visibility = Visibility.Visible;
                buttonFinish.Visibility = Visibility.Collapsed;
                cbBusLines.IsEnabled = true;
            }
            else//if button finish is not visible
            {
                buttonAdd.Visibility = Visibility.Collapsed;//disable add button while adding a bus
                buttonUpdate.Visibility = Visibility.Collapsed;
                buttonDelete.Visibility = Visibility.Collapsed;
                buttonFinish.Visibility = Visibility.Visible;
                cbBusLines.IsEnabled = false;
            }
        }
        /// <summary>
        /// enables or disables user from inputing data about a bus line in the grid
        /// </summary>
        /// <param name="choice"></param>choose 1 to disable, 2 to enable
        void enableDisableGridBusLine(int choice)
        {
            if (choice == 1)// 1=disable 
            {
                txtBlineNumber.IsEnabled = false;
                cbRegion.IsEnabled = false;
            }
            else//2=enable
            {
                txtBlineNumber.IsEnabled = true;
            }


        }
        /// <summary>
        /// changes the Region display from a combo box to text box
        /// </summary>
        /// <param name="choice"></param>1=text box view, 2= combo box view
        void comboBoxOrTextBoxRegion(int choice)
        {
            if (choice == 1)// text box view of region
            {
                cbRegion.Visibility = Visibility.Collapsed;
            }
            else//combo box view of region
            {
                cbRegion.Visibility = Visibility.Visible;
                cbRegion.IsEnabled = true;
                cbRegion.DataContext = bl.getAllRegions().ToList();
                cbRegion.SelectedIndex = -1;
            }
        }
        /// <summary>
        /// changes the begin and end stop display from a combo box to text box
        /// </summary>
        /// <param name="choice"></param>1=text box view, 2=combo box view
        void comboBoxOrTextBoxBegOrEndStop(int choice)
        {
            if (choice == 1)// text box view of the begin and end stop
            {
                cbBegStop.Visibility = Visibility.Collapsed;
                cbEndStop.Visibility = Visibility.Collapsed;
            }
            else// combo box view of the begin and end stop
            {
                cbBegStop.Visibility = Visibility.Visible;
                cbEndStop.Visibility = Visibility.Visible;
                cbBegStop.IsEnabled = true;
                cbEndStop.IsEnabled = true;
                cbBegStop.DataContext = bl.getAllBusStops().ToList().OrderBy(x=>x.StationCode);
                cbEndStop.DataContext = bl.getAllBusStops().ToList().OrderBy(x => x.StationCode);
                cbBegStop.SelectedIndex = -1;
                cbEndStop.SelectedIndex = -1;
            }
        }
        #endregion***Enable/Disable***
        #region ***events***
        /// <summary>
        /// event on selecting a bus line from the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbBusLines_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbBusLines.SelectedIndex != -1)
            {
                curBL = (cbBusLines.SelectedItem as BO.BusLine);
                comboBoxOrTextBoxRegion(1);//display the region of the bus line
                refreshBusStopsInLine(curBL);
                gridBusLine.DataContext = curBL;//sets the grids data to the selected bus stop
                buttonUpdate.IsEnabled = true;
                buttonDelete.IsEnabled = true;
                if (curBL != null)
                {
                    refreshLBbusStops();// get list of bus stops of selected bus Line
                }
            }
            else
            {
                buttonDelete.IsEnabled = false;
                buttonUpdate.IsEnabled = false;
            }
        }

        /// <summary>
        /// event on adding a new bus line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            enableDisableOKCancel();//enable ok and cancel button
            enableDisableGridBusLine(2);//enable user to enter infromation abt bus line
            buttonUpdate.IsEnabled = false;//disable update button while adding a bus
            ClearBusLineData();//clear the bus stop that is being displayed
            comboBoxOrTextBoxRegion(2);//open the region combo box for the user 
            comboBoxOrTextBoxBegOrEndStop(2);
        }
        /// <summary>
        ///if the ok was pressed after adding a new bus line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="i"></param>
        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                curBL = new BO.BusLine
                {
                    LineNumber = Convert.ToInt32(txtBlineNumber.Text),
                    Region = (BO.Region)Enum.Parse(typeof(BO.Region), cbRegion.SelectedItem.ToString()),//get selected region
                    BegStopNum = (cbBegStop.SelectedItem as BO.BusStop).StationCode,
                    EndStopNum = (cbEndStop.SelectedItem as BO.BusStop).StationCode,//Convert.ToInt32(cbEndStop.SelectedItem.ToString()),
                };
                bl.addBusLine(curBL);
                curBL = bl.getBusLinewithOutID(curBL).ToList().ElementAt(0);//resets the current bus line with it's ID
                bl.addLineStop(curBL.ID, curBL.BegStopNum, 1);//add first bus stop to the line
                bl.addLineStop(curBL.ID, curBL.EndStopNum, 2);//add last bus stop to the line
                refreshBusStopSelection(curBL);//list of bus stop will not contain already chosen stops
                enableDisableGridBusLine(1);
                enableDisableOKCancel();//disable ok button for adding a bus line
                comboBoxOrTextBoxBegOrEndStop(1);//close the combo box for the region
                comboBoxOrTextBoxBegOrEndStop(1);//close the combo box for the begin and end stop
                enableDisableFinish();//enable finish button fo selecting bu stops for line
                refreshGridBusLine();//display the bus line in the grid
                refreshLBbusStops();//display the first and last bus stops in the line
                lbBusStopSelection.Visibility = Visibility.Visible;//enable user to select bus stops
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter a valid number for the new bus line");
            }
            catch (NullReferenceException)
            {
                string error = "";
                if (cbRegion.SelectedIndex == -1)//if a region was not selected
                    error += "Please select a Region for the new bus line\n";
                if (cbBegStop.SelectedIndex == -1)//if a region was not selected
                    error += ("Please select the first bus stop for the new bus line\n");
                if (cbEndStop.SelectedIndex == -1)//if a region was not selected
                    error += "Please select a destination for the new bus line";
                MessageBox.Show(error);
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (BO.BadBusStopIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// erases the data that the user inputed about a busLine
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel1_click(object sender, RoutedEventArgs e)
        {
            enableDisableOKCancel();
            enableDisableGridBusLine(1);
            ClearBusLineData();//clear the bus stop that is being displayed
            comboBoxOrTextBoxBegOrEndStop(1);
            comboBoxOrTextBoxRegion(1);

        }
        /// <summary>
        /// event handeler after user finishes selecting bus stops for the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFinish_click(object sender, RoutedEventArgs e)
        {   addAdjStop(curBL.StopsInLine.ElementAt(curBL.StopsInLine.Count()-2), curBL.StopsInLine.Last());//creates adjacent stop for last bus stop
            lbBusStopSelection.Visibility = Visibility.Collapsed;//close the list box display of the bus stops
            ClearBusLineData();//clears display of the current bus line
            comboBoxOrTextBoxBegOrEndStop(1);
            comboBoxOrTextBoxRegion(1);
            refreshBusLineComboBox();//refresh bus line combo box inorder to display added bus line
            enableDisableFinish();
            cbBusLines.IsEnabled = true;
        }
        /// <summary>
        /// event for selecting a bus stop to add to the bus line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxItemSelected(Object sender, RoutedEventArgs e)
        {
            ListBoxItem item = e.Source as ListBoxItem;
            // gets the previos bus stop in the line
            try
            {
                BO.BusStop prevStop = curBL.StopsInLine.ToList().ElementAt(curBL.StopsInLine.ToList().Count() - 2);
                BO.BusStop currStop = (BO.BusStop)(item.Content);
                bl.updateLineStop(new BO.LineStop { LineID = curBL.ID, StationCode = curBL.StopsInLine.Last().StationCode, StationIndex = curBL.StopsInLine.Count() + 1, });
                bl.addLineStop(curBL.ID, currStop.StationCode, curBL.StopsInLine.ToList().Count());//add a new line stop
                refreshBusStopSelection(curBL);
                refreshLBbusStops();
                addAdjStop(prevStop, currStop);
            }
            catch (BO.BadBusStopIdException)
            {
                MessageBox.Show("this is already a bus stop in the line");//or an adjacent stop
            }

        }
        private void buttonUpdate_click(object sender, RoutedEventArgs e)
        {
            enableDisableFinish();//enable the finish Button
            lbBusStopSelection.Visibility = Visibility.Visible;
            refreshBusStopSelection(gridBusLine.DataContext as BO.BusLine);
            cbRegion.SelectedItem = curBL.Region.ToString();//sets the selected region to the current bus lines region
            buttonUpdate.IsEnabled = false;
        }

        private void buttonDelete_click(object sender, RoutedEventArgs e)
        {
            deleteBusLine(gridBusLine.DataContext as BO.BusLine);
            refreshBusLineComboBox();
            gridBusLine.DataContext = -1;
            DGStopsInLine.ItemsSource = null;
        }

        /// <summary>
        /// event will delete the selected bus stop from the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGStopsInLine_selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGStopsInLine.SelectedIndex != -1)
            {
                try
                {
                    BO.BusStop myBusStop = (BO.BusStop)DGStopsInLine.SelectedItem;//gets the selected bus stop from the data grid
                    bl.deleteBusStopFromLine(myBusStop, int.Parse(txtBiD.Text));
                    curBL = bl.getBusLine(curBL.ID);
                    refreshGridBusLine();
                    refreshBusStopSelection(curBL);
                    refreshLBbusStops();//refresh view of the bus stops in the line
                    refreshGridBusLine();
                }
                catch (BO.BadBusLineToDelException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (BO.BadBusStopIdException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion***Events***
        #region***Methods***
        void deleteBusLine(BO.BusLine myBusLine)
        {
            try
            {
                bl.deleteBusLine(myBusLine);
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        /// <summary>
        /// adds an adjacent stop to the data source
        /// </summary>
        /// <param name="prevStop"></param>
        /// <param name="currStop"></param>
        void addAdjStop(BO.BusStop prevStop, BO.BusStop currStop)
        {
            try
            {
                //creates a new adjacent stop
                BO.AdjacentStops adjStop = bl.getAdjacentStops(prevStop.StationCode, currStop.StationCode);
                if (adjStop != null)//if the adjacent stop already exists, continue
                { //continue
                }
                else//if the adjacent stop does not already exist, allow user to enter distance
                {

                    adjStop = new BO.AdjacentStops();
                    new WindowDistance(adjStop, "Enter the distance from the previous bus stop: ").ShowDialog();
                    adjStop.StopCode1 = prevStop.StationCode;
                    adjStop.StopCode2 = currStop.StationCode;
                    adjStop.AvgTravelTime = TimeSpan.FromHours(adjStop.Distance/55);
                    bl.addAdjacentStops(adjStop);
                }
            }
            catch (BO.BadBusLineIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion***Methods***

  
    }
}

