namespace Basket.API.Basket.StoreBasket;

public record StoreBasketCommand(ShoppingCart Cart): ICommand<StoreBasketResult>;

public record StoreBasketResult(string userName);

public class StoreBasketValidator : AbstractValidator<StoreBasketCommand>
{
    public StoreBasketValidator()
    {
        RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
        RuleFor(x => x.Cart.UserName).NotNull().WithMessage("Username is required");
    }
}

public class StoreBasketCommandHandler(IBasketRepository repository): ICommandHandler<StoreBasketCommand, StoreBasketResult>
{
    public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
    {
        ShoppingCart cart = command.Cart;

        await repository.StoreBasket(cart, cancellationToken);

        return new StoreBasketResult(command.Cart.UserName);
    }
}