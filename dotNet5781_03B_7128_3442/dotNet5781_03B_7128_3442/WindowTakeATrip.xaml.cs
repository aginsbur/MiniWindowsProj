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
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Threading;

namespace dotNet5781_03B_7128_3442
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        private Bus currentBus = new Bus();//variable that will hold data of bus that was sent to the window
        public Window3(Bus userBus)
        {
            currentBus = userBus;
            InitializeComponent();
            Text_Box_distance.KeyDown += new KeyEventHandler(Text_Box_distance_key_Down);
            Text_Box_distance.IsEnabled = true;//enable user to enter distance
        }

       /// <summary>
       /// checks which key was pressed on the distance text box 
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
       private void Text_Box_distance_key_Down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)//cheks if the enter key was pressed
            {   currentBus.KM= int.Parse(Text_Box_distance.Text);//distance that user wants to travel
                this.Close();//close window3
                e.Handled = true;
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

        /// <summary>
        /// closes window if the x is pressed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// not used in code
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Text_Box_distance_textChange(object sender, TextChangedEventArgs e)
        {
            ///
        }
    }
}

 
