using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.API.Endpoints
{

    public record GetOrdersByCustomerResponse(List<OrderDto> Orders);
    public class GetOrdersByCustomer : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders/customer/{customerId}", async (Guid customerId,ISender sender) => 
            {
                var query = new GetOrdersByCustomerQuery(customerId);

                var result = await sender.Send(query);
                var response = result.Adapt<GetOrdersByCustomerResponse>();

                return Results.Ok(response);


            }).WithName("GetOrdersByCustomer")
                .Produces<GetOrdersByCustomerResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("GetOrdersByCustomer")
                .WithDescription("GetOrdersByCustomer"); ;
        }
    }
}
