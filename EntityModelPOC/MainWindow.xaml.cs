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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EntityModelPOC.Model;

namespace EntityModelPOC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private EntityModelModel Model { get; set; }

		public MainWindow()
		{
			InitializeComponent();
			Model = TryFindResource("Model") as EntityModelModel;
		}

		private void StackPanel_PreviewMouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
		{
			Model.LoadSwitch();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
