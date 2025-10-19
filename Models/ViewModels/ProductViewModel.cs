namespace newmvc.Models.ViewModels;

public class ProductViewModel
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageType { get; set; }

}