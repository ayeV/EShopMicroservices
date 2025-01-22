
namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
    {
        public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o=>o.OrderItems)
                .Where(o=> o.CustomerId.Value == query.CustomerId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return new GetOrdersByCustomerResult(orders.ToOrderDto());
        }
    }
}
