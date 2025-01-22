using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.Commands.DeleteOrder;

namespace Ordering.API.Endpoints
{

    public record DeleteOrderRequest(Guid Id);

    public record DeleteOrderResponse(bool Succeeded);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders/{Id}", async (DeleteOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<DeleteOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();
                return response.Succeeded ? Results.Ok(response) : Results.NotFound(response);
            })
                .WithName("DeleOrder")
                .Produces<DeleteOrderResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .WithSummary("DeleteOrder")
                .WithDescription("DeleteOrder");
        }
    }
}
