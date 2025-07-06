using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.GetProducts;

public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());
                return Results.Ok(result.Adapt<GetProductsResponse>());
            })
            .WithSummary("Get all products")
            .WithDescription("Retrieves a list of all products in the catalog.")
            .WithName("GetProducts")
            .WithTags("Products")
            .Produces<GetProductsResult>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}