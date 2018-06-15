using System;
using System.Windows;
using System.Windows.Controls;

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
            ChatManager.ChatBox = ChatTextBox;
            ChatManager.Sender.Send($"JOIN {ChatManager.Room.Name} {ChatManager.LocalUser.Username}");
            ChatManager.Sender.Send($"WHOIS {ChatManager.Room.Name}");
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            ChatManager.Sender.Send($"MSG {ChatManager.LocalUser.Username} {ChatManager.Room.Name} {MessageBox.Text}");
            MessageBox.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var newWindow = new LoginWindow();
            newWindow.Show();
            ChatManager.Sender.Abort();
            ChatManager.Listener.Abort();
            this.Close();
        }

        private void UpdateScrollViewer(object sender, ScrollChangedEventArgs e)
        {
            ChatBoxScrollViewer.ScrollToEnd();
        }

        protected override void OnClosed(EventArgs e)
        {
            ChatManager.Sender.Send($"LEFT {ChatManager.Room.Name} {ChatManager.LocalUser.Username}");
            base.OnClosed(e);
        }
    }
}
