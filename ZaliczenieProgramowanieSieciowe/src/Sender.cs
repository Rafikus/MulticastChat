using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ZaliczenieProgramowanieSieciowe
{
    class Sender
    {
        private Thread _thread;
        public void Send(string message)
        {
            _thread = new Thread(() =>
            {
                UdpClient udpclient = new UdpClient();

                IPAddress multicastaddress = IPAddress.Parse("239.0.0.222");
                IPEndPoint remoteep = new IPEndPoint(multicastaddress, 2222);
                udpclient.JoinMulticastGroup(multicastaddress);

                var buffer = Encoding.Unicode.GetBytes(message);
                udpclient.Send(buffer, buffer.Length, remoteep);
            });
            _thread.IsBackground = true;
            _thread.Start();
        }
    }
}
