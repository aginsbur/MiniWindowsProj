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

namespace UI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class WindowDistance : Window
    {
        BO.AdjacentStops adjStop;
        BO.Bus bus;
        #region***c-tors***
        /// <summary>
        /// c-tor for entering distance between two stops
        /// </summary>
        /// <param name="myAdjStop"></param>
        public WindowDistance(BO.AdjacentStops myAdjStop,string text)
        {
            InitializeComponent();
            textBlockDistance.Text = text;
            adjStop = myAdjStop;
        }
        /// <summary>
        /// c-tor for entering a busses travel distance
        /// </summary>
        /// <param name="myBus"></param>
        public WindowDistance(BO.Bus myBus, string text)
        {
            InitializeComponent();
            textBlockDistance.Text = text;
            bus = myBus;
        }
        #endregion
        #region ***Events***
        /// <summary>
        /// will only allow user to enter numbers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBoxdistance_keyDown(object sender, KeyEventArgs e)
        {  if(e.Key==Key.Enter)
            {   if (adjStop != null)
                    adjStop.Distance = int.Parse(textBoxDistance.Text);//save the inputed distance
                else
                bus.KM = double.Parse(textBoxDistance.Text);
                this.Close();//close the window
            }
        else
            e.Handled = !IsNumberKey(e.Key) && !IsActionKey(e.Key);//if the key entered is not a digit between 0-9, e.handeled will be true, terminating the event
        }
        /// <summary>
        /// returns true if key set is a number key
        /// </summary>
        /// <param name="inKey"></param>the key pressed by the user
        /// <returns></returns>
        private bool IsNumberKey(Key inKey)
        {
            if (inKey < Key.D0 || inKey > Key.D9)//if it's not a number key
            {
                if (inKey < Key.NumPad0 || inKey > Key.NumPad9)//if it's not a numPad key
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// returns true if key entered is an action key
        /// </summary>
        /// <param name="inKey"></param>the key pressed by the user
        /// <returns></returns>
        private bool IsActionKey(Key inKey)
        {
            return inKey == Key.Delete || inKey == Key.Back || inKey == Key.Tab || inKey == Key.Return || Keyboard.Modifiers.HasFlag(ModifierKeys.Alt);
        }
        #endregion
    }
}
