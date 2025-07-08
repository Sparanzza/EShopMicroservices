using Basket.API.Data;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBaskteResult>;

public record GetBaskteResult(ShoppingCart ShoppingCart);

public class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBaskteResult>
{
    public async Task<GetBaskteResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetBasketAsync(query.UserName, cancellationToken);
        return result.Adapt<GetBaskteResult>();
    }
}