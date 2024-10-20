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
        var books = _context.Books.ToList();
        return View(books);
    }

    public IActionResult NewBook()
    {
        var books = _context.Books.ToList();
        ViewBag.Book = books;
        ViewBag.Categories = _context.Categories.ToList();
        return View(new LibraryManagementSystem.Models.Book());
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook(Book newBook, IFormFile? BookImage)
    {
        if (ModelState.IsValid)
        {
            if (BookImage != null && BookImage.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");
                var fileName = Guid.NewGuid() + Path.GetExtension(BookImage.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await BookImage.CopyToAsync(stream);
                }

                newBook.ImagePath = $"/images/books/{fileName}";
            }

            _context.Books.Add(newBook);
            await _context.SaveChangesAsync();
            return RedirectToAction("NewBook");
        }

        ViewBag.Book = _context.Books.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        return View("NewBook", newBook); 
    }

    public IActionResult EditBook(int id)
    {
        var book = _context.Books.Find(id);
        if (book == null)
        {
            return NotFound();
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> EditBook(int id, Book book, IFormFile? BookImage)
    {
        if (ModelState.IsValid)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
            {
                return NotFound();
            }

            existingBook.Title = book.Title; 
            existingBook.Author = book.Author; 
            existingBook.CategoryId = book.CategoryId; 

            if (BookImage != null && BookImage.Length > 0)
            {
                var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");

                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                var fileName = Guid.NewGuid() + Path.GetExtension(BookImage.FileName);
                var filePath = Path.Combine(uploadDir, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await BookImage.CopyToAsync(stream);
                }

                existingBook.ImagePath = $"/images/books/{fileName}"; 
            }

            _context.Update(existingBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(book);
    }




    // POST: Delete a book
    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.ImagePath.TrimStart('/'));
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Index");
    }



    public IActionResult CategoryList()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    public IActionResult CreateCategory()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return RedirectToAction("CategoryList");
        }

        return View(category);
    }

    public IActionResult EditCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> EditCategory(Category category)
    {
        if (ModelState.IsValid)
        {
            var existingCategory = await _context.Categories.FindAsync(category.CategoryId);

            if (existingCategory != null)
            {
                existingCategory.Name = category.Name;
                _context.Update(existingCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("CategoryList");
            }
        }
        return View(category);
    }

    [HttpPost]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("CategoryList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}