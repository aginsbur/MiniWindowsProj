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
	/// Interaction logic for UserTrip.xaml
	/// </summary>
	public partial class UserTrip : Window
	{
		IBL bl;
		public UserTrip(IBL ibl)
		{
			InitializeComponent();
			bl = ibl;
			bustopbeg.DisplayMemberPath = "StationCode";
			bustopend.DisplayMemberPath = "StationCode";
			bustopbeg.SelectedValue = "StationCode";
			bustopend.SelectedValue = "StationCode";
			bustopbeg.ItemsSource = bl.getAllBusStops();
			bustopend.ItemsSource = bl.getAllBusStops();
		}

		private void buttonSelect_click(object sender, RoutedEventArgs e)
		{
			// if he selected 2 stops
			if ((bustopbeg.SelectedIndex != -1) && (bustopend.SelectedIndex != -1))
			{
				buttonTravel.IsEnabled = true;
				buttonCancel.IsEnabled = true;
				buttonSelect.IsEnabled = false;
				lbBuslineSelection.ItemsSource=bl.GetUserTrip(bustopbeg.SelectedItem as BO.BusStop, bustopend.SelectedItem as BO.BusStop);
			}
			else
				MessageBox.Show("please choose 2 bus stops");
		}

		private void buttonTravel_click(object sender, RoutedEventArgs e)
		{
			//checks if he chose if a bus line
			if(lbBuslineSelection.SelectedIndex != -1)
            {
				MessageBox.Show("your travel was successful!");
				this.Close();
			}
			else
				MessageBox.Show("please choose a bus line to travel");
		}

		private void buttonCancel1_click(object sender, RoutedEventArgs e)
		{
			lbBuslineSelection.ItemsSource = null;
			buttonTravel.IsEnabled = false;
			buttonCancel.IsEnabled = false;
			buttonSelect.IsEnabled = true;
		}

	}
}
