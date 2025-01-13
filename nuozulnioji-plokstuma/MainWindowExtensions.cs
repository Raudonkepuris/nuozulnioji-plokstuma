using System.Windows;
using System.Windows.Controls;

namespace nuozulnioji_plokstuma
{
    public partial class MainWindow : Window
    {
        public double angle
        {
            get { return Resources["angle"] is double angle ? angle : 0; }
            set { Resources["angle"] = value; }
        }
        public double figureSize
        {
            get { return Resources["figureSize"] is double angle ? angle : 0; }
            set { Resources["figureSize"] = value; }
        }
        public double platformLength
        {
            get { return Resources["platformLength"] is double angle ? angle : 0; }
            set { Resources["platformLength"] = value; }
        }
        public double figure
        {
            get { return Resources["figure"] is double angle ? angle : 0; }
            set { Resources["figure"] = value; }
        }
        public double figureMass
        {
            get { return Resources["figureMass"] is double angle ? angle : 0; }
            set { Resources["figureMass"] = value; }
        }
        public double frictionCoeficcient
        {
            get { return Resources["frictionCoeficcient"] is double angle ? angle : 0; }
            set { Resources["frictionCoeficcient"] = value; }
        }

        public double platformTop
        {
            get { return Canvas.GetTop(platform); }
            set { Canvas.SetTop(platform, value); }
        }
        public double platformLeft
        {
            get { return Canvas.GetLeft(platform); }
            set { Canvas.SetLeft(platform, value); }
        }
        public double figureImageTop
        {
            get { return Canvas.GetTop(figureImage); }
            set { Canvas.SetTop(figureImage, value); }
        }
        public double figureImageLeft
        {
            get { return Canvas.GetLeft(figureImage); }
            set { Canvas.SetLeft(figureImage, value); }
        }
    }
}
