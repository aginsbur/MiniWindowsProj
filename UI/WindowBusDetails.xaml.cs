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
    /// Interaction logic for WindowBusDetails.xaml
    /// </summary>
    public partial class WindowBusDetails : Window
    {
        IBL bl;
        private BO.Bus currentBus = new BO.Bus();
        public WindowBusDetails(BO.Bus passedBus, IBL _bl)
        {
            InitializeComponent();
            bl = _bl;
            Button_refuel.Click += new RoutedEventHandler(Button_refuel_Click);
            Button_treatment.Click += new RoutedEventHandler(Button_treatment_Click);
            Button_refuel.Visibility = Visibility.Visible;//makes button visible to user
            Button_treatment.Visibility = Visibility.Visible;//makes button visible to user
            Button_Delete.Visibility = Visibility.Visible;//makes button visible to user
            text_box_license.IsEnabled = false;//disables data change of bus being displayed
            gBusData.DataContext = passedBus;
            currentBus = passedBus;
        }
        #region***Events***
        private void button_close_click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// sends bus to refuel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_refuel_Click(object sender, RoutedEventArgs e)
        {
            try {
                currentBus.Travel = 0;
                bl.UpdateBus(currentBus);
                //currentBus.choice = "Refuel";// saves the event name in the main window
                this.Close();//closes window
                e.Handled = true;
            }
            catch(BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
            /// <summary>
            /// sends bus for maintenance
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            private void Button_treatment_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                currentBus.TotalTravel = 0;
                currentBus.Travel = 0;
                currentBus.Date = DateTime.Now;
                bl.UpdateBus(currentBus);
                this.Close();//closes window
                e.Handled = true;
            }
            catch (BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.DeleteBus(currentBus.LicenseNum);
                this.Close();//closes window
                e.Handled = true;
            }
            catch (BO.BadBusIdException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


    }
}
