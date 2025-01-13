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
        private readonly double _platformHeight;

        public String[] figures = { "Kvadratas", "Apskritimas" };

        public MainWindow()
        {
            InitializeComponent();

            var initialAngle = (double)Resources["angle"];

            angleLabel.Content = Math.Abs(initialAngle);

            RotateTransform rotateTransform = new RotateTransform(initialAngle);
            platform.RenderTransform = rotateTransform;

            figureComboBox.ItemsSource = figures;
            figureComboBox.SelectedIndex = 0;

            squareLengthLabel.Visibility = Visibility.Visible;
            circleDiameterLabel.Visibility = Visibility.Hidden;

            figureImage.Source = new BitmapImage(new Uri("assets/kvadratas.png", UriKind.Relative));
            figureImage.Width = (double)Resources["figureSize"];

            figureSizeInput.Value = (double)Resources["figureSize"];
            figureMassInput.Value = (double)Resources["figureMass"];
            frictionCoefficientInput.Value = (double)Resources["frictionCoeficcient"];
            platformLengthInput.Value = (double)Resources["platformLength"];

            _platformHeight = platform.ActualHeight;
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

        private void figureSizeInputChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var curAngle = (double)this.FindResource("angle");
            var curLeft = Canvas.GetLeft(figureImage);

            var curTop = Canvas.GetTop(figureImage);
            var curWidth = (double)Resources["figureSize"];
            Resources["figureSize"] = ((DoubleUpDown)sender).Value;
            figureImage.RenderTransformOrigin = new Point(1, 1);
            figureImage.Width = (double)Resources["figureSize"];
            Canvas.SetTop(figureImage, curTop + (curWidth - (double)Resources["figureSize"])/1.06);
            Canvas.SetLeft(figureImage, curLeft + (curWidth - (double)Resources["figureSize"]));
        }

        private void runSimulation(object sender, RoutedEventArgs e)
        {
            var platformAngle = (double)this.FindResource("angle");
            var platformWidth = platform.ActualWidth;

            var curFigureX = (platformWidth / 2) * Math.Cos(Math.Abs(platformAngle) * (Math.PI / 180));
            var curFigureY = (platformWidth / 2) * Math.Sin(Math.Abs(platformAngle) * (Math.PI / 180));

            var curFigureLeft = Canvas.GetLeft(figureImage);
            var curFigureTop = Canvas.GetTop(figureImage);

            var newFigureLeft = curFigureLeft - 2 * curFigureX;
            var newFigureTop = curFigureTop + 2 * curFigureY;
            
            Canvas.SetLeft(figureImage, newFigureLeft);
            Canvas.SetTop(figureImage, newFigureTop);
        }

        private void adjustAngle(object sender, RoutedEventArgs e)
        {
            var angleDif = Convert.ToDouble(((Button)sender).Tag.ToString());

            var newAngle = (double)this.FindResource("angle") + angleDif;

            if (newAngle >= -40 && newAngle <= 0)
            {
                Resources["angle"] = newAngle;
                angleLabel.Content = Math.Abs(newAngle);

                var rotateTransform = new RotateTransform(newAngle, 0.5, 0.5);

                var point = new transform

                platform.RenderTransform = rotateTransform;
                figureImage.RenderTransform = rotateTransform;

                var platformWidth = platform.ActualWidth;

                var curFigureLeft = Canvas.GetLeft(figureImage);
                var curFigureTop = Canvas.GetTop(figureImage);

                var curFigureX = (platformWidth / 2) * Math.Cos(Math.Abs(newAngle - angleDif) * (Math.PI / 180));
                var curFigureY = (platformWidth / 2) * Math.Sin(Math.Abs(newAngle - angleDif) * (Math.PI / 180));

                var newFigureX = (platformWidth / 2) * Math.Cos(Math.Abs(newAngle) * (Math.PI / 180));
                var newFigureY = (platformWidth / 2) * Math.Sin(Math.Abs(newAngle) * (Math.PI / 180));

                var newFigureLeft = curFigureLeft + (newFigureX - curFigureX);
                var newFigureTop = curFigureTop - (newFigureY - curFigureY);

                figureImage.RenderTransformOrigin = new Point(1, 1);
                Canvas.SetTop(figureImage, newFigureTop);
                Canvas.SetLeft(figureImage, newFigureLeft);
            }
        }

        private void platformLengthInputChanged(object sender, EventArgs e)
        {
            var newPlatformWidth = ((DoubleUpDown)sender).Value;
            var curPlatformWidth = platform.ActualWidth;

            if(newPlatformWidth is not null && curPlatformWidth != 0)
            {
                var widthTransform = new ScaleTransform((double)newPlatformWidth / curPlatformWidth, 1, 0.5, 0.5);

                platform.RenderTransform = widthTransform;

                var platformAngle = (double)this.FindResource("angle");

                RotateTransform rotateTransform = new RotateTransform(platformAngle, 0.5, 0.5);

                platform.RenderTransform = rotateTransform;

                //platform.Stretch = Stretch.Fill;
                //platform.Width = (double)newPlatformWidth;
                //var widthDif = newPlatformWidth - curPlatformWidth;
                //var curPlatformLeft = Canvas.GetLeft(platform);
                //var newPlatformLeft = curPlatformLeft - widthDif / 2;
                //Canvas.SetLeft(platform, (double)newPlatformLeft);

                //var curFigureLeft = Canvas.GetLeft(figureImage);
                //var newFigureLeft = curFigureLeft + widthDif / 2;
                //Canvas.SetLeft(figureImage, (double)newFigureLeft);
            }
        }
    }
}