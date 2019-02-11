using System.Net.Sockets;
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
