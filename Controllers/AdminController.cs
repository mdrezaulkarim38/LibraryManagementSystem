using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Controllers;
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly LibraryContext _context;

    public AdminController(LibraryContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var users = _context.Users.ToList();
        var books = _context.Books.Include(b => b.Category).ToList();
        var borrowRequests = _context.BorrowRequests.Include(r => r.User).Include(r => r.Book).ToList(); // Corrected
        var returnRequests = _context.BorrowedBooks.Include(r => r.User).Include(r => r.Book).ToList(); // Corrected

        ViewBag.Users = users;
        ViewBag.Books = books;
        ViewBag.BorrowRequests = borrowRequests;
        ViewBag.ReturnRequests = returnRequests;

        return View();
    }



    [HttpPost]
    public async Task<IActionResult> DeactivateUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Status = 0;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index","Admin"); 
    }

    [HttpPost]
    public async Task<IActionResult> ActivateUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        user.Status = 1;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Admin");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ApproveBorrowRequest(int requestId)
    {
        var request = await _context.BorrowRequests.FindAsync(requestId);

        if (request == null || request.Status != "Pending")
        {
            return BadRequest("Invalid request.");
        }

        var book = await _context.Books.FindAsync(request.BookId);
        if (book == null || book.AvailableCopies <= 0)
        {
            return BadRequest("Book not available.");
        }

        request.Status = "Approved";
        book.AvailableCopies--;

        // Create a borrowed book record
        var borrowedBook = new BorrowedBook
        {
            UserId = request.UserId,
            BookId = request.BookId,
            BorrowedDate = DateTime.Now,
            ReturnDueDate = DateTime.Now.AddDays(14) // Example 14-day return period
        };

        _context.BorrowedBooks.Add(borrowedBook);
        await _context.SaveChangesAsync();

        return Ok("Borrow request approved.");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ReturnBook(int borrowId)
    {
        var borrow = await _context.BorrowedBooks.FindAsync(borrowId);

        if (borrow == null || borrow.IsReturned)
        {
            return BadRequest("Invalid borrow record.");
        }

        borrow.IsReturned = true;
        var book = await _context.Books.FindAsync(borrow.BookId);
        book.AvailableCopies++;

        await _context.SaveChangesAsync();

        return Ok("Book returned.");
    }


}
