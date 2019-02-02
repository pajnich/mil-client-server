using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Button_SendData.Click += Button_SendData_Click;
        }

        private void Button_SendData_Click(object sender, RoutedEventArgs e)
        {
            TcpClient tcpClient = new TcpClient("localhost", 8080);

            int byteCount = 0;
            byteCount = Encoding.ASCII.GetByteCount(TextBox_Designation.Text);

            byte[] dataToSend = new byte[byteCount];
            dataToSend = Encoding.ASCII.GetBytes(TextBox_Designation.Text);

            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(dataToSend, 0, dataToSend.Length);
            networkStream.Close();

            tcpClient.Close();
        }
    }
}
