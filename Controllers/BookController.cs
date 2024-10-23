using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

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
    [AllowAnonymous]
    public IActionResult Index()
    {
        var books = _context.Books.ToList();
        ViewBag.Message = TempData["Message"];
        return View(books);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult NewBook()
    {
        var books = _context.Books.ToList();
        ViewBag.Book = books;
        ViewBag.Categories = _context.Categories.ToList();
        return View(new LibraryManagementSystem.Models.Book());
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateBook(Book newBook, IFormFile? BookImage)
    {
        if (ModelState.IsValid)
        {
            try
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
            catch (Exception ex)
            {
                // Log the error (you can use a logging framework here)
                ModelState.AddModelError("", $"An error occurred while saving the book: {ex.Message}");
            }
        }

        ViewBag.Book = _context.Books.ToList();
        ViewBag.Categories = _context.Categories.ToList();
        return View("NewBook", newBook);
    }


    //[HttpPost]
    //public async Task<IActionResult> CreateBook(Book newBook, IFormFile? BookImage)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        if (BookImage != null && BookImage.Length > 0)
    //        {
    //            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books");
    //            var fileName = Guid.NewGuid() + Path.GetExtension(BookImage.FileName);
    //            var filePath = Path.Combine(uploadDir, fileName);

    //            using (var stream = new FileStream(filePath, FileMode.Create))
    //            {
    //                await BookImage.CopyToAsync(stream);
    //            }

    //            newBook.ImagePath = $"/images/books/{fileName}";
    //        }

    //        _context.Books.Add(newBook);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction("NewBook");
    //    }

    //    ViewBag.Book = _context.Books.ToList();
    //    ViewBag.Categories = _context.Categories.ToList();
    //    return View("NewBook", newBook); 
    //}
    [Authorize(Roles = "Admin")]
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
            return RedirectToAction("NewBook");
        }
        ViewBag.Categories = _context.Categories.ToList();
        return View(book);
    }




    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            if (!string.IsNullOrEmpty(book.ImagePath))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", book.ImagePath.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("NewBook");
    }



    [Authorize(Roles = "Admin")]
    public IActionResult CategoryList()
    {
        var categories = _context.Categories.ToList();
        return View(categories);
    }

    [Authorize(Roles = "Admin")]
    public IActionResult CreateCategory()
    {
        return View();
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    public IActionResult EditCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            return NotFound();
        }
        return View(category);
    }
    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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

    [Authorize]
    public async Task<IActionResult> BorrowBook(int bookId)
    {
        var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

        if (user.Status == 0)
        {
            TempData["Message"] = "Your account is not activated.";
            return RedirectToAction("Index");
        }

        var borrowedBooksCount = _context.BorrowedBooks.Count(bb => bb.UserId == user.UserId && bb.IsReturned == false);
        if (borrowedBooksCount >= 3)
        {
            TempData["Message"] = "You have already borrowed 3 books. Return a book to borrow a new one.";
            return RedirectToAction("Index");
        }

        var book = await _context.Books.FindAsync(bookId);
        if (book == null || (book.TotalCopies - book.AvailableCopies) <= 0)
        {
            TempData["Message"] = "The book is out of stock.";
            return RedirectToAction("Index");
        }

        var existingBorrowedBook = await _context.BorrowedBooks
            .FirstOrDefaultAsync(bb => bb.UserId == user.UserId && bb.BookId == bookId && !bb.IsReturned);

        if (existingBorrowedBook != null)
        {
            TempData["Message"] = "You have already borrowed this book.";
            return RedirectToAction("Index");
        }

        var borrowRequest = new BorrowRequest
        {
            UserId = user.UserId,
            BookId = book.BookId,
            RequestDate = DateTime.Now,
            Status = "Pending"
        };

        _context.BorrowRequests.Add(borrowRequest);
        await _context.SaveChangesAsync();

        TempData["Message"] = "Borrow request sent to admin.";
        return RedirectToAction("Index");
    }





    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}