using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using Marten;

namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductCommand(Guid Id ) : ICommand<DeleteProductResult>;

public record DeleteProductResult(bool IsSuccess);

public class DeleteProductQueryHandler(IDocumentSession session, ILogger<DeleteProductQueryHandler> logger)
    : ICommandHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProducts handler called {@Command}", command);
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        
        return new DeleteProductResult(true);
    }
}