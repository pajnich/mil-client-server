using Client.Providers;
using Client.Util;
using Common;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Client.Network
{
    class UnitListeningTask
    {
        public UnitListeningTask(UnitProvider unitProvider)
        {
            ListenForNewUnitsAsync(unitProvider);
        }

        private async void ListenForNewUnitsAsync(UnitProvider unitProvider)
        {
            await Task.Run(() => ListenForNewUnits(unitProvider));
        }

        private void ListenForNewUnits(UnitProvider unitProvider)
        {
            // create socket listener
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener tcpListener = new TcpListener(ip, 8080);
            TcpClient tcpClient = default(TcpClient);

            // start socket listener
            try
            {
                tcpListener.Start();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            // read message from socket
            while (true)
            {
                tcpClient = tcpListener.AcceptTcpClient();

                byte[] receivedBuffer = new byte[200];
                NetworkStream networkStream = tcpClient.GetStream();

                networkStream.Read(receivedBuffer, 0, receivedBuffer.Length);

                Unit unit = UnitDecoder.DecodeUnit(receivedBuffer);

                unitProvider.ProvideUnit(unit);
            }
        }
    }
}
