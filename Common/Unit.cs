using System.Configuration;

namespace Common
{
    class Unit
    {
        public enum HostilityEnum : sbyte
        {
            /// <summary>
            /// Unspecified Hostility status
            /// </summary>
            UNSPECIFIED = -1,
            /// <summary>
            /// Unknown Hostility status
            /// </summary>
            UNKNOWN = 0,
            /// <summary>
            /// Friendly status
            /// </summary>
            FRIEND,
            /// <summary>
            /// Neutral status
            /// </summary>
            NEUTRAL,
            /// <summary>
            /// Hostile status
            /// </summary>
            HOSTILE
        }

        public ulong Id { get; set; }

        [StringValidator(MaxLength = 21)]
        public string Designation { get; set; }
        
        public Telemetry Telemetry { get; set; }

        [StringValidator(MaxLength = 20)]
        public string StaffComment { get; set; }

        [StringValidator(MaxLength = 20)]
        public string AdditionalInfo { get; set; }

        public HostilityEnum Hostility { get; set; }

        [IntegerValidator(MinValue = 0, MaxValue = 100)]
        public byte StatusAmmo { get; set; }

        [IntegerValidator(MinValue = 0, MaxValue = 100)]
        public byte StatusPersonel { get; set; }

        [IntegerValidator(MinValue = 0, MaxValue = 100)]
        public byte StatusWeapons { get; set; }

        [IntegerValidator(MinValue = 0, MaxValue = 100)]
        public byte StatusPOL { get; set; }

        [StringValidator(MaxLength = 20)]
        public string EquipmentType { get; set; }
    }
}
