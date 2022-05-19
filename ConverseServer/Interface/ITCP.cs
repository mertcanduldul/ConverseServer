using System;
using System.Net.Sockets;
using Server;

namespace ConverseServer.Interface
{
    public interface ITCP
    {
        public void Connect(TcpClient _socket);
        public void SendData(Packet packet);
        public bool HandleData(byte[] _data);
        public void Disconnect();
    }
}

