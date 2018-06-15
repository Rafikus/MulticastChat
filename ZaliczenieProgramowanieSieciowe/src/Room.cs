using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;

namespace ZaliczenieProgramowanieSieciowe
{
    class Room
    {
        public delegate void ReloadListCallback();

        public string Name { get; }
        public Dictionary<string, User> Users { get; }

        public Room(string name)
        {
            Name = name;
            Users = new Dictionary<string, User>();
        }

        public void AddUser(string username)
        {
            if (!Users.ContainsKey(username))
            {
                Users.Add(username, new User(username));
                ChatManager.RoomTreeView.Dispatcher.Invoke(new ReloadListCallback(ReloadList));
            }
        }

        public void RemoveUser(string username)
        {
            Users.Remove(username);
            ChatManager.RoomTreeView.Dispatcher.Invoke(new ReloadListCallback(ReloadList));

        }

        private void ReloadList()
        {
            ChatManager.RoomTreeView.Items.Clear();
            foreach (KeyValuePair<string, User> name in Users)
            {
                ChatManager.RoomTreeView.Items.Add(new TreeViewItem { Header = name.Key });
            }
            ChatManager.RoomTreeView.Items.SortDescriptions.Add(new SortDescription("Header", ListSortDirection.Ascending));
        }

    }
}
