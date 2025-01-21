using BuildingBlocks.CQRS;
using FluentValidation;
using Ordering.Application.Dtos;

namespace Ordering.Application.Orders.Commands.CreateOrder;

public record CreateOrderCommand(OrderDto Order) :ICommand<CreateOrderResult>;
public record CreateOrderResult(Guid Id);

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Order).NotNull();
        RuleFor(x => x.Order.OrderName).NotNull().NotEmpty();
        RuleFor(x => x.Order.OrderItems).Must(x => x.Count > 0);
    }
}
