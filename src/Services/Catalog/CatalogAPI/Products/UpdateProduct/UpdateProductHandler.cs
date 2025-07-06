using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using Marten;

namespace CatalogAPI.Products.GetProducts;

public record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    List<string> Categories,
    string ImageFile,
    decimal Price
) : ICommand<UpdateProductResult>;

public record UpdateProductResult(bool IsSuccess);

public class UpdateProductQueryHandler(IDocumentSession session, ILogger<UpdateProductQueryHandler> logger)
    : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProducts handler called {@Command}", command);
        var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
        if (product == null)
        {
            logger.LogWarning("Product with ID {Id} not found", command.Id);
            return new UpdateProductResult(false);
        }

        product.Name = command.Name;
        product.Description = command.Description;
        product.Categories = command.Categories;
        product.ImageFile = command.ImageFile;
        product.Price = command.Price;

        session.Update(product);
        await session.SaveChangesAsync(cancellationToken);
        
        return new UpdateProductResult(true);
    }
}