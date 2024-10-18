using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models;
public class User
{
    public int UserId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public required string Password { get; set; }
    public string? Role { get; set; } // e.g. "Admin" or "Member"
    public DateTime MembershipStartDate { get; set; }
    public DateTime? MembershipEndDate { get; set; }
    
}