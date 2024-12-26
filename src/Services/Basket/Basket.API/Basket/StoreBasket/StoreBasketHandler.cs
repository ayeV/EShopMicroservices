

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart.Items.Count()).GreaterThan(0).WithMessage("Cart cannot be empty");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("Username cannot be null");
        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            var shoppingCart = command.Cart;

            await repository.StoreBasket(shoppingCart, cancellationToken);


            return new StoreBasketResult(shoppingCart.UserName);
        }
    }
}
