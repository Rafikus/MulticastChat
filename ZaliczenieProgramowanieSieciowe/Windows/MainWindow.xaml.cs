using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            ChatManager.RoomTreeView = new TreeViewItem(){Header = ChatManager.Room.Name};
            RoomName.Items.Add(ChatManager.RoomTreeView);
            ChatManager.RoomTreeView.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));

            ChatManager.Sender.Send($"JOIN {ChatManager.Room.Name} {ChatManager.LocalUser.Username}");
            ChatManager.Sender.Send($"WHOIS {ChatManager.Room.Name}");
        }

        private void SendMessage(object sender, RoutedEventArgs e)
        {
            ChatManager.Sender.Send($"MSG {ChatManager.LocalUser.Username} {ChatManager.Room.Name} {MessageBox.Text}");
            MessageBox.Clear();
        }

        private void BackToLogin(object sender, RoutedEventArgs e)
        {
            var newWindow = new LoginWindow();
            newWindow.Show();
            ChatManager.Sender.Abort();
            ChatManager.Listener.Abort();
            this.Close();
        }

        private void SendMessageWithEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage(sender, null);
            }
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
