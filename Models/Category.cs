using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models;
public class Category
{
    public int CategoryId { get; set; }
    [Required(ErrorMessage = "Category name is required")]
    public string? Name { get; set; }
    public ICollection<Book>? Books { get; set; }
}
 