using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models.ViewModels;

public class RegisterViewModel
{
    [Required]
    public string FullName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string NIDNumber { get; set; }
}
