using System;
using System.Collections.Generic;
using System.Text;

namespace Server
{
    class Constants
    {
        public static int dataBufferSize = 4096;
        public const int TICKS_PER_SEC = 50;
        public const int MS_PER_TICK = 1000 / TICKS_PER_SEC;
    }
}
