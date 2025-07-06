using CatalogAPI.Models;
using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.GetProductByCategory;

public record GetProductByCategoryResponse(IEnumerable<Product> Products);

public class GetProductsByCategoryEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));
                return Results.Ok(result.Adapt<GetProductByCategoryResponse>());
            })
            .WithSummary("Get products by category")
            .WithDescription("Retrieves products by category in the catalog.")
            .WithName("GetProductByCategory")
            .WithTags("Products")
            .Produces<GetProductByCategoryResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}