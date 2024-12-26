namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketRequest(ShoppingCart Cart);
    public record StoreBasketResponse(string UserName);
    public class StoreBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var result = await sender.Send(request.Adapt<StoreBasketCommand>());

                var response =  result.Adapt<StoreBasketResponse>();
                return Results.Created($"/basket/{response.UserName}",response);

            })
            .WithName("Create basket")
            .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create basket")
            .WithDescription("Create basket");
        }
    }
}
