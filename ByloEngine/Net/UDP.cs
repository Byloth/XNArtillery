using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ByloEngine.Net
{
    public class UDP
    {
        private int port;
        private byte[] receivedBytes;
        private byte[] sendedBytes;

        private UdpClient client;

        public UDP(int selectedPort)
        {
            port = selectedPort;

            client = new UdpClient(new IPEndPoint(IPAddress.Any, selectedPort));
            client.EnableBroadcast = true;
        }

        public UDP(IPEndPoint endPoint)
        {
            port = endPoint.Port;

            client = new UdpClient(endPoint);
            client.EnableBroadcast = true;
        }

        public UDP(UdpClient UDPClient, int selectedPort)
        {
            port = selectedPort;

            client = UDPClient;
        }

        public DateTime Send(Packet packet)
        {
            sendedBytes = packet.Serialize();
            client.Send(sendedBytes, sendedBytes.Length, new IPEndPoint(IPAddress.Broadcast, port));

            return DateTime.Now;
        }

        public DateTime Send(Packet packet, IPEndPoint receiverEndPoint)
        {
            sendedBytes = packet.Serialize();
            client.Send(sendedBytes, sendedBytes.Length, receiverEndPoint);

            return DateTime.Now;
        }

        public Packet Receive(ref IPEndPoint senderEndPoint)
        {
            return Receive(ref senderEndPoint, 200);
        }

        public Packet Receive(ref IPEndPoint senderEndPoint, int millisecondsTimeout)
        {
            bool received = false;

            IAsyncResult ar = client.BeginReceive(null, null);

            do
            {
                received = ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
            }
            while (received == false);
                
            receivedBytes = client.EndReceive(ar, ref senderEndPoint);

            return Packet.Deserialize(receivedBytes);
        }
    }
}
