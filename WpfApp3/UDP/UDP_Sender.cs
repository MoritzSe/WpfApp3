using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.UDP
{
    class UDP_Sender
    {
        Socket server;

        public UDP_Sender()
        {
            server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
     
        }

        public void sendData(string message, string IP, int port)
        {
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            byte[] data = Encoding.ASCII.GetBytes(message);
            server.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
        }

        public void sendByteArray(byte[] data, string IP, int port)
        {
            IPEndPoint RemoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
            server.SendTo(data, data.Length, SocketFlags.None, RemoteEndPoint);
        }



    }
}
