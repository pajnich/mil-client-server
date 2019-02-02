using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SnapWindowLeft();
        }

        void MainWindow_ContentRendered(object e, EventArgs eventArgs)
        {
            StartListeningAsync();
        }

        private void SnapWindowLeft()
        {
            this.Top = 0;
            this.Left = SystemParameters.PrimaryScreenWidth / 2;
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }

        async void StartListeningAsync()
        {
            await Task.Run(() => ListenForData(this));
        }

        internal void ListenForData(MainWindow gui)
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

                int doubleFieldLength = 8;
                int floatFieldLength = 4;
                int byteFieldLength = 1;
                int currentReceivedBufferIndex = 0;

                // designation
                int stringFieldLength = receivedBuffer[currentReceivedBufferIndex];
                currentReceivedBufferIndex++;
                byte[] fieldByteArray = new byte[stringFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, stringFieldLength);
                string designation = Encoding.ASCII.GetString(fieldByteArray);
                currentReceivedBufferIndex += stringFieldLength;

                // longitude
                fieldByteArray = new byte[doubleFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, doubleFieldLength);
                currentReceivedBufferIndex += doubleFieldLength;
                string longitude = BitConverter.ToDouble(fieldByteArray, 0).ToString();

                // latitude
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, doubleFieldLength);
                currentReceivedBufferIndex += doubleFieldLength;
                string latitude = BitConverter.ToDouble(fieldByteArray, 0).ToString();

                // altitude
                fieldByteArray = new byte[floatFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, floatFieldLength);
                currentReceivedBufferIndex += floatFieldLength;
                string altitude = BitConverter.ToSingle(fieldByteArray, 0).ToString();

                // staff comment
                stringFieldLength = receivedBuffer[currentReceivedBufferIndex];
                currentReceivedBufferIndex++;
                fieldByteArray = new byte[stringFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, stringFieldLength);
                string staffComment = Encoding.ASCII.GetString(fieldByteArray);
                currentReceivedBufferIndex += stringFieldLength;

                // additional info
                stringFieldLength = receivedBuffer[currentReceivedBufferIndex];
                currentReceivedBufferIndex++;
                fieldByteArray = new byte[stringFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, stringFieldLength);
                string additionalInfo = Encoding.ASCII.GetString(fieldByteArray);
                currentReceivedBufferIndex += stringFieldLength;

                // hostility
                fieldByteArray = new byte[byteFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
                currentReceivedBufferIndex += byteFieldLength;
                int hostilityIndex = fieldByteArray[0];
                string hostility = "UNSPECIFIED";
                switch (hostilityIndex)
                {
                    case 1:
                        hostility = "UNKNOWN";
                        break;
                    case 2:
                        hostility = "FRIEND";
                        break;
                    case 3:
                        hostility = "NEUTRAL";
                        break;
                    case 4:
                        hostility = "HOSTILE";
                        break;
                }

                // status ammo
                fieldByteArray = new byte[byteFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
                currentReceivedBufferIndex += byteFieldLength;
                string statusAmmo = fieldByteArray[0].ToString();

                // status personel
                fieldByteArray = new byte[byteFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
                currentReceivedBufferIndex += byteFieldLength;
                string statusPersonel = fieldByteArray[0].ToString();

                // status weapons
                fieldByteArray = new byte[byteFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
                currentReceivedBufferIndex += byteFieldLength;
                string statusWeapons = fieldByteArray[0].ToString();

                // status POL
                fieldByteArray = new byte[byteFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
                currentReceivedBufferIndex += byteFieldLength;
                string statusPOL = fieldByteArray[0].ToString();

                // equipment type
                stringFieldLength = receivedBuffer[currentReceivedBufferIndex];
                currentReceivedBufferIndex++;
                fieldByteArray = new byte[stringFieldLength];
                Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, stringFieldLength);
                string equipmentType = Encoding.ASCII.GetString(fieldByteArray);

                // update GUI with received data
                gui.Dispatcher.Invoke(() =>
                {
                    Label_Designation.Content = designation;
                    Label_Longitude.Content = longitude;
                    Label_Latitude.Content = latitude;
                    Label_Altitude.Content = altitude;
                    Label_StaffComment.Content = staffComment;
                    Label_AdditionalInfo.Content = additionalInfo;
                    Label_Hostility.Content = hostility;
                    Label_StatusAmmo.Content = statusAmmo;
                    Label_StatusPersonel.Content = statusPersonel;
                    Label_StatusWeapons.Content = statusWeapons;
                    Label_StatusPOL.Content = statusPOL;
                    Label_EquipmentType.Content = equipmentType;
                });
            }
        }
    }
}
