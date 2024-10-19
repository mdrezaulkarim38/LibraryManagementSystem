using System.ComponentModel.DataAnnotations;
namespace LibraryManagementSystem.Models;
public class Book
{
    public int BookId { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Description { get; set; }
    public int TotalCopies { get; set; }
    public int AvailableCopies { get; set; }
    public DateTime PublishedDate { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; } 
}
