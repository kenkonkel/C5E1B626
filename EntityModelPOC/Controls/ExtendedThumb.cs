using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace EntityModelPOC.Controls
{
    public class ExtendedThumb : Thumb
    {


        public Brush DisabledBackgroundColor
        {
            get { return (Brush)GetValue(DisabledBackgroundColorProperty); }
            set { SetValue(DisabledBackgroundColorProperty, value); }
        }

        public static readonly DependencyProperty DisabledBackgroundColorProperty =
            DependencyProperty.Register("DisabledBackgroundColor", typeof(Brush), typeof(ExtendedThumb), new UIPropertyMetadata(null));

        public Brush HoverBackgroundColor
        {
            get { return (Brush)GetValue(HoverBackgroundColorProperty); }
            set { SetValue(HoverBackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty HoverBackgroundColorProperty =
            DependencyProperty.Register("HoverBackgroundColor", typeof(Brush), typeof(ExtendedThumb), new UIPropertyMetadata(null));


        public Brush NormalBackgroundColor
        {
            get { return (Brush)GetValue(NormalBackgroundColorProperty); }
            set { SetValue(NormalBackgroundColorProperty, value); }
        }
        public static readonly DependencyProperty NormalBackgroundColorProperty =
            DependencyProperty.Register("NormalBackgroundColor", typeof(Brush), typeof(ExtendedThumb), new UIPropertyMetadata(null));
    }
}
