using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace FinalCountdown {
	/// <summary>
	/// Interaction logic for TimeCalc.xaml
	/// </summary>
	public partial class TimeCalc : Window {

		private DateTime trenutni;
		private bool zaprtoZOKGumbom;

		public TimeCalc() {
			InitializeComponent();
			this.zaprtoZOKGumbom = false;
			this.trenutni = DateTime.Now;
			vhod_ura_Copy.Text = trenutni.Hour.ToString();
			vhod_minuta_Copy.Text = trenutni.Minute.ToString();
			vhod_sekunda_Copy.Text = trenutni.Second.ToString();
		}

		private void gumb_calc_Click(object sender, RoutedEventArgs e) {
			if (string.IsNullOrEmpty(vhod_ura.Text) || string.IsNullOrEmpty(vhod_minuta.Text) || string.IsNullOrEmpty(vhod_sekunda.Text)) {
				this.Close();
				return;
			}

			//preveri ce so vsi casi veljavni
			if (!preveriVnos(vhod_ura.Text, vhod_minuta.Text, vhod_sekunda.Text)) {
				MessageBox.Show("Wrong time format!\nInput correct format.", "Error!");
				return;
			}

			string vnesen_cas = vhod_ura.Text + ":" + vhod_minuta.Text + ":" + vhod_sekunda.Text;
			DateTime cas = Convert.ToDateTime(vnesen_cas);

			TimeSpan razlika = cas.Subtract(this.trenutni);

			this.zaprtoZOKGumbom = true;
			MainWindow main = new MainWindow(razlika);
			main.Show();
			this.Close();


		}

		private bool preveriVnos(string ura, string minuta, string sekunda) {
			double H = Double.Parse(ura);
			double m = Double.Parse(minuta);
			double s = Double.Parse(sekunda);

			if (H > 23 || H < 0) {
				return false;
			}
			if (m > 59 || m < 0) {
				return false;
			}
			if (s > 59 || s < 0) {
				return false;
			}
			return true;
		}

		private void Input_time_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
			if (!this.zaprtoZOKGumbom) {
				MainWindow main = new MainWindow();
				main.Show();
				//this.Close();
			}
		}

		private void vhod_ura_PreviewTextInput(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}
	}
}
