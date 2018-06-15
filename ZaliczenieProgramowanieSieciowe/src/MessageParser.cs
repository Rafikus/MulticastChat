using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Controls;

namespace ZaliczenieProgramowanieSieciowe
{
    class MessageParser
    {
        private readonly Dictionary<string, Func<string[], bool>> _parserFunctions;
        public delegate void UpdateTextCallback(string message);

        public MessageParser()
        {
            _parserFunctions = new Dictionary<string, Func<string[], bool>>
            {
                {"NICK", VerifyNickname},
                {"MSG", HandleMessage},
                {"JOIN", SomeoneJoined},
                {"LEFT", SomeoneLeft},
                {"WHOIS", ImInTheRoom},
                {"ROOM", HandleRoom}
            };
        }

        private bool VerifyNickname(string[] arg)
        {
            if (arg.Contains("BUSY"))
            {
                LoginManager.Flag = false;
            }
            else if (arg[1] == ChatManager.LocalUser?.Username)
            {
                ChatManager.Sender.Send($"NICK {arg[1]} BUSY");
            }
            return true;
        }

        private bool HandleMessage(string[] splitMessage)
        {
            string username = splitMessage[1];
            string room = splitMessage[2];
            string message = ConcatRestOfArray(3, splitMessage);
            if(room == ChatManager.Room.Name)
                if (ChatManager.ChatBox != null)
                    ChatManager.ChatBox.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), $"{username}: {message}");
            
            return true;
        }

        private bool SomeoneJoined(string[] arg)
        {
            if (arg[1] == ChatManager.Room?.Name)
            {
                if (ChatManager.ChatBox != null)
                    ChatManager.ChatBox.Dispatcher.Invoke(new UpdateTextCallback(UpdateText),
                        $"{arg[2]} joined {ChatManager.Room?.Name}");
                ChatManager.Room?.AddUser(arg[2]);
            }
            return true;
        }

        private bool SomeoneLeft(string[] arg)
        {
            if (arg[1] == ChatManager.Room?.Name)
            {
                if (ChatManager.ChatBox != null)
                    ChatManager.ChatBox.Dispatcher.Invoke(new UpdateTextCallback(UpdateText),
                        $"{arg[2]} left {ChatManager.Room?.Name}");
                ChatManager.Room?.RemoveUser(arg[2]);
            }

            return true;
        }

        private bool HandleRoom(string[] arg)
        {
            if(arg[1] == ChatManager.Room?.Name)
                ChatManager.Room?.AddUser(arg[2]);
            return true;
        }

        private bool ImInTheRoom(string[] arg)
        {
            if(arg[1] == ChatManager.Room?.Name)
                ChatManager.Sender.Send($"ROOM {ChatManager.Room?.Name} {ChatManager.LocalUser.Username}");
            return true;
        }
         
        private void UpdateText(string message)
        {
            ChatManager.ChatBox.AppendText(message + "\n");
        }

        private string ConcatRestOfArray(int startingIndex, string[] array)
        {
            string output = "";
            for (int i = startingIndex; i < array.Length; i++)
            {
                output += $"{array[i]} ";
            }

            return output;
        }

        public void Parse(string message)
        {
            string[] splitMessage = message.Split(' ');
            if(ChatManager.ChatBox != null)
                ChatManager.ChatBox.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), $"DEBUG: {message}");
            _parserFunctions[splitMessage[0]].Invoke(splitMessage);
        }
    }
}
