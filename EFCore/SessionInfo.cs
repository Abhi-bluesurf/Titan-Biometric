using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Titan_Biometric.EFCore
{
    [Table("tbl_SessionInfo")]
    public class SessionInfo
    {
        [Key,Required]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        
        [NotNull]
        public DateTime ActivityDate { get; set; }
        
        public string SessionId { get; set; } = string.Empty;
        public string SessionProtocol { get; set; } = string.Empty;
        public string SensorLocation { get; set; } = string.Empty;
        public decimal StartWeight { get; set; } = 0;
        public decimal EndWeight { get; set; } = 0;
        public string SessionNote { get; set; } = string.Empty;
    }
}
