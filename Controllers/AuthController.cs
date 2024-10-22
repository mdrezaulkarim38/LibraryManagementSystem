using System.Security.Claims;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Text;
using System.Security.Cryptography;

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

    [HttpGet("Login")]
    public IActionResult Login()
    {
        return View();
    }
    [HttpGet("Register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the hardcoded admin credentials are used
            if (model.Email == "admin" && model.Password == "admin@123")
            {
                // Create claims for the hardcoded admin user
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Admin User"),
                new Claim(ClaimTypes.Email, "admin@example.com"),
                new Claim(ClaimTypes.Role, "Admin") // Explicitly set the role as "Admin"
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                // Sign in the hardcoded admin user
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home"); // Redirect to homepage after successful admin login
            }

            // Check if the user exists in the database for non-admin users
            var user = _context.Users.SingleOrDefault(u => u.Email == model.Email && u.Password == model.Password);

            if (user != null)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role ?? "Member") // Assign role from the database
            };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home"); // Redirect to homepage after successful user login
            }

            // If login failed, show an error
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        return View(model);
    }



    // POST: Register
    [HttpPost("Register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if the email already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Email is already taken.");
                return View(model);
            }

            // Ensure password and confirmation match
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
                return View(model);
            }

            // Create new user
            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                NIDNumber = model.NIDNumber,
                Password = HashPassword(model.Password), // Hash the password
                Role = "Member",
                MembershipStartDate = DateTime.Now,
                MembershipEndDate = null // Optionally set this if you implement membership expiration
            };

            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", "An error occurred while registering the user.");
                    return View(model);
                }
            }

            // Redirect to login after successful registration
            return RedirectToAction("Login", "User");
        }

        return View(model);
    }

    // Method to hash password (you can use bcrypt or ASP.NET Core Identity utilities)
    private string HashPassword(string password)
    {
        // Implement proper password hashing here (e.g., bcrypt or SHA256)
        return password; // Replace with actual hash implementation
    }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}
