using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.GetProducts;

// public record GetProductsQuery : IQuery<GetProductsResult>;
public record GetProductByIdResponse(Product Product);

public class GetProductsByIdEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByIdQuery(id));
                return Results.Ok(result.Adapt<GetProductByIdResponse>());
            })
            .WithSummary("Get product by Id")
            .WithDescription("Retrieves a product by Id in the catalog.")
            .WithName("GetProductById")
            .WithTags("Products")
            .Produces<GetProductsResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}