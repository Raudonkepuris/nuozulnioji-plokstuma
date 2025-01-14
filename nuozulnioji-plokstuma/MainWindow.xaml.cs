using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xceed.Wpf.Toolkit;

namespace nuozulnioji_plokstuma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String[] figures = { "Kvadratas", "Apskritimas" };

        public MainWindow()
        {
            InitializeComponent();

            var initialAngle = angle;

            angleLabel.Content = Math.Abs(initialAngle);

            RotateTransform rotateTransform = new RotateTransform(initialAngle);
            platform.RenderTransform = rotateTransform;

            figureComboBox.ItemsSource = figures;
            figureComboBox.SelectedIndex = 0;

            squareLengthLabel.Visibility = Visibility.Visible;
            circleDiameterLabel.Visibility = Visibility.Hidden;

            figureImage.Source = new BitmapImage(new Uri("assets/kvadratas.png", UriKind.Relative));
            figureImage.Width = figureSize;

            figureSizeInput.Value = figureSize;
            figureMassInput.Value = figureMass;
            frictionCoefficientInput.Value = frictionCoeficcient;
            platformLengthInput.Value = platformLength;


            AdjustFigureImagePosition();
            updateLabel();
        }

        private void figureSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            squareLengthLabel.Visibility = Visibility.Hidden;
            circleDiameterLabel.Visibility = Visibility.Hidden;

            switch (figureComboBox.SelectedItem)
            {
                case "Kvadratas":
                    squareLengthLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri("assets/kvadratas.png", UriKind.Relative));
                    break;
                case "Apskritimas":
                    circleDiameterLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri("assets/apskritimas.png", UriKind.Relative));
                    break;
                default:
                    break;
            }
        }

        private void toggleFigureSelection(object sender, MouseButtonEventArgs e)
        {
            squareLengthLabel.Visibility = Visibility.Hidden;
            circleDiameterLabel.Visibility = Visibility.Hidden;

            switch (figureComboBox.SelectedItem)
            {
                case "Kvadratas":
                    squareLengthLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri("assets/apskritimas.png", UriKind.Relative));
                    figureComboBox.SelectedIndex = 1;
                    break;
                case "Apskritimas":
                    circleDiameterLabel.Visibility = Visibility.Visible;
                    figureImage.Source = new BitmapImage(new Uri("assets/kvadratas.png", UriKind.Relative));
                    figureComboBox.SelectedIndex = 0;
                    break;
                default:
                    break;
            }
        }

        private void runSimulation(object sender, RoutedEventArgs e)
        {
            var curFigureLeft = figureImageLeft;
            var curFigureTop = figureImageTop;

            var curFigureX = (platformLength / 2) * Math.Cos(Math.Abs(angle) * (Math.PI / 180));
            var curFigureY = (platformLength / 2) * Math.Sin(Math.Abs(angle) * (Math.PI / 180));

            var newFigureLeft = figureImageLeft - 2 * curFigureX;
            var newFigureTop = figureImageTop + 2 * curFigureY;

            var animationTime = 2; //seconds

            // TODO: move somewhere else
            // TODO: add actual calculation for slide/rotation times
            DoubleAnimation slideAnimationLeft = new DoubleAnimation
            {
                From = figureImageLeft,
                To = newFigureLeft,
                Duration = new Duration(TimeSpan.FromSeconds(animationTime)),
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut },
                FillBehavior = FillBehavior.Stop
            };
            DoubleAnimation slideAnimationTop = new DoubleAnimation
            {
                From = figureImageTop,
                To = newFigureTop,
                Duration = new Duration(TimeSpan.FromSeconds(animationTime)),
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
            
            AdjustFigureImagePosition();
            updateLabel();
        }

        private void adjustAngle(object sender, RoutedEventArgs e)
        {
            var angleDif = Convert.ToDouble(((Button)sender).Tag.ToString());

            var newAngle = angle + angleDif;

            if (newAngle >= -40 && newAngle <= 0)
            {
                angle = newAngle;
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

            if (figureSize != newWidth)
            {
                figureImage.Width = newWidth;
                figureSize = newWidth;
            }

            updateLabel();
            AdjustFigureImagePosition();
        }

        private void platformLengthInputChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var newLength = (double)e.NewValue;

            if (platformLength != newLength)
            {
                platform.Width = newLength;

                platformLeft = platformLeft - ((newLength - platformLength) / 2);

                platformLength = newLength;
            }

            updateLabel();
            AdjustFigureImagePosition();
        }
    }
}