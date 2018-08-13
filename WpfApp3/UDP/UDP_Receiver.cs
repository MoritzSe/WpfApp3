using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3.UDP
{
    class UDP_Receiver
    {
        

        private Socket udpSockSILABSpeed;
        private byte[] bufferSILABSpeed;
        private EndPoint epSILABSpeed = new IPEndPoint(IPAddress.Any, 0); //0 auszutauschen für den richtigen Port, ebenfalls für alle anderen 

        private Socket udpSockSILABRpm;
        private byte[] bufferSILABRpm;
        private EndPoint epSILABRpm = new IPEndPoint(IPAddress.Any, 0);

        private Socket udpSockSILAB;
        private byte[] bufferSILAB;
        private EndPoint epSILAB = new IPEndPoint(IPAddress.Any, 0);

        private Socket udpSockTakeOver;
        private byte[] bufferTackOVer;
        private EndPoint epTakeOVer = new IPEndPoint(IPAddress.Any, 0);

        private Socket udpSockSendUpdate;
        private byte[] bufferSendUpdate;
        private EndPoint epSendUpdate = new IPEndPoint(IPAddress.Any, 0);

        private Socket udpSockSportSensor;
        private byte[] bufferSportSensor;
        private EndPoint epSportSensor = new IPEndPoint(IPAddress.Any, 0);

        private Socket udpSockMenueNavigation;
        private byte[] bufferMenueNavigation;
        private EndPoint epMenueNavigation = new IPEndPoint(IPAddress.Any, 8001); //Testport

        private Socket udpSockAutopilot;
        private byte[] bufferAutopilot;
        private EndPoint epAutopilot = new IPEndPoint(IPAddress.Any, 8002); // Ports dürfen nicht gleich sein, sonst meckert VS

        public UDP_Receiver()
        {   //InterNetwork is IPv4, Dgram is Datagramm
            udpSockSILABSpeed = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSockSILABSpeed.Bind(epSILAB);
            bufferSILABSpeed = new byte[1]; // 1 Byte sollte reichen, um Geschwindigkeiten von 0 bis etwa 100/120 km/h darzustellen

            udpSockAutopilot = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSockAutopilot.Bind(epAutopilot);
            bufferAutopilot = new byte[1];

            udpSockMenueNavigation = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSockMenueNavigation.Bind(epMenueNavigation);
            bufferMenueNavigation = new byte[1];

            //Start listening for a new message
            EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0); // Portnummer?

            udpSockSILABSpeed.BeginReceiveFrom(bufferSILABSpeed, 0, bufferSILABSpeed.Length, SocketFlags.None, ref clientEP, DoReceiveFromSILABSpeed, udpSockSILABSpeed);
            
            udpSockMenueNavigation.BeginReceiveFrom(bufferMenueNavigation, 0, bufferMenueNavigation.Length, SocketFlags.None, ref clientEP, DoReceiveFromMenueNavigation, udpSockMenueNavigation);
            udpSockAutopilot.BeginReceiveFrom(bufferAutopilot, 0, bufferAutopilot.Length, SocketFlags.None, ref clientEP, DoReceiveFromAutopilot, udpSockAutopilot);

        }

        public void DoReceiveFromSILABSpeed(IAsyncResult iar)
        {
            try
            {
                // Get received message
                Socket recvSock = (Socket)iar.AsyncState;
                EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                int msgLen = recvSock.EndReceiveFrom(iar, ref clientEP);
                byte[] localMsg = new byte[msgLen];
                Array.Copy(bufferSILABSpeed, localMsg, msgLen);

                PacketSILABSpeed silabspeed = new PacketSILABSpeed(this.bufferSILABSpeed);

                if(silabspeed.SILABSpeeddata != null)
                {
                   //MainWindow.mainWindow.udpSILABSpeed = silabspeed;
                }
                udpSockSILABSpeed.BeginReceiveFrom(bufferSILABSpeed, 0, bufferSILABSpeed.Length, SocketFlags.None, ref clientEP, DoReceiveFromSILABSpeed, udpSockSILABSpeed);
            }
            catch (ObjectDisposedException)
            {
                //do smth here
            }
        }

        public void DoReceiveFromMenueNavigation(IAsyncResult iar)
        {
            try
            {
                Socket recvSock = (Socket)iar.AsyncState;
                EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                int msgLen = recvSock.EndReceiveFrom(iar, ref clientEP);
                byte[] localMsg = new byte[msgLen];
                Array.Copy(bufferMenueNavigation, localMsg, msgLen);
                PacketMenueNavigation menueNavigation = new PacketMenueNavigation(this.bufferMenueNavigation);

                if (menueNavigation.Data != null)
                {
                    MainWindow.mainWindow.udpMenueNavigation = menueNavigation;
                }
                udpSockMenueNavigation.BeginReceiveFrom(bufferMenueNavigation, 0, bufferMenueNavigation.Length, SocketFlags.None, ref clientEP, DoReceiveFromMenueNavigation, udpSockMenueNavigation);
            }
            catch (ObjectDisposedException)
            {

            }
        }

        private void DoReceiveFromAutopilot(IAsyncResult iar)
        {
            try
            {
                Socket recvSock = (Socket)iar.AsyncState;
                EndPoint clientEP = new IPEndPoint(IPAddress.Any, 0);
                int msgLen = recvSock.EndReceiveFrom(iar, ref clientEP);
                byte[] localMsg = new byte[msgLen];
                Array.Copy(bufferAutopilot, localMsg, msgLen);
                string asciistring = Encoding.ASCII.GetString(bufferAutopilot, 0, bufferAutopilot.Length);
                Debug.Print("Autopilot received bufferbyte: " + bufferAutopilot + " and ASCII Encoded string: " + asciistring);
                PacketAutopilot autopilot = new PacketAutopilot(this.bufferAutopilot);

                if(autopilot.Data != null)
                {
                    MainWindow.mainWindow.udpAutopilot = autopilot;
                }
                udpSockAutopilot.BeginReceiveFrom(bufferAutopilot, 0, bufferAutopilot.Length, SocketFlags.None, ref clientEP, DoReceiveFromAutopilot, udpSockAutopilot);

                
            }
            catch (ObjectDisposedException)
            {

            }
        }

        private string ReceiveUDP(int port) // vielleicht damit?
        {
            try
            {
                UdpClient listener = new UdpClient(port);
                IPEndPoint ep = new IPEndPoint(IPAddress.Any, port);
                string received_data = "";  
                byte[] received_data_byte;
                while (!true)
                {
                    received_data_byte = listener.Receive(ref ep);
                    received_data = Encoding.ASCII.GetString(received_data_byte, 0, received_data_byte.Length);


                }
                return received_data;
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
            
    }
}
