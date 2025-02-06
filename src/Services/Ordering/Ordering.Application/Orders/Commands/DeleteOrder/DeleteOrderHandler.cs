

namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderHandler(IApplicationDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderResult>
    {
        public async Task<DeleteOrderResult> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var id = OrderId.Of(request.OrderId);
            var orderId = await dbContext.Orders.FindAsync([id],cancellationToken);
            if (orderId is null)
            {
                throw new OrderNotFoundException(request.OrderId);
            }

            dbContext.Orders.Remove(orderId);
            await dbContext.SaveChangesAsync(cancellationToken);
            return new DeleteOrderResult(true);
        }
    }
}
