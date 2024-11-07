using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Titan_Biometric.EFCore
{
    [Table("tbl_SessionEvents")]
    public class Events
    {
        [Key,Required]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string SessionId { get; set; } = string.Empty;
        public DateTime EventDate { get; set; }
        public TimeSpan EventTime { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
    }
}
