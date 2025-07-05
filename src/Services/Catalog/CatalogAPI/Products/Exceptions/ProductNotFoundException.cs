namespace CatalogAPI.Products.Exceptions;

public class ProductNotFoundException : Exception
{
    public ProductNotFoundException() : base($"Product was not found!")
    {
    }
}