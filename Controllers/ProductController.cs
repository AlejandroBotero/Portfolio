
using Microsoft.AspNetCore.Mvc;
using newmvc.Models;
using newmvc.Data;
using newmvc.Models.ViewModels;
using System.IO;
using System.Runtime.CompilerServices;

namespace newmvc.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    public ProductController(ILogger<ProductController> logger, ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _logger = logger;
        _context = context;
        _environment = environment;
    }

    public IActionResult Products()
    {
        var products = _context.Products.ToList();
        return View(products);
    }
    public IActionResult CreateProduct()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductViewModel productviewmodel)
    {
        Console.WriteLine("Entered Controller");
        string? imagePath = null;
        if (ModelState.IsValid)
        {
            Console.WriteLine(productviewmodel.ImageFile.Name);
            if (productviewmodel.ImageFile != null)
            {
                Console.WriteLine("image is not null");
                string wwwRootPath = _environment.WebRootPath;
                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(productviewmodel.ImageFile.Name);
                string finalpath = Path.Combine(wwwRootPath, "images", uniqueFileName);

                using (var fileStream = new FileStream(finalpath, FileMode.Create))
                {
                    await productviewmodel.ImageFile.CopyToAsync(fileStream);
                }
                imagePath = "/images/" + uniqueFileName;
                Console.WriteLine(imagePath);
            }
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                Name = productviewmodel.Name,
                Price = productviewmodel.Price,
                Description = productviewmodel.Description,
                ImagePath = productviewmodel.ImageFile != null ? imagePath : null,
                ImageType = productviewmodel.ImageFile?.ContentType,
                CreationDate = DateTime.Now
            };
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Products");
        }
        _logger.LogWarning("Invalid product data submitted.");
        return View();
    }

    public IActionResult Details(Guid id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(product);
    }
}