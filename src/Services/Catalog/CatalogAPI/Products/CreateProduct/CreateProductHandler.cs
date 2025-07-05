using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using Marten;

namespace CatalogAPI.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    string Description,
    List<string> Categories,
    string ImageFile,
    decimal Price
): ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Categories = request.Categories,
            ImageFile = request.ImageFile,
            Price = request.Price
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id);
        
    }
}