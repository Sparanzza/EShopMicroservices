using BuildingBlocks.CQRS;
using CatalogAPI.Models;
using CatalogAPI.Products.Exceptions;
using Marten;

namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;

public record GetProductsByCategoryResult(IEnumerable<Product> Products);

public class GetProductsByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductsByCategoryQueryHandler> logger)
    : IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
{
    public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery query, CancellationToken cancellationToken)
    {
        logger.LogInformation("GetProductsByCategoryQueryHandler handler called {@Query}", query);
        var products = await session.Query<Product>()
            .Where( p => p.Categories.Contains(query.Category))
            .ToListAsync(cancellationToken);
        return products is null
            ? throw new ProductNotFoundException(query.Category)
            : new GetProductsByCategoryResult(products);
    }
}