using Basket.API.Data;
using Discount.Grpc;

namespace Basket.API.Basket.GetBasket;

public record GetBasketQuery(string UserName) : IQuery<GetBaskteResult>;

public record GetBaskteResult(ShoppingCart Cart);

public class GetBasketQueryHandler(
    IBasketRepository repository,
    DiscountProtoService.DiscountProtoServiceClient discountProto) : IQueryHandler<GetBasketQuery, GetBaskteResult>
{
    public async Task<GetBaskteResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var result = await repository.GetBasketAsync(query.UserName, cancellationToken);
        return new GetBaskteResult(result);
    }
}