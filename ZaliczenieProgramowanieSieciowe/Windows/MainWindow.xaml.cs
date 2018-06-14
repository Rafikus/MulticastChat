using System.Windows;

namespace ZaliczenieProgramowanieSieciowe.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string username)
        {
            InitializeComponent();
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {

        }
    }
}
