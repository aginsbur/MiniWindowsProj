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

namespace dotNet5781_03A_7128_3442//using another classes namespace
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //create a collection of busLines
        private BusCompany busList;
        private BusLine currentDisplayBusLine;
        public MainWindow()
        {
            InitializeComponent();
            FillBusList();
            cbBusLines.ItemsSource = busList;
            cbBusLines.DisplayMemberPath = "LN";
            cbBusLines.SelectedIndex = 0;
            ShowBusLine((cbBusLines.SelectedValue as BusLine).LN);
        }
        //method used to fill the list of bus Lines
        /// <summary>
        /// fills busList with 10 busLines and 10 route bus stops
        /// </summary>
        private void FillBusList()
        {
            busList = new BusCompany();
            for (int i = 0; i < 10; i++)//initializes 10 bus lines into a bus company
            {
                try { busList.addLine(new BusLine()); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
            foreach (BusLine item in busList)
            {
                for (int p = 0; p < 3; p++)//inserts ten random bus stops into each bus line
                {
                    try { item.Add(new Route_Bus_Stop()); }
                    catch (Exception ex) { Console.WriteLine(ex.Message); }
                }
            }
        }
        /// <summary>
        /// diplays the bus lines route bus stops
        /// </summary>
        /// <param name="index"></param>index of the busLine in the buslist
        private void ShowBusLine(int index)
        {
            currentDisplayBusLine = busList.indexer(index);//updates the current display to the busline at "index"
            UpGrid.DataContext = currentDisplayBusLine;
            lbBusLineStations.DataContext = currentDisplayBusLine.L;//diplays the selected bus lines data
            tbArea.Text = (currentDisplayBusLine.R.ToString());//displays the new bus lines region

        }

        private void tbArea_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void lbBusLineStations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbBusLineStations_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        /// <summary>
        /// event called when selection in combo box is made by the user
        /// </summary>
        /// <param name="sender"></param>object sent to the method
        /// <param name="e"></param>event argument
        private void cbBusLines_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowBusLine((cbBusLines.SelectedValue as BusLine).LN);//sends selected bus line number
        }
    }
}
