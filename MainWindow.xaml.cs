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

using System.Windows.Threading;

namespace FinalCountdown {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		private DispatcherTimer timer;
		private TimeSpan time;

		public MainWindow() {
			InitializeComponent();
			vhod_ura.Focusable = true;
			Keyboard.Focus(vhod_ura);

			timer = new DispatcherTimer();
			timer.Tick += Timer_tick;
			timer.Interval = new TimeSpan(0, 0, 1);

		}

		public MainWindow(TimeSpan input) {
			InitializeComponent();

			if (input != null) {
				vhod_ura.Text = input.Hours.ToString();
				vhod_minuta.Text = input.Minutes.ToString();
				vhod_sekunda.Text = input.Seconds.ToString();
			}

			timer = new DispatcherTimer();
			timer.Tick += Timer_tick;
			timer.Interval = new TimeSpan(0, 0, 1);

		}

		//se izvede vsako sekundo
		private void Timer_tick(object sender, EventArgs e) {
			this.time = this.time.Subtract(new TimeSpan(0, 0, 1));
			vhod_ura.Text = this.time.Hours.ToString();
			vhod_minuta.Text = this.time.Minutes.ToString();
			vhod_sekunda.Text = this.time.Seconds.ToString();
			if (this.time == new TimeSpan(0, 0, 0)) {
				konecCounta();
			}
		}

		private void konecCounta() {
			timer.Stop();
			System.Media.SoundPlayer player = new System.Media.SoundPlayer(@"C:\Windows\Media\Windows Proximity Notification.wav");
			player.Play();
			MessageBox.Show("Timeout over!", "End!");
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

		private void gumb_start_Click(object sender, RoutedEventArgs e) {
			int H = Int32.Parse(vhod_ura.Text);
			int m = Int32.Parse(vhod_minuta.Text);
			int s = Int32.Parse(vhod_sekunda.Text);
			this.time = new TimeSpan(H, m, s);


			timer.Start();
		}

		private void gumb_stop_Click(object sender, RoutedEventArgs e) {
			timer.Stop();
		}
	}
}
