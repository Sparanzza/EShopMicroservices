namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBaskteResult>;

public record GetBaskteResult(ShoppingCart ShoppingCart);

public class GetBasketQueryHandler : IQueryHandler<GetBasketQuery, GetBaskteResult>
{
    public Task<GetBaskteResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        // TBD
        // var basket = awiat _repository.GetBasketAsync(request, cancellationToken);
        return Task.FromResult(new GetBaskteResult(new ShoppingCart("swn")));
    }
}