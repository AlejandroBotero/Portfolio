
namespace newmvc.Models;

public class Product
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public DateTime CreationDate { get; set; }
    public string? ImagePath { get; set; }
    public string? ImageType { get; set; }


}