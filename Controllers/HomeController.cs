using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using newmvc.Models;

namespace newmvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Products()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Products(Product product)
    {
        if (ModelState.IsValid)
        {
            // Save the product to the database or perform other actions
            // _logger.LogInformation("Product created: {Name}, Price: {Price}, Description: {Description}", product.Name, product.Price, product.Description);
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }
        return View(product);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
