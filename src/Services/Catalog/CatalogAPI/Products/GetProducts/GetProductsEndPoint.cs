using BuildingBlocks.CQRS;
using CatalogAPI.Models;

namespace CatalogAPI.Products.GetProducts;

public record GetProductsRequest(int? PageNumber = 1, int? PageSize = 10);
public record GetProductsResponse(IEnumerable<Product> Products);

public class GetProductsEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender sender) =>
            {
                var result = await sender.Send(request.Adapt<GetProductsQuery>());
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