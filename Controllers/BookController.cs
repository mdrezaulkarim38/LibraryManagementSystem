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
        return View(new LibraryManagementSystem.Models.Book());
    }

    [HttpPost]
    public IActionResult CreateBook(Book newBook)
    {
        if (ModelState.IsValid)
        {
            _context.Books.Add(newBook);
            _context.SaveChanges();
            return RedirectToAction("NewBook"); // Redirect to the same page after adding the book
        }

        var books = _context.Books.ToList(); // Get the list of books again to display
        return View("NewBook", books); // Return the view with the books list if the model state is invalid
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}