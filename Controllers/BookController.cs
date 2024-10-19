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
    public async Task<IActionResult> CreateBook(Book newBook, string newCategory)
    {
        if (ModelState.IsValid)
        {
            if (!string.IsNullOrEmpty(newCategory))
            {
                // Create new category if provided
                var category = new Category { Name = newCategory };
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                newBook.CategoryId = category.CategoryId; // Set the new category ID
            }

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("NewBook");
        }
        var books = _context.Books.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        return View("NewBook", books);
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}