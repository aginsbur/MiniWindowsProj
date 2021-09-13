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
using System.ComponentModel;

namespace dotNet5781_03B_7128_3442
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private Bus currentBus=new Bus();
      /// <summary>
      /// c-tor for adding a bus window
      /// </summary>
      public Window2()
        {
            InitializeComponent();
            text_box_license.KeyDown += new KeyEventHandler(eventKeyDown_License);
            text_box_start_date.KeyDown += new KeyEventHandler(eventKeyDown_Date);
            Button_refuel.Visibility = Visibility.Collapsed;//makes button invisible to user
            Button_treatment.Visibility = Visibility.Collapsed;//makes button invisible to user
            text_box_license.IsEnabled = true;//enables data change of bus being displayed
        }
      /// <summary>
      /// c-tor for viewing a bus
      /// </summary>
      /// <param name="passedBus"></param>bus whos data will be displayed on the window
      public Window2(Bus passedBus)
        {
            InitializeComponent();
            Button_refuel.Click += new RoutedEventHandler(Button_refuel_Click);
            Button_treatment.Click += new RoutedEventHandler(Button_treatment_Click);
            Button_refuel.Visibility = Visibility.Visible;//makes button visible to user
            Button_treatment.Visibility = Visibility.Visible;//makes button visible to user
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
        /// checks keys entered into the date text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventKeyDown_License(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)//checks if the enter key was pressed
            {
                try { 
                currentBus.L=text_box_license.Text;//inserts license number into a temporary bus
                text_box_license.IsEnabled = false;//disables license text box
                text_box_start_date.IsEnabled = true;//enables date text box
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show(ex.Message);//show exception in a window
                }
                e.Handled = true;
            }
        }

        /// <summary>
        /// event handler for keys entered into the date text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void eventKeyDown_Date(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)//cheks if the enter key was pressed
            {
                if (DateTime.TryParse(text_box_start_date.Text, out DateTime result))//if the date entered is valid
                { currentBus.SD = result;
                    AddBus();
                }
                else
                    MessageBox.Show("Invalid date entered!");// shows exception in message box
                e.Handled = true;
            }
        }
       /// <summary>
       /// sends bus to refuel
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void Button_refuel_Click(object sender, RoutedEventArgs e)
        {
            currentBus.choice = "Refuel";// saves the event name in the main window
            this.Close();//closes window
            e.Handled = true;
        }

        /// <summary>
        /// sends bus for maintenance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_treatment_Click(object sender, RoutedEventArgs e)
        {

           currentBus.choice = "Maintain";//saves the event name in the main window
            this.Close();//closes window
            e.Handled = true;

        }
       /// <summary>
       /// not used in the program
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void text_box_km_since_last_refuel_TextChanged(object sender, TextChangedEventArgs e)
        {
            ////
        }

        private void text_box_date_from_maintenence_TextChanged(object sender, TextChangedEventArgs e)
        {
            ////
        }
        #endregion
        #region***methods***
        public void AddBus()//inserts a valid bus to list
        {
            text_box_start_date.IsEnabled = false;//disables date text box
            try
            {
                MainWindow.listOfBuses.AddBus(currentBus);//attempts to add new bus to the list
                this.Close();
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message);//shows exception in message box 
                text_box_license.Text=" ";
                text_box_start_date.Text=" ";
                text_box_license.IsEnabled = true;//enables license text box
            }
            
        }
        #endregion

       

        
    }

}
