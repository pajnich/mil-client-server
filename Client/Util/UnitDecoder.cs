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
        private readonly static int FIELD_LENGTH_DOUBLE = 8;
        private readonly static int FIELD_LENGTH_FLOAT = 4;
        private readonly static int FIELD_LENGTH_BYTE = 1;

        internal static Unit DecodeUnit(byte[] receivedBuffer)
        {
            int currentReceivedBufferIndex = 0;

            // designation
            int stringFieldLength = receivedBuffer[currentReceivedBufferIndex];
            currentReceivedBufferIndex++;
            byte[] fieldByteArray = new byte[stringFieldLength];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, stringFieldLength);
            string designation = Encoding.ASCII.GetString(fieldByteArray);
            currentReceivedBufferIndex += stringFieldLength;

            // longitude
            fieldByteArray = new byte[FIELD_LENGTH_DOUBLE];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_DOUBLE);
            currentReceivedBufferIndex += FIELD_LENGTH_DOUBLE;
            double longitude = BitConverter.ToDouble(fieldByteArray, 0);

            // latitude
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_DOUBLE);
            currentReceivedBufferIndex += FIELD_LENGTH_DOUBLE;
            double latitude = BitConverter.ToDouble(fieldByteArray, 0);

            // altitude
            fieldByteArray = new byte[FIELD_LENGTH_FLOAT];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_FLOAT);
            currentReceivedBufferIndex += FIELD_LENGTH_FLOAT;
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
            fieldByteArray = new byte[FIELD_LENGTH_BYTE];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_BYTE);
            currentReceivedBufferIndex += FIELD_LENGTH_BYTE;
            int hostilityIndex = fieldByteArray[0];
            HostilityEnum hostility;
            switch (hostilityIndex)
            {
                case 0:
                    hostility = HostilityEnum.UNSPECIFIED;
                    break;
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
                default:
                    hostility = HostilityEnum.UNSPECIFIED;
                    break;
            }

            // status ammo
            fieldByteArray = new byte[FIELD_LENGTH_BYTE];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_BYTE);
            currentReceivedBufferIndex += FIELD_LENGTH_BYTE;
            byte statusAmmo = fieldByteArray[0];

            // status personel
            fieldByteArray = new byte[FIELD_LENGTH_BYTE];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_BYTE);
            currentReceivedBufferIndex += FIELD_LENGTH_BYTE;
            byte statusPersonel = fieldByteArray[0];

            // status weapons
            fieldByteArray = new byte[FIELD_LENGTH_BYTE];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_BYTE);
            currentReceivedBufferIndex += FIELD_LENGTH_BYTE;
            byte statusWeapons = fieldByteArray[0];

            // status POL
            fieldByteArray = new byte[FIELD_LENGTH_BYTE];
            Array.Copy(receivedBuffer, currentReceivedBufferIndex, fieldByteArray, 0, FIELD_LENGTH_BYTE);
            currentReceivedBufferIndex += FIELD_LENGTH_BYTE;
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
