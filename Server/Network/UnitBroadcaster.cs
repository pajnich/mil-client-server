using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using Server.Util;

namespace Server.Network
{
    class UnitBroadcaster
    {
        internal static void BroadcastUnit(Unit unit)
        {
            TcpClient tcpClient = new TcpClient("localhost", 8080);
            
            byte[] dataToSend = UnitEncoder.EncodeUnit(unit);

            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(dataToSend, 0, dataToSend.Length);
            networkStream.Close();

            tcpClient.Close();
        }
    }
}
