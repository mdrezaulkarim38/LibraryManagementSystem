using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using LibraryManagementSystem.Models.ViewModels;

namespace LibraryManagementSystem.Controllers
{
    public class UserController : Controller
    {
        private readonly LibraryContext _context;

        public UserController(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            var borrowedBooks = await _context.BorrowedBooks
                .Include(bb => bb.Book) 
                .Where(bb => bb.UserId == user.UserId && !bb.IsReturned)
                .ToListAsync();

            // Fetch borrow requests for the user
            var borrowRequests = await _context.BorrowRequests
                .Include(br => br.Book) // Include book details
                .Where(br => br.UserId == user.UserId)
                .ToListAsync();

            var viewModel = new UserBorrowHistoryViewModel
            {
                BorrowedBooks = borrowedBooks,
                BorrowRequests = borrowRequests
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ReturnBook(int borrowedBookId)
        {
            var borrowedBook = await _context.BorrowedBooks.FindAsync(borrowedBookId);
            if (borrowedBook == null)
            {
                return NotFound("Borrowed book not found.");
            }

            borrowedBook.IsReturned = true;
            _context.BorrowedBooks.Update(borrowedBook);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Book returned successfully.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelBorrowRequest(int borrowRequestId)
        {
            var borrowRequest = await _context.BorrowRequests.FindAsync(borrowRequestId);
            if (borrowRequest == null)
            {
                return NotFound("Borrow request not found.");
            }

            // Optionally, you might want to check if the request can be cancelled based on its status.

            _context.BorrowRequests.Remove(borrowRequest);
            await _context.SaveChangesAsync();

            TempData["Message"] = "Borrow request cancelled successfully.";
            return RedirectToAction("Index");
        }
    }
}
