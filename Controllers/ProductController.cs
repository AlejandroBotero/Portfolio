
using Microsoft.AspNetCore.Mvc;
using newmvc.Models;
using newmvc.Data;
using newmvc.Models.ViewModels;
using newmvc.Models.ViewModels.Mappers;

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
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel productviewmodel)
    {
        Console.WriteLine("Entered Controller");
        string? imagePath = null;
        if (ModelState.IsValid)
        {
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

    public IActionResult EditDeleteDetails(Guid id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        return View(ProductMapper.ToViewModel(product));
    }

    public IActionResult EditDelete()
    {
        List<Product> products = _context.Products.ToList();
        return View(products);
    }

    public IActionResult Delete(Guid id)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == id);
        if (product == null)
        {
            return NotFound();
        }
        _context.Products.Remove(product);
        _context.SaveChanges();
        return RedirectToAction("EditDelete");
    }

    [HttpPost]
    public async Task<IActionResult> Update(ProductViewModel productviewmodel)
    {
        var existingProduct = await _context.Products.FindAsync(productviewmodel.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = productviewmodel.Name;
            existingProduct.Price = productviewmodel.Price;
            existingProduct.Description = productviewmodel.Description;
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Products");
    }
}
