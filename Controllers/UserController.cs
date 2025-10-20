using Microsoft.AspNetCore.Mvc;
using newmvc.Data;
using newmvc.Models;
using newmvc.Models.ViewModels;

namespace newmvc.Controllers;

public class UserController : Controller
{

    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Users()
    {
        var users = _context.Users.ToList();
        return View(users);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(UserViewModel userviewmodel)
    {
        if (ModelState.IsValid)
        {
            User user = new User
            {
                Name = userviewmodel.Name,
                Surname = userviewmodel.Surname,
                Email = userviewmodel.Email,
                DateOfBirth = userviewmodel.DateOfBirth,
                AccountCreationDate = DateTime.UtcNow
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        return View(userviewmodel);
    }
}