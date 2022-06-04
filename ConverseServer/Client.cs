using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using ConverseServer.Communication;

namespace Server
{
    class Client
    {
        public int id;
        public Player player;
        public TCP tcp;
        public UDP udp;

        public Client(int _clientid)
        {
            id = _clientid;
            tcp = new TCP(id);
            udp = new UDP(id);
        }

        public void SendIntoGame(string _playerName)
        {
            player = new Player(id, _playerName, new Vector3(0, 0, 127));


            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    if (_client.id != id)
                    {
                        ServerSend.SpawnPlayer(id, _client.player);
                    }
                }
            }

            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    ServerSend.SpawnPlayer(_client.id, player);
                }
            }
        }

        public void Disconnect()
        {
            Console.WriteLine($"{tcp.socket.Client.RemoteEndPoint} has disconnected");

            Server.clients[id].tcp.socket.Client = null;
            ServerSend.DestroyPlayer(id);

            player = null;
            tcp.Disconnect();
            udp.Disconnect();
        }
    }
}
