using BuildingBlocks.CQRS;
using CatalogAPI.Products.GetProducts;

namespace CatalogAPI.Products.UpdateProduct;

public record UpdateProductRequest(
    Guid Id,
    string Name,
    string Description,
    List<string> Categories,
    string ImageFile,
    decimal Price
) : ICommand<UpdateProductResponse>;

public record UpdateProductResponse(bool IsSuccess);

public class UpdateProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                return Results.Ok(result.Adapt<UpdateProductResponse>());
            })
            .WithSummary("Update Product")
            .WithDescription("Update product details by providing the product ID and updated information.")
            .WithName("Update Products")
            .WithTags("Products")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}