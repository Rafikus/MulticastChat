using System.Collections.Generic;

namespace ZaliczenieProgramowanieSieciowe
{
    class Room
    {
        public string Name { get; }
        public Dictionary<string, User> Users { get; }

        public Room(string name)
        {
            Name = name;
            Users = new Dictionary<string, User>();
        }

        public void AddUser(string username)
        {
            if(!Users.ContainsKey(username))
                Users.Add(username, new User(username));
        }

        public void RemoveUser(string username)
        {
            Users.Remove(username);
        }
    }
}
