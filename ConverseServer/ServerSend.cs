using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class ServerSend
    {
        public static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        public static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayer; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }

        public static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayer; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }

            }
        }

        public static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayer; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }

        public static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.maxPlayer; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i]?.udp.SendData(_packet);
                }

            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }


        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.position);
                _packet.Write(_player.rotation);

                SendTCPData(_toClient, _packet);
            }
        }

        public static void PlayerPosition(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerPosition))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.position);
                _packet.Write(_player.animatorState);

                SendUDPDataToAll(_player.id, _packet);
            }
        }

        public static void PlayerRotation(Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.playerRotation))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.rotation);

                SendUDPDataToAll(_player.id, _packet);

            }
        }

        public static void SendMessage(string _username, string _message)
        {
            using (Packet _packet = new Packet((int)ServerPackets.message))
            {
                _packet.Write(_username);
                _packet.Write(_message);

                SendTCPDataToAll(_packet);
            }
        }

        public static void DanceMusic(int _id)
        {
            using (Packet _packet = new Packet((int)ServerPackets.danceMusic))
            {
                _packet.Write(_id);

                SendUDPDataToAll(_packet);
            }
        }

        public static void DestroyPlayer(int _id)
        {
            using (Packet _packet = new Packet((int)ServerPackets.destroyPlayer))
            {
                _packet.Write(_id);
                SendUDPDataToAll(_id, _packet);
            }
        }

        #endregion
    }
}
