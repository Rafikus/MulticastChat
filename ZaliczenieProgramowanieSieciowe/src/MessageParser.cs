using System;
using System.Collections.Generic;
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
            _parserFunctions = new Dictionary<string, Func<string[], bool>>();
            _parserFunctions.Add("NICK", HandleMessage);
            _parserFunctions.Add("MSG", HandleMessage);
            _parserFunctions.Add("ROOM", HandleRoom);
            _parserFunctions.Add("JOIN", HandleMessage);
            _parserFunctions.Add("LEFT", HandleMessage);
        }

        private bool HandleRoom(string[] arg)
        {

            return true;
        }

        private bool HandleMessage(string[] splitMessage)
        {
            string username = splitMessage[1];
            string room = splitMessage[2];
            string message = ConcatRestOfArray(3, splitMessage);
            ChatManager.ChatBox.Dispatcher.Invoke(new UpdateTextCallback(UpdateText), $"{username}@{room}: {message}");
            
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
            _parserFunctions[splitMessage[0]].Invoke(splitMessage);
        }
    }
}
