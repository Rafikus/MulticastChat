using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ZaliczenieProgramowanieSieciowe
{
    class Listener
    {
        private Thread _thread;
        public void StartListening()
        {
            _thread = new Thread(() =>
            {
                UdpClient client = new UdpClient { ExclusiveAddressUse = false };

                IPEndPoint localEp = new IPEndPoint(IPAddress.Any, 2222);
                
                client.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                client.ExclusiveAddressUse = false;

                client.Client.Bind(localEp);

                IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
                client.JoinMulticastGroup(multicastaddress);

                while (true)
                {
                    Byte[] data = client.Receive(ref localEp);
                    string strData = Encoding.Unicode.GetString(data);
                    ChatManager.Parser.Parse(strData);
                }
            });
            _thread.IsBackground = true;
            _thread.Start();
        }

        public void Abort()
        {
            _thread.Abort();
        }
       
    }
}
