using System.Windows.Controls;

namespace ZaliczenieProgramowanieSieciowe
{
    static class ChatManager
    {
        public static User LocalUser;
        public static Room Room;
        public static Sender Sender;
        public static Listener Listener;
        public static MessageParser Parser;

        public static TextBox ChatBox;
        public static TreeViewItem RoomTreeView;
    }
}
