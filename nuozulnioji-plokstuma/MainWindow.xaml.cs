using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using nuozulnioji_plokstuma.Exceptions;
using nuozulnioji_plokstuma.Models;
using static System.Net.Mime.MediaTypeNames;

namespace nuozulnioji_plokstuma
{
    /// <summary>
    /// <see cref="MainWindow"/> lango veikimo logika.
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int INTIIAL_ANGLE = 0;
        private Figure figureObject;

        /// <summary>
        /// Numatytasis konstruktorius inicijuojantis <see cref="MainWindow"/> langą.
        /// </summary>
        public MainWindow()
        {
            // TODO: clean up
            InitializeComponent();

            this.Loaded += (sender, e) =>
            {
                var initialPosition = new Position
                {
                    left = Canvas.GetLeft(figureImage),
                    top = Canvas.GetTop(figureImage),
                    angle = INTIIAL_ANGLE,
                    width = figureImage.Width,
                    mass = 0.2
                };
                figureObject = FigureFactory.GetFigure(Figures.Square, initialPosition);

                angleLabel.Content = Math.Abs(INTIIAL_ANGLE);

                figureImage.Source = new BitmapImage(new Uri(figureObject.source, UriKind.Relative));

                figureSizeInput.Value = figureObject.position.width;
                figureMassInput.Value = figureObject.position.mass;

                FrictionCoeficcient = 0.2;
                frictionCoefficientInput.Value = FrictionCoeficcient;
                platformLengthInput.Value = platform.Width;

                string[] comboBoxItemsSource = (AvailableFigureNamesSingleton.Instance).CalculatedArray;
                figureComboBox.ItemsSource = comboBoxItemsSource;
                figureComboBox.SelectedIndex = Array.IndexOf(comboBoxItemsSource, "Kvadratas");

                squareLengthLabel.Visibility = Visibility.Visible;
                circleDiameterLabel.Visibility = Visibility.Hidden;

                AdjustFigureImagePosition();
                updateLabel();
            };
        }

        private void figureSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: refactor into factory pattern
            squareLengthLabel.Visibility = Visibility.Hidden;
            circleDiameterLabel.Visibility = Visibility.Hidden;

            switch (figureComboBox.SelectedItem)
            {
                case "Kvadratas":
                    figureObject = FigureFactory.GetFigure(Figures.Square, figureObject.position);
                    squareLengthLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri(figureObject.source, UriKind.Relative));
                    break;
                case "Apskritimas":
                    figureObject = FigureFactory.GetFigure(Figures.Circle, figureObject.position);
                    circleDiameterLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri(figureObject.source, UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        private void toggleFigureSelection(object sender, MouseButtonEventArgs e)
        {
            // TODO: refactor into factory pattern
            squareLengthLabel.Visibility = Visibility.Hidden;
            circleDiameterLabel.Visibility = Visibility.Hidden;

            switch (figureComboBox.SelectedItem)
            {
                case "Kvadratas":
                    figureObject = FigureFactory.GetFigure(Figures.Square, figureObject.position);
                    squareLengthLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri(figureObject.source, UriKind.Relative));
                    figureComboBox.SelectedIndex = 1;
                    break;
                case "Apskritimas":
                    figureObject = FigureFactory.GetFigure(Figures.Circle, figureObject.position);
                    circleDiameterLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri(figureObject.source, UriKind.Relative));
                    figureComboBox.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        private void runSimulation(object sender, RoutedEventArgs e)
        {
            try
            {
                AnimationDuration = figureObject.AnimationTime(FrictionCoeficcient, PlatformWidth); //milli seconds

                var curFigureLeft = figureObject.position.left;
                var curFigureTop = figureObject.position.top;

                var curFigureX = (PlatformWidth / 2) * Math.Cos(Math.Abs(figureObject.position.angle) * (Math.PI / 180));
                var curFigureY = (PlatformWidth / 2) * Math.Sin(Math.Abs(figureObject.position.angle) * (Math.PI / 180));

                var newFigureLeft = figureObject.position.left - 2 * curFigureX;
                var newFigureTop = figureObject.position.top + 2 * curFigureY;


                // TODO: move somewhere else
                DoubleAnimation slideAnimationLeft = new DoubleAnimation
                {
                    From = figureObject.position.left,
                    To = newFigureLeft,
                    Duration = new Duration(TimeSpan.FromMilliseconds(AnimationDuration ?? 0)),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut },
                    FillBehavior = FillBehavior.Stop
                };
                DoubleAnimation slideAnimationTop = new DoubleAnimation
                {
                    From = figureObject.position.top,
                    To = newFigureTop,
                    Duration = new Duration(TimeSpan.FromMilliseconds(AnimationDuration ?? 0)),
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut },
                    FillBehavior = FillBehavior.Stop
                };

                Storyboard.SetTarget(slideAnimationLeft, figureImage);
                Storyboard.SetTargetProperty(slideAnimationLeft, new PropertyPath("(Canvas.Left)"));

                Storyboard.SetTarget(slideAnimationTop, figureImage);
                Storyboard.SetTargetProperty(slideAnimationTop, new PropertyPath("(Canvas.Top)"));

                Storyboard storyboard = new Storyboard();
                storyboard.Children.Add(slideAnimationLeft);
                storyboard.Children.Add(slideAnimationTop);

                storyboard.Begin();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                AdjustFigureImagePosition();
                updateLabel();
            }
        }

        // TODO: move all the calculations to singletons
        private void adjustAngle(object sender, RoutedEventArgs e)
        {
            var angleDif = Convert.ToInt32(((Button)sender).Tag.ToString());

            var newAngle = figureObject.position.angle + angleDif;

            if (newAngle >= -40 && newAngle <= 0)
            {
                figureObject.position.angle = newAngle;
                angleLabel.Content = Math.Abs(newAngle);

                var platformRotateTransform = new RotateTransform(newAngle, 0.5, 0);
                var figureImageRotateTransform = new RotateTransform(newAngle, 1, 1);

                platform.RenderTransform = platformRotateTransform;
                figureImage.RenderTransform = figureImageRotateTransform;
            }

            updateLabel();
            AdjustFigureImagePosition();
        }

        private void figureSizeInputChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var newWidth = (double)e.NewValue;

            if (figureObject.position.width != newWidth)
            {
                figureObject.position.width = newWidth;
                figureImage.Width = figureObject.position.width;
            }

            updateLabel();
            AdjustFigureImagePosition();
        }

        private void platformLengthInputChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var newLength = (double)e.NewValue;

            if (PlatformWidth != newLength && PlatformWidth != 0)
            {
                PlatformLeft = PlatformLeft - ((newLength - PlatformWidth) / 2);

                PlatformWidth = newLength;
            }

            updateLabel();
            AdjustFigureImagePosition();
        }

        private void platformFrictionCoefficientChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FrictionCoeficcient = (double)e.NewValue;
        }
    }
}