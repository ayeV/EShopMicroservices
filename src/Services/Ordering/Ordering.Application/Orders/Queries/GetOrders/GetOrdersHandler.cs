

namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersHandler(IApplicationDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersResult>
    {
        public async Task<GetOrdersResult> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .OrderBy(o=>o.OrderName.Value)
                .AsNoTracking()
                .Skip(request.PaginationRequest.PageSize * request.PaginationRequest.PageIndex)
                .Take(request.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);
          var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);


            return new GetOrdersResult(new PaginatedResult<OrderDto>(
                request.PaginationRequest.PageIndex,
                request.PaginationRequest.PageSize,
                totalCount,
                orders.ToOrderDto()));
        }
    }
}
