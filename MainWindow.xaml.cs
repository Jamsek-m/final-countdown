using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace FinalCountdown {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
			vhod_ura.Focusable = true;
			Keyboard.Focus(vhod_ura);
		}

		public MainWindow(TimeSpan input) {
			InitializeComponent();


			if (input != null) {
				vhod_ura.Text = input.Hours.ToString();
				vhod_minuta.Text = input.Minutes.ToString();
				vhod_sekunda.Text = input.Seconds.ToString();
			}

		}

		private void gumb_calc_Click(object sender, RoutedEventArgs e) {
			TimeCalc tc = new TimeCalc();
			tc.Show();
			this.Close();
		}

		private void vhod_ura_PreviewTextInput(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
