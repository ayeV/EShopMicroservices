namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record class GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameResult>;

    public record GetOrdersByNameResult(IEnumerable<OrderDto> Orders);


}
