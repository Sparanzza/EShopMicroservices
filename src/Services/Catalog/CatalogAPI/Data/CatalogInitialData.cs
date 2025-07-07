using CatalogAPI.Models;
using Marten;
using Marten.Schema;

namespace CatalogAPI.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        await using var session = store.LightweightSession();

        if (await session.Query<Product>().AnyAsync(token: cancellation))
            return;

        session.Store<Product>(GetPreconfiguredProducts());
        await session.SaveChangesAsync(cancellation);
    }

    private IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
    {
        new Product
        {
            Id = new Guid("b786103d-c621-4f5a-b498-23452610f88c"),
            Name = "HTC U11+ Plus",
            Description = "This phone is the company's biggest change to its f",
            ImageFile = "product-5.png",
            Price = 380.00M,
            Categories = ["Smart Phone"]
        },
        new Product
        {
            Id = new Guid("c4bbc4a2-4555-45d8-97cc-2a99b2167bff"),
            Name = "LG G7 ThinQ",
            Description = "This phone is the company's biggest change to its f",
            ImageFile = "product-6.png",
            Price = 240.00M,
            Categories = ["Home Kitchen"]
        },
        new Product
        {
            Id = new Guid("93170c85-7795-489c-8e8f-7dcf3b4f4188"),
            Name = "Panasonic Lumix",
            Description = "This phone is the company's biggest change to its f",
            ImageFile = "product-6.png",
            Price = 240.00M,
            Categories = ["Camera"]
        },
        new Product
        {
            Id = new Guid("a1b2c3d4-e5f6-7890-1234-567890abcdef"),
            Name = "Samsung Galaxy Watch 6",
            Description = "This phone is the company's biggest change to its f",
            ImageFile = "product-7.png",
            Price = 350.00M,
            Categories = ["Smart Watch", "Electronics"]
        },
        new Product
        {
            Id = new Guid("f0e9d8c7-b6a5-4321-fedc-ba9876543210"),
            Name = "Sony WH-1000XM5",
            Description = "This phone is the company's biggest change to its f",
            ImageFile = "product-8.png",
            Price = 399.99M,
            Categories = ["Audio", "Headphones"]
        },
        new Product
        {
            Id = new Guid("12345678-90ab-cdef-0123-456789abcdef"),
            Name = "Instant Pot Duo",
            Description = "This phone is the company's biggest change to its f",
            ImageFile = "product-9.png",
            Price = 120.50M,
            Categories = ["Home Kitchen", "Appliances"]
        }
    };
}