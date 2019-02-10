using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using static Common.Unit;

namespace Client.Util
{
    class UnitDecoder
    {
        internal static Unit DecodeUnit(byte[] receivedBuffer)
        {
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
            double longitude = BitConverter.ToDouble(fieldByteArray, 0);

            // latitude
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, doubleFieldLength);
            currentReceivedBufferIndex += doubleFieldLength;
            double latitude = BitConverter.ToDouble(fieldByteArray, 0);

            // altitude
            fieldByteArray = new byte[floatFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, floatFieldLength);
            currentReceivedBufferIndex += floatFieldLength;
            float altitude = BitConverter.ToSingle(fieldByteArray, 0);

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
            HostilityEnum hostility = HostilityEnum.UNSPECIFIED;
            switch (hostilityIndex)
            {
                case 1:
                    hostility = HostilityEnum.UNKNOWN;
                    break;
                case 2:
                    hostility = HostilityEnum.FRIEND;
                    break;
                case 3:
                    hostility = HostilityEnum.NEUTRAL;
                    break;
                case 4:
                    hostility = HostilityEnum.HOSTILE;
                    break;
            }

            // status ammo
            fieldByteArray = new byte[byteFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
            currentReceivedBufferIndex += byteFieldLength;
            byte statusAmmo = fieldByteArray[0];

            // status personel
            fieldByteArray = new byte[byteFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
            currentReceivedBufferIndex += byteFieldLength;
            byte statusPersonel = fieldByteArray[0];

            // status weapons
            fieldByteArray = new byte[byteFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
            currentReceivedBufferIndex += byteFieldLength;
            byte statusWeapons = fieldByteArray[0];

            // status POL
            fieldByteArray = new byte[byteFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, byteFieldLength);
            currentReceivedBufferIndex += byteFieldLength;
            byte statusPOL = fieldByteArray[0];

            // equipment type
            stringFieldLength = receivedBuffer[currentReceivedBufferIndex];
            currentReceivedBufferIndex++;
            fieldByteArray = new byte[stringFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, stringFieldLength);
            string equipmentType = Encoding.ASCII.GetString(fieldByteArray);

            Unit unit = new Unit
            {
                Designation = designation,
                Telemetry = new Telemetry(longitude, latitude, altitude),
                StaffComment = staffComment,
                AdditionalInfo = additionalInfo,
                Hostility = hostility,
                StatusAmmo = statusAmmo,
                StatusPersonel = statusPersonel,
                StatusWeapons = statusWeapons,
                StatusPOL = statusPOL,
                EquipmentType = equipmentType
            };

            return unit;
        }
    }
}
