using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Titan_Biometric.EFCore
{
    [Table("tbl_SensorData")]
    public class SensorData
    {
        [Key,Required]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public int LedOne { get; set; }
        public int LedTwo { get; set; }
        public int LedThree { get; set; }
        public int LedFour { get; set; }
        public int BatteryVoltage { get; set; }

        public DateTime DateTime { get; set; }

        public string SessionId { get; set; } = string.Empty;

        public long TimeStampMSB { get; set; }

        public long TimeStampLSB { get; set; }
    }
}
