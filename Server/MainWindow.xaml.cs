using System;
using System.Globalization;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SnapWindowRight();
            Button_SendData.Click += Button_SendData_Click;
        }

        private void SnapWindowRight()
        {
            this.Top = 0;
            this.Left = 0;
            this.Width = SystemParameters.PrimaryScreenWidth / 2;
            this.Height = SystemParameters.PrimaryScreenHeight;
        }

        private void Button_SendData_Click(object sender, RoutedEventArgs e)
        {
            TcpClient tcpClient = new TcpClient("localhost", 8080);

            int byteCount = 0;
            byteCount = Encoding.ASCII.GetByteCount(TextBox_Designation.Text);

            byte[] dataToSend = new byte[byteCount];
            dataToSend = ExtractDataToSendFromGUI();

            NetworkStream networkStream = tcpClient.GetStream();
            networkStream.Write(dataToSend, 0, dataToSend.Length);
            networkStream.Close();

            tcpClient.Close();
        }

        private byte[] ExtractDataToSendFromGUI()
        {
            byte[] designation = Encoding.ASCII.GetBytes(TextBox_Designation.Text);
            byte[] designationLength = GetByteArrayLengthInBytes(designation);

            byte[] longitude = BitConverter.GetBytes(Convert.ToDouble(TextBox_Longitude.Text));
            byte[] latitude = BitConverter.GetBytes(Convert.ToDouble(TextBox_Latitude.Text));
            byte[] altitude = BitConverter.GetBytes(Convert.ToSingle(TextBox_Altitude.Text, CultureInfo.InvariantCulture.NumberFormat));

            byte[] staffComment = Encoding.ASCII.GetBytes(TextBox_StaffComment.Text);
            byte[] staffCommentLength = GetByteArrayLengthInBytes(staffComment);

            byte[] additionalInfo = Encoding.ASCII.GetBytes(TextBox_AdditionalInfo.Text);
            byte[] additionalInfoLength = GetByteArrayLengthInBytes(additionalInfo);

            byte[] hostility = ExtractHostilityFromGUI();
            byte[] statusAmmo = getNumberFromTextAsOneByteArray(TextBox_StatusAmmo.Text); 
            byte[] statusPersonel = getNumberFromTextAsOneByteArray(TextBox_StatusPersonel.Text);
            byte[] statusWeapons = getNumberFromTextAsOneByteArray(TextBox_StatusWeapons.Text);
            byte[] statusPOL = getNumberFromTextAsOneByteArray(TextBox_StatusPOL.Text);

            byte[] equipmentType = Encoding.ASCII.GetBytes(TextBox_EquipmentType.Text);
            byte[] equipmentTypeLength = GetByteArrayLengthInBytes(equipmentType);

            byte[] dataToSend = (from array
                                    in new[] { designationLength, designation,
                                        longitude, latitude, altitude,
                                        staffCommentLength, staffComment,
                                        additionalInfoLength, additionalInfo, 
                                        hostility, statusAmmo, statusPersonel, statusWeapons, statusPOL,
                                        equipmentTypeLength, equipmentType}
                                 from arr in array select arr)
                                 .ToArray();

            return dataToSend;
        }

        private byte[] getNumberFromTextAsOneByteArray(string text)
        {
            return new byte[1] { BitConverter.GetBytes(Convert.ToByte(text))[0] };
        }

        private byte[] GetByteArrayLengthInBytes(byte[] byteArray)
        {
            return new byte[1] { Convert.ToByte(byteArray.Length) };
        }

        private byte[] ExtractHostilityFromGUI()
        {
            string hostilityStringValue = ComboBox_Hostility.SelectionBoxItem.ToString();
            byte[] hostilityByteArray = new byte[1];

            switch (hostilityStringValue)
            {
                case "UNSPECIFIED":
                    hostilityByteArray[0] = 0;
                    break;
                case "UNKNOWN":
                    hostilityByteArray[0] = 1;
                    break;
                case "FRIEND":
                    hostilityByteArray[0] = 2;
                    break;
                case "NEUTRAL":
                    hostilityByteArray[0] = 3;
                    break;
                case "HOSTILE":
                    hostilityByteArray[0] = 4;
                    break;
            }
            return hostilityByteArray;
        }
    }
}
