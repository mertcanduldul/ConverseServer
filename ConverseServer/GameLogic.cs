using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class GameLogic
    {
        public static void Update()
        {
            foreach (Client client in Server.clients.Values)
            {
                if (client.player != null)
                {
                    client.player.Update();
                }
            }


            ThreadManager.UpdateMain();
        }
    }
}
