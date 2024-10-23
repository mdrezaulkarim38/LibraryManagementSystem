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
        var borrowRequests = _context.BorrowRequests.Include(r => r.User).Include(r => r.Book).Where(r => r.Status == "Pending").ToList();
        var returnRequests = _context.BorrowedBooks.Include(r => r.User).Include(r => r.Book).Where(r => r.IsUserRequestReturn).ToList();

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
            return RedirectToAction("Index", "Admin");
        }

        var book = await _context.Books.FindAsync(request.BookId);
        if (book == null || (book.TotalCopies - book.AvailableCopies) <= 0)
        {
            return RedirectToAction("Index", "Admin");
        }

        request.Status = "Approved";
        book.AvailableCopies++;

        var borrowedBook = new BorrowedBook
        {
            UserId = request.UserId,
            BookId = request.BookId,
            BorrowedDate = DateTime.Now,
            ReturnDueDate = DateTime.Now.AddDays(14) 
        };

        _context.BorrowedBooks.Add(borrowedBook);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Admin");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ReturnBook(int borrowId)
    {
        var borrow = await _context.BorrowedBooks.FindAsync(borrowId);

        if (borrow == null || borrow.IsUserRequestReturn == false)
        {
            return RedirectToAction("Index", "Admin");
        }

        borrow.IsReturned = true;
        var book = await _context.Books.FindAsync(borrow.BookId);
        book.AvailableCopies--;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Admin");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> RejectReturnBook(int borrowId)
    {
        var borrow = await _context.BorrowedBooks.FindAsync(borrowId);

        if (borrow == null || borrow.IsUserRequestReturn == false)
        {
            return RedirectToAction("Index", "Admin");
        }

        borrow.IsUserRequestReturn = false;
        var book = await _context.Books.FindAsync(borrow.BookId);
        book.AvailableCopies++;

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Admin");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> RejectBorrowRequest(int borrowRequestId)
    {
        var borrowRequest = await _context.BorrowRequests.FindAsync(borrowRequestId);
        if (borrowRequest == null)
        {
            return NotFound("Borrow request not found.");
        }
        _context.BorrowRequests.Remove(borrowRequest);
        await _context.SaveChangesAsync();

        TempData["Message"] = "Borrow request cancelled successfully.";
        return RedirectToAction("Index");
    }
}
