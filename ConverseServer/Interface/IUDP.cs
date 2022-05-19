using System;
using System.Net;
using Server;

namespace ConverseServer.Interface
{
    public interface IUDP
    {
        public void Connect(IPEndPoint _endPoint);
        public void SendData(Packet _packet);
        public void HandleData(Packet _packet);
        public void Disconnect();
    }
}

