using System.Windows;

namespace nuozulnioji_plokstuma
{
    public partial class UrlInpuWindow : Window
    {
        public string Url { get; private set; }

        public UrlInpuWindow()
        {
            InitializeComponent();
        }

        private void OkClick(object sender, RoutedEventArgs e)
        {
            Url = UrlInput.Text;
            DialogResult = true;
            Close();
        }

        private void CancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
