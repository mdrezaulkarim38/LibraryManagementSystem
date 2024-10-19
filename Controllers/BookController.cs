using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Controllers;
public class BookController : Controller
{
    private readonly ILogger<BookController> _logger;
    private readonly LibraryContext _context;

    public BookController(ILogger<BookController> logger, LibraryContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult NewBook()
    {
        var books = _context.Books.ToList();
        ViewBag.Book = books;
        ViewBag.Categories = _context.Categories.ToList();
        return View(new LibraryManagementSystem.Models.Book());
    }

    [HttpPost]
    public IActionResult CreateBook(Book newBook, string newCategory)
    {
        if (ModelState.IsValid)
        {
            if (string.IsNullOrWhiteSpace(newCategory))
            {
                // If a category is selected, use that CategoryId
                newBook.CategoryId = Convert.ToInt32(Request.Form["categorySelect"]);
            }
            else
            {
                // If a new category is provided, add it to the database
                var category = new Category { Name = newCategory };
                _context.Categories.Add(category);
                _context.SaveChanges();

                // Set the new category ID for the book
                newBook.CategoryId = category.CategoryId;
            }

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return RedirectToAction("NewBook");
        }

        // Reload categories and books if model state is invalid
        ViewBag.Book = _context.Books.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        return View("NewBook", newBook); // Pass the newBook model to show validation errors
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}