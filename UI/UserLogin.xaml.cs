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
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public static IBL bl = BLFactory.GetBL();
        public UserLogin()
        {
            InitializeComponent();
        }

        private void text_box_username_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void text_box_password_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
                string username = text_box_username.Text;
                string password = text_box_password.Text;
                try
                {
                    BO.User u = bl.GetUser(username);

                    if (u.Username == "johnny")//will check if the user is the administrator "johnny"
                    {
                        if (u.Password == password)
                        {
                            MainWindow w = new MainWindow(bl);
                            w.ShowDialog();
                        }
                        else
                            MessageBox.Show("invalid password");
                    }
                    else
                    {
                        if (u.Password == password)
                        {
                            UserTrip w = new UserTrip(bl);
                            w.ShowDialog();
                        }
                        else
                            MessageBox.Show("invalid password");
                    }
                }
                catch (BO.BadUserNameException ex)
                {
                    MessageBox.Show(ex.Message);
                }
        }

        private void Button_Register_Click(object sender, RoutedEventArgs e)
        {
                string username = text_box_username.Text;
                string password = text_box_password.Text;
                try
                {
                    BO.User u = bl.GetAllUserBy(x=>x.Username==username).FirstOrDefault();
                    if (u == null)//will check if the user doesnt exist in the system
                    {
                        MessageBox.Show("you were registered successfully");
                        bl.AddUser(username, password);
                        UserTrip w = new UserTrip(bl);
                        w.ShowDialog();
                    }
                    else
                        MessageBox.Show("user already exists in the system");
                }
                catch (BO.BadUserNameException ex)
                {
                    MessageBox.Show(ex.Message);
                }

    }
    }
}