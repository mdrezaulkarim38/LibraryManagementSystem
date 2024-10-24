using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.Models
{
    public class User
    {
        public User()
        {
            Status = 0; // Set default status to 0 (pending approval)
            MembershipStartDate = DateTime.Now; // Automatically set membership start date
        }

        public int UserId { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? Role { get; set; } // e.g. "Admin" or "Member"

        public DateTime MembershipStartDate { get; set; }
        public DateTime? MembershipEndDate { get; set; }

        [Required]
        public string? Phone { get; set; }

        [Required]
        public string? NIDNumber { get; set; }

        [Required]
        public int? Status { get; set; } // Default is 0 (pending approval)
    }
}
