using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
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
			System.Windows.MessageBox.Show("Timeout over!", "End!");
			if (this.WindowState == WindowState.Minimized) {
				this.Show();
				this.WindowState = WindowState.Normal;
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

		private void gumb_start_Click(object sender, RoutedEventArgs e) {
			if (string.IsNullOrEmpty(vhod_ura.Text)) {
				vhod_ura.Text = "0";
			}
			if (string.IsNullOrEmpty(vhod_minuta.Text)) {
				vhod_minuta.Text = "0";
			}
			if (string.IsNullOrEmpty(vhod_sekunda.Text)) {
				vhod_sekunda.Text = "0";
			}

			int H = Int32.Parse(vhod_ura.Text);
			int m = Int32.Parse(vhod_minuta.Text);
			int s = Int32.Parse(vhod_sekunda.Text);
			this.time = new TimeSpan(H, m, s);


			timer.Start();
		}

		private void gumb_stop_Click(object sender, RoutedEventArgs e) {
			timer.Stop();
		}

		private void notIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e) {
			this.Show();
			this.WindowState = WindowState.Normal;
		}

		private void Final_Countdown_StateChanged(object sender, EventArgs e) {
			if (this.WindowState == WindowState.Minimized) {
				this.Hide();
			}
		}

		private void Final_Countdown_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
			if (e.Key == Key.Enter) {
				gumb_start_Click(sender, e);
			}
		}
	}
}
