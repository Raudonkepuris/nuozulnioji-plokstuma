using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using Newtonsoft.Json;
using nuozulnioji_plokstuma.Models;

namespace nuozulnioji_plokstuma
{
    public partial class MainWindow : Window
    {
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            var data = new FileData
            (
                figureObject.name,
                figureObject.position.width,
                figureObject.position.mass,
                figureObject.position.left,
                figureObject.position.top,
                FrictionCoeficcient,
                PlatformWidth,
                PlatformLeft,
                PlatformTop,
                figureObject.position.angle
            );

            string json = JsonConvert.SerializeObject(data);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.WriteAllText(saveFileDialog.FileName, json);
                    MessageBox.Show("Failas išsaugotas!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Klaida saugant failą: {ex.Message}");
                }
            }
        }

        private void LoadClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JSON Files (*.json)|*.json|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string json = File.ReadAllText(openFileDialog.FileName);

                    FileData data = JsonConvert.DeserializeObject<FileData>(json);

                    var oldImageWidth = figureObject.position.width;
                    FixImageSize(oldImageWidth, data.FigureWidth);

                    var position = new Position
                    {
                        left = data.FigureLeft,
                        top = data.FigureTop,
                        angle = data.Angle,
                        width = data.FigureWidth,
                        mass = data.FigureMass

                    };

                    Figures figure;
                    if (data.FigureName == "Kvadratas")
                        figure = Figures.Square;
                    else
                        figure = Figures.Circle;

                    figureObject = FigureFactory.GetFigure(figure, position);

                    angleLabel.Content = Math.Abs(data.Angle);
                    angleBtnUp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    angleBtnDown.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

                    
                    figureSizeInput.Value = data.FigureWidth;
                    figureMassInput.Value = data.FigureMass;
                    frictionCoefficientInput.Value = data.PlatformFrictionCoefficient;
                    platformLengthInput.Value = data.PlatformLength;

                    MessageBox.Show($"Failas nuskaitytas! {data.ToString()}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Klaida atidarant failą: {ex.Message}");
                }
            }
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private class FileData
        {
            public string FigureName { get; set; }
            public double FigureWidth { get; set; }
            public double FigureMass { get; set; }
            public double FigureLeft { get; set; }
            public double FigureTop { get; set; }
            public double PlatformFrictionCoefficient { get; set; }
            public double PlatformLength { get; set; }
            public double PlatformLeft { get; set; }
            public double PlatformTop { get; set; }
            public int Angle { get; set; }

            public FileData(string figureName, double figureWidth, double figureMass, double figureLeft, double figureTop,
                              double platformFrictionCoefficient, double platformLength, double platformLeft, double platformTop, int angle)
            {
                FigureName = figureName;
                FigureWidth = figureWidth;
                FigureMass = figureMass;
                FigureLeft = figureLeft;
                FigureTop = figureTop;
                PlatformFrictionCoefficient = platformFrictionCoefficient;
                PlatformLength = platformLength;
                PlatformLeft = platformLeft;
                PlatformTop = platformTop;
                Angle = angle;
            }

            public override string ToString()
            {
                return $"Duomenys: \n" +
                        $"Figūra: {FigureName},\n" +
                        $"Figūros dydis: {FigureWidth} mm,\n" +
                        $"Figūros masė: {FigureMass} g,\n" +
                        $"Figūros pozicija: ({FigureLeft}, {FigureTop}),\n" +
                        $"Platformos trinties koeficientas: {PlatformFrictionCoefficient},\n" +
                        $"Platformos pozicija: ({PlatformLength}, {PlatformTop}),\n" +
                        $"Platformos ilgis: {PlatformLength} mm,\n" +
                        $"Pasisukimo kampas: {Angle}";
            }
        }

        private void FixImageSize(double oldWidth, double newWidth)
        {
            RoutedPropertyChangedEventArgs<object> args =
                new RoutedPropertyChangedEventArgs<object>(oldWidth, newWidth);

            figureSizeInputChanged(figureSizeInput, args);
        }
    }
}
