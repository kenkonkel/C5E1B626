using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using EntityModelPOC.Model;

namespace EntityModelPOC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private EntityModelModel Model { get; set; }
        private Direction _currentDirection =Direction.Left ;
	    private Storyboard _storyBoard;

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
		    _currentDirection = Direction.Right;
			// slide in the addresses.
			slide(this.Resources, this.Addresses, Direction.Right);
		}

		private void EntityAddressDetails_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
            _currentDirection = Direction.Left;
			// slide back, but this would actually select this address.
			slide(this.Addresses, this.Resources, Direction.Left);
		}

		private enum Direction { Left, Right }
		private void slide(Border oldVisual, Border newVisual, Direction direction)
		{
			var width = this.ActualWidth;
			var animOut = new ThicknessAnimation(new Thickness(0), new Thickness(direction == Direction.Right ? -width : width, 0, direction == Direction.Right ? width : 0, 0), new Duration(TimeSpan.FromMilliseconds(300)));
			var animIn = new ThicknessAnimation(new Thickness(direction == Direction.Right ? width : -width, 0, direction == Direction.Right ? 0 : width, 0), new Thickness(0), new Duration(TimeSpan.FromMilliseconds(400)));
            
            animIn.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 4 };
            animIn.EasingFunction = new ExponentialEase { EasingMode = EasingMode.EaseOut, Exponent = 3 };

			Storyboard.SetTarget(animOut, oldVisual);
			Storyboard.SetTargetProperty(animOut, new PropertyPath(MarginProperty));

			Storyboard.SetTarget(animIn, newVisual);
			Storyboard.SetTargetProperty(animIn, new PropertyPath(MarginProperty));

            _storyBoard = new Storyboard();
            _storyBoard.Children.Add(animOut);
            _storyBoard.Children.Add(animIn);
            _storyBoard.Completed += StoryBoardCompleted;
            _storyBoard.Begin();
		}

        void StoryBoardCompleted(object sender, EventArgs e)
        {
            _storyBoard.Stop();
            UpdateMarginValues();
        }

        private void UpdateMarginValues()
        {
            if (_currentDirection == Direction.Right)
            {
                Resources.Margin = new Thickness(-ActualWidth, 0, ActualWidth, 0);
                Addresses.Margin = new Thickness(0);
            }
            else
            {
                Addresses.Margin = new Thickness(ActualWidth, 0, ActualWidth, 0);
                Resources.Margin = new Thickness(0);
            }
        }


	    private void Addresses_GoBack(object sender, RoutedEventArgs routedEventArgs)
		{
            _currentDirection = Direction.Left;
			slide(this.Addresses, this.Resources, Direction.Left);
		}

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateMarginValues();
        }
	}
}
