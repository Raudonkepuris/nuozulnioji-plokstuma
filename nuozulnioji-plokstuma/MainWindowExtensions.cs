using System.Windows;
using System.Windows.Controls;

namespace nuozulnioji_plokstuma
{
    public partial class MainWindow : Window
    {
        public double PlatformWidth
        {
            get { return platform.Width; }
            set { platform.Width = value; }
        }
        private double _frictionCoefficient;
        public double FrictionCoeficcient
        {
            get { return _frictionCoefficient; }
            set { _frictionCoefficient = value; }
        }
        public double PlatformTop
        {
            get { return Canvas.GetTop(platform); }
            set { Canvas.SetTop(platform, value); }
        }
        public double PlatformLeft
        {
            get { return Canvas.GetLeft(platform); }
            set { Canvas.SetLeft(platform, value); }
        }
        private long? _animationDuration = default;
        public long? AnimationDuration
        {
            get { return _animationDuration; }
            set { _animationDuration = value; }
        }
    }
}
