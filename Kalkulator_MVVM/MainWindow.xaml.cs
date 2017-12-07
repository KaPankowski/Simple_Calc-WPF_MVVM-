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

namespace Kalkulator_MVVM
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void btn_1_Click(object sender, RoutedEventArgs e)
		{
			txtboxResult.Text += ((Button)sender).Content.ToString();
		}

		private void txtboxResult_PreviewTextInput(object sender, TextCompositionEventArgs e)
		{
			e.Handled = isNumeric(e.Text);
		}

		private static bool isNumeric(string text)
		{
			
			Regex reg= reg = new Regex("[^0-9^+^*^/^-^,]");
			
			
			return reg.IsMatch(text);
		}

		
	}
}
