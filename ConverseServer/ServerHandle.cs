using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int clientIdCheck = _packet.ReadInt();
            string username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected succesfully and is new player {_fromClient} {username}");
            if (_fromClient != clientIdCheck)
            {
                Console.WriteLine($"Player \"{username}\" (ID: {_fromClient}) has assumed the wrong client ID {clientIdCheck} ");
            }
            Server.clients[_fromClient].SendIntoGame(username);
        }

        public static void PlayerMovements(int _fromClient, Packet _packet)
        {

            Vector3 position = _packet.ReadVector3();
            Quaternion rotation = _packet.ReadQuaternion();
            int animatorState = _packet.ReadInt();

            Server.clients[_fromClient].player.SetMovements(position, rotation, animatorState);
        }

        public static void ReadMessageFromClient(int _fromClient, Packet _packet)
        {
            string username = _packet.ReadString();
            string message = _packet.ReadString();

            ServerSend.SendMessage(username, message);

        }

        public static void DanceMusic(int _fromClient, Packet _packet)
        {
            ServerSend.DanceMusic(_packet.ReadInt());
        }

    }
}
