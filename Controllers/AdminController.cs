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

    public async Task<IActionResult> Index()
    {
        // Fetch users and books from the database
        var users = await _context.Users.ToListAsync();
        var books = await _context.Books.Include(b => b.Category).ToListAsync();

        ViewBag.Users = users;
        ViewBag.Books = books;

        return View();
    }
}
