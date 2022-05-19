using System;
using System.Net;
using ConverseServer.Interface;
using Server;

namespace ConverseServer.Communication
{
    public class UDP : IUDP
    {
        public IPEndPoint endPoint;

        private int id;

        public UDP(int _id)
        {
            id = _id;
        }

        public void Connect(IPEndPoint _endPoint)
        {
            endPoint = _endPoint;
        }

        public void SendData(Packet _packet)
        {
            Server.Server.SendUDPData(endPoint, _packet);
        }

        public void HandleData(Packet _packet)
        {
            int packetLength = _packet.ReadInt();
            byte[] packetBytes = _packet.ReadBytes(packetLength);

            ThreadManager.ExecuteOnMainThread(() =>
            {

                using (Packet _packet = new Packet(packetBytes))
                {
                    int packetId = _packet.ReadInt();
                    Server.Server.packetHandlers[packetId](id, _packet);
                }

            });
        }

        public void Disconnect()
        {
            endPoint = null;
        }
    }
}

