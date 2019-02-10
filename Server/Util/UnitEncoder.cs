using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Server.Util
{
    class UnitEncoder
    {
        internal static byte[] EncodeUnit(Unit unit)
        {
            byte[] designation = Encoding.ASCII.GetBytes(unit.Designation);
            byte[] designationLength = GetByteArrayLengthInBytes(designation);

            byte[] longitude = BitConverter.GetBytes(Convert.ToDouble(unit.Telemetry.Longitude));
            byte[] latitude = BitConverter.GetBytes(Convert.ToDouble(unit.Telemetry.Latitude));
            byte[] altitude = BitConverter.GetBytes(Convert.ToSingle(unit.Telemetry.Altitude));

            byte[] staffComment = Encoding.ASCII.GetBytes(unit.StaffComment);
            byte[] staffCommentLength = GetByteArrayLengthInBytes(staffComment);

            byte[] additionalInfo = Encoding.ASCII.GetBytes(unit.AdditionalInfo);
            byte[] additionalInfoLength = GetByteArrayLengthInBytes(additionalInfo);

            byte[] hostility = EncodeHostility(unit.HostilityEnumValue);
            byte[] statusAmmo = new byte[] { unit.StatusAmmo };
            byte[] statusPersonel = new byte[] { unit.StatusPersonel };
            byte[] statusWeapons = new byte[] { unit.StatusWeapons };
            byte[] statusPOL = new byte[] { unit.StatusPOL };

            byte[] equipmentType = Encoding.ASCII.GetBytes(unit.EquipmentType);
            byte[] equipmentTypeLength = GetByteArrayLengthInBytes(equipmentType);

            byte[] dataToSend = (from array
                                    in new[] { designationLength, designation,
                                        longitude, latitude, altitude,
                                        staffCommentLength, staffComment,
                                        additionalInfoLength, additionalInfo,
                                        hostility, statusAmmo, statusPersonel, statusWeapons, statusPOL,
                                        equipmentTypeLength, equipmentType}
                                 from arr in array
                                 select arr)
                                 .ToArray();

            return dataToSend;
        }

        private static byte[] GetByteArrayLengthInBytes(byte[] byteArray)
        {
            return new byte[1] { Convert.ToByte(byteArray.Length) };
        }

        private static byte[] GetNumberFromTextAsOneByteArray(string text)
        {
            return new byte[1] { BitConverter.GetBytes(Convert.ToByte(text))[0] };
        }

        private static byte[] EncodeHostility(Unit.HostilityEnum hostility)
        {
            byte[] hostilityByteArray = new byte[1];

            switch (hostility)
            {
                case Unit.HostilityEnum.UNSPECIFIED:
                    hostilityByteArray[0] = 0;
                    break;
                case Unit.HostilityEnum.UNKNOWN:
                    hostilityByteArray[0] = 1;
                    break;
                case Unit.HostilityEnum.FRIEND:
                    hostilityByteArray[0] = 2;
                    break;
                case Unit.HostilityEnum.NEUTRAL:
                    hostilityByteArray[0] = 3;
                    break;
                case Unit.HostilityEnum.HOSTILE:
                    hostilityByteArray[0] = 4;
                    break;
            }
            return hostilityByteArray;
        }
    }
}
