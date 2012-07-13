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
using System.Windows.Media.Animation;
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

		private void EntityResource_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			// slide in the addresses.
			slide(this.Resources, this.Addresses, Direction.Right);
		}

		private enum Direction { Left, Right }
		private void slide(Border oldVisual, Border newVisual, Direction direction)
		{
			var width = this.ActualWidth;
			var animOut = new ThicknessAnimation(new Thickness(0), new Thickness(direction == Direction.Right ? -width : width, 0, direction == Direction.Right ? width : 0, 0), new Duration(TimeSpan.FromMilliseconds(500)));
			var animIn = new ThicknessAnimation(new Thickness(direction == Direction.Right ? width : -width, 0, direction == Direction.Right ? 0 : width, 0), new Thickness(0), new Duration(TimeSpan.FromMilliseconds(500)));

			Storyboard.SetTarget(animOut, oldVisual);
			Storyboard.SetTargetProperty(animOut, new PropertyPath(MarginProperty));

			Storyboard.SetTarget(animIn, newVisual);
			Storyboard.SetTargetProperty(animIn, new PropertyPath(MarginProperty));

			var storyboard = new Storyboard();
			storyboard.Children.Add(animOut);
			storyboard.Children.Add(animIn);
			storyboard.Freeze();
			BeginStoryboard(storyboard);
		}

		private void Addresses_GoBack(object sender, RoutedEventArgs routedEventArgs)
		{
			slide(this.Addresses, this.Resources, Direction.Left);
		}
	}
}
