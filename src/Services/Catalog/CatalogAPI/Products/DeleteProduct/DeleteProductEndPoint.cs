namespace CatalogAPI.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndPoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                return Results.Ok(result.Adapt<DeleteProductResponse>());
            })
            .WithSummary("Delete Product")
            .WithDescription("Delete product details by providing the product ID and updated information.")
            .WithName("Delete Product")
            .WithTags("Products")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}