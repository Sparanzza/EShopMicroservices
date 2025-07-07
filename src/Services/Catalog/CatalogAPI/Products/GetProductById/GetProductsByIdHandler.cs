using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using CatalogAPI.Products.Exceptions;
using Marten;

namespace CatalogAPI.Products.GetProductById;

public record GetProductsByIdQuery(Guid Id) : IQuery<GetProductsByIdResult>;

public record GetProductsByIdResult(Product Product);

public class GetProductsByIdQueryHandler(IDocumentSession session, ILogger<GetProductsByIdQueryHandler> logger)
    : IQueryHandler<GetProductsByIdQuery, GetProductsByIdResult>
{
    public async Task<GetProductsByIdResult> Handle(GetProductsByIdQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsByIdQueryHandler handler called {@Query}", query);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        return product is null
            ? throw new ProductNotFoundException(query.Id)
            : new GetProductsByIdResult(product);
    }
}