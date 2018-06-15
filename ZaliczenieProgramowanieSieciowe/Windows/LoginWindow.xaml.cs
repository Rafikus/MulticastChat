using System.Windows;
using System.Windows.Input;

namespace ZaliczenieProgramowanieSieciowe.Windows
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            ChatManager.Sender = new Sender();
            ChatManager.Listener = new Listener();
            ChatManager.Listener.StartListening();
            ChatManager.Parser = new MessageParser();
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow(new User(UsernameTextBox.Text));
            mainWindow.Show();
            this.Close();
        }
    }
}
