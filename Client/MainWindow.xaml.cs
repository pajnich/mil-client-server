using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SnapWindowLeft();
        }

        private void SnapWindowLeft()
        {
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }

        void MainWindow_ContentRendered(object e, EventArgs eventArgs)
        {
            Console.WriteLine("CLIENT: MainWindow_ContentRendered");
            StartListeningAsync();
        }

        async void StartListeningAsync()
        {
            await Task.Run(() => HeavyMethod(this));
        }

        internal void HeavyMethod(MainWindow gui)
        {
            // create socket listener
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener tcpListener = new TcpListener(ip, 8080);
            TcpClient tcpClient = default(TcpClient);

            // start socket listener
            try
            {
                tcpListener.Start();
                Console.WriteLine("CLIENT: Socket listener started.");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            // read message from socket
            while (true)
            {
                tcpClient = tcpListener.AcceptTcpClient();

                byte[] receivedBuffer = new byte[100];
                NetworkStream networkStream = tcpClient.GetStream();

                networkStream.Read(receivedBuffer, 0, receivedBuffer.Length);

                string message = Encoding.ASCII.GetString(receivedBuffer, 0, receivedBuffer.Length);

                Console.WriteLine("CLIENT: Received message: " + message);

                gui.Dispatcher.Invoke(() =>
                {
                    // UI operations go inside of Invoke
                    TextBox_ReceivedData.Text += message;
                });

                // Heavy operations go outside of Invoke
                System.Threading.Thread.Sleep(51);
            }
        }
    }
}
