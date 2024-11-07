using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace Titan_Biometric.EFCore
{
    [Table("tbl_UserInfo")]
    public class UserInfo
    {
        [Key,Required]
        public int RID { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        [NotNull]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int Height { get; set; }

        public string HeightUnit { get; set; } = string.Empty;

        public decimal Weight { get; set; }

        public string WeightUnit { get; set; } = string.Empty;

        public decimal BMI { get; set; }

        public string Gender { get; set; } = string.Empty;

        public DateTime DOB { get; set; }

        public int SkinTone { get; set; }

        [NotNull]
        public string Password { get; set; } = string.Empty;

        public bool IsEmailVarified { get; set; }

        public bool IsPhoneVarified { get; set; }

    }
}
