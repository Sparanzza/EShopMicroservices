namespace Basket.API.Basket.GetBasket;

public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

public record StoreBasketResult(string Username);

public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketCommandValidator()
    {
        RuleFor(x => x.ShoppingCart).NotNull();
        RuleFor(x => x.ShoppingCart.UserName).NotEmpty();
    }
}

public class StoreBasketCommandHandler : ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        // TBD
        ShoppingCart cart = command.ShoppingCart;
        return Task.FromResult(new StoreBasketResult("swn"));
    }
}
