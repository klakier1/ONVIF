using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace OnvifLib
{
    class ProbeTask
    {
        UdpClient udp = new UdpClient(55363);
    }
}
