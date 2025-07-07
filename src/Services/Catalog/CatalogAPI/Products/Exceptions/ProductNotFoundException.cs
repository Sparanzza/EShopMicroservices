using BuildingBlocks.Exceptions;

namespace CatalogAPI.Products.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(Guid id) : base("Product", id)
    {
    }
    
    public ProductNotFoundException(string name) : base("Product", name)
    {
    }
}