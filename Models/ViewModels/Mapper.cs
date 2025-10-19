namespace newmvc.Models.ViewModels;

public class Mapper
{
    public static ProductViewModel ToViewModel(Product product)
    {
        return new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Description = product.Description,
            ImagePath = product.ImagePath,
            ImageType = product.ImageType
        };
    }

    public static Product ToModel(ProductViewModel productViewModel)
    {
        return new Product
        {
            Id = productViewModel.Id,
            Name = productViewModel.Name,
            Price = productViewModel.Price,
            Description = productViewModel.Description,
            ImagePath = productViewModel.ImagePath,
            ImageType = productViewModel.ImageType
        };
    }
}