using FluentValidation;

namespace Ordering.Application.Orders.Commands.UpdateOrder
{
   public record UpdateOrderCommand(OrderDto OrderDto) : ICommand<UpdateOrderResult>;
    public record UpdateOrderResult(bool IsSuccess);

    public class UpdateCommandValidator :AbstractValidator<UpdateOrderCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.OrderDto.Id).NotNull();
            RuleFor(x => x.OrderDto.OrderName).NotNull().NotEmpty();
            RuleFor(x => x.OrderDto.OrderItems).Must(x => x.Count > 0);
        }
    }
}
