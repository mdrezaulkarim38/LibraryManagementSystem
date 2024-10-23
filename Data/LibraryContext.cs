using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.Models;
public class LibraryContext : DbContext
{
    public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BorrowRequest> BorrowRequests { get; set; }
    public DbSet<BorrowedBook> BorrowedBooks { get; set; }
    // Add other DbSets for your models

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure the relationship between Book and Category
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId);

        // Additional configurations can be added here
    }
}
