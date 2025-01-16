using System.Windows;
using System.Windows.Controls;

namespace nuozulnioji_plokstuma
{
    /// <summary>
    /// Dalinė klasė papildomiems <see cref="MainWindow"/> parametrams.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Platformos ilgio parametras.
        /// </summary>
        public double PlatformWidth
        {
            get { return platform.Width; }
            set { platform.Width = value; }
        }
        private double _frictionCoefficient;
        /// <summary>
        /// Platformos trinties koeficiento parametras.
        /// </summary>
        public double FrictionCoeficcient
        {
            get { return _frictionCoefficient; }
            set { _frictionCoefficient = value; }
        }
        /// <summary>
        /// Platformos pozicijos nuo viršutinio kampo parametras.
        /// </summary>
        public double PlatformTop
        {
            get { return Canvas.GetTop(platform); }
            set { Canvas.SetTop(platform, value); }
        }
        /// <summary>
        /// Platformos pozicijos nuo kairiojo kampo parametras.
        /// </summary>
        public double PlatformLeft
        {
            get { return Canvas.GetLeft(platform); }
            set { Canvas.SetLeft(platform, value); }
        }
        private long? _animationDuration = default;
        /// <summary>
        /// Figūros slidimo trukmės parametras, naudojamas <see cref="AnimationDurationLabel"/>
        /// </summary>
        public long? AnimationDuration
        {
            get { return _animationDuration; }
            set { _animationDuration = value; }
        }
    }
}
