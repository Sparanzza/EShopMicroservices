namespace Basket.API.Basket.GetBasket;


public record DeleteBasketRequest(string UserName);

public record DeleteBasketResponse(bool IsSuccess);

public class DeleteBasketEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var command = new DeleteBasketCommand(userName);
                var result = await sender.Send(command);
                return Results.Ok(result.Adapt<DeleteBasketResponse>());
            })
            .WithTags("Basket")
            .WithName("DeleteBasket")
            .WithSummary("Delete Basket")
            .WithDescription("Deletes the shopping cart for a user.")
            .Produces<DeleteBasketResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent);
    }
}