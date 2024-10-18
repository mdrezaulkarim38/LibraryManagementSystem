using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models;
public class Category
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public ICollection<Book>? Books { get; set; }
}
 