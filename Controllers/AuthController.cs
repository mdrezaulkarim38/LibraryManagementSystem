using System.Security.Claims;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LibraryManagementSystem.Controllers;

[Route("[controller]")]
public class AuthController : Controller
{
    private readonly ILogger<AuthController> _logger;
    private readonly LibraryContext _context;

    public AuthController(ILogger<AuthController> logger, LibraryContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}