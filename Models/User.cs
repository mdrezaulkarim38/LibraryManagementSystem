using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; } // e.g. "Admin" or "Member"

        public DateTime MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? NIDNumber { get; set; }
    }
}
