namespace Basket.API.Basket.GetBasket;

public record GetBasketResponse(ShoppingCart Cart);

public class GetBasketEndPoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            
            var result = await sender.Send(new GetBasketQuery(userName));
            return Results.Ok(result.Adapt<GetBasketResponse>());
        })
        .WithName("GetBasket")
        .WithTags("Basket");
    }
}