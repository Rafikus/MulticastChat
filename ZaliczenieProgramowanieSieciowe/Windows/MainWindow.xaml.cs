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

        public MainWindow(User user)
        {
            InitializeComponent();
            ChatManager.ChatBox = ChatTextBox;
            ChatManager.LocalUser = user;
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            ChatManager.Sender.Send($"MSG {ChatManager.LocalUser.Username} ROOM {MessageBox.Text}");
            MessageBox.Clear();
        }
    }
}
