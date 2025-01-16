using System.Windows;
using System.Windows.Controls;

namespace nuozulnioji_plokstuma
{
    /// <summary>
    /// Dalinė klasė pagalbiniams <see cref="MainWindow"/> metodams.
    /// </summary>
    public partial class MainWindow : Window
    {
        private void updateLabel()
        {
            AnimationDurationLabel.Content = AnimationDuration == null ? 
                                            "" : $"Slidimo laikas {AnimationDuration} ms";
        }

        private (double left, double top) GetActualPlatformTopLeft()
        {
            var x = (PlatformWidth / 2) * Math.Cos(Math.Abs(figureObject.position.angle) * (Math.PI / 180));
            var y = (PlatformWidth / 2) * Math.Sin(Math.Abs(figureObject.position.angle) * (Math.PI / 180));

            var l = PlatformLeft + (PlatformWidth / 2) + x;
            var t = PlatformTop - y;

            l = Math.Round(l, 2);
            t = Math.Round(t, 2);

            return (l, t);
        }

        private (double left, double top) AdjustFigureImagePosition()
        {
            var (platformLeft, platformTop) = GetActualPlatformTopLeft();

            var (figureImageAdjustedLeft, figureImageAdjustedTop) = figureObject.GetAccurateFigurePosition(platformLeft, platformTop);

            figureObject.position.left = figureImageAdjustedLeft;
            figureObject.position.top = figureImageAdjustedTop;

            Canvas.SetLeft(figureImage, figureObject.position.left);
            Canvas.SetTop(figureImage, figureObject.position.top);

            return (figureImageAdjustedLeft, figureImageAdjustedTop);
        }
    }
}
