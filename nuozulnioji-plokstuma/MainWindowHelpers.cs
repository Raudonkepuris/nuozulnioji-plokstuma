using System.Windows;
using System.Windows.Controls;

namespace nuozulnioji_plokstuma
{
    public partial class MainWindow : Window
    {
        private void updateLabel()
        {
            leftTopPlatformLabel.Content = $"platform: {platformLeft} ; {platformTop}";
            var (actualPlatformLeft, actualPlatformTop) = GetActualPlatformTopLeft();
            leftTopActualPlatformLabel.Content = $"platform actual: {actualPlatformLeft} ; {actualPlatformTop}";
            leftTopFigureImageLabel.Content = $"figure: {figureImageLeft} ; {figureImageTop}";
        }

        private (double left, double top) GetActualPlatformTopLeft()
        {
            var x = (platformLength / 2) * Math.Cos(Math.Abs(angle) * (Math.PI / 180));
            var y = (platformLength / 2) * Math.Sin(Math.Abs(angle) * (Math.PI / 180));

            var l = platformLeft + (platformLength / 2) + x;
            var t = platformTop - y;

            l = Math.Round(l, 2);
            t = Math.Round(t, 2);

            return (l, t);
        }

        private (double left, double top) AdjustFigureImagePosition()
        {
            var (platformLeft, platformTop) = GetActualPlatformTopLeft();

            var figureImageAdjustedLeft = platformLeft - figureImage.ActualWidth;
            var figureImageAdjustedTop = platformTop - figureImage.ActualHeight;

            figureImageLeft = figureImageAdjustedLeft;
            figureImageTop = figureImageAdjustedTop;

            return (figureImageAdjustedLeft, figureImageAdjustedTop);
        }
    }
}
