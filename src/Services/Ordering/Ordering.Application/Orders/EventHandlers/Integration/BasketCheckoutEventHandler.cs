namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event Handled: {IntegrationEvent}", context.GetType().Name);

            var command = MapToCreateOrderCommand(context.Message);
             await sender.Send(command);

            throw new NotImplementedException();
        }

        private CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent message)
        {
            
            var addressDto = new AddressDto(message.FirstName,message.LastName,message.EmailAddress,message.AddressLine,
                message.Country, message.State, message.ZipCode);

            var payment = new PaymentDto(message.CardName,message.CardNumber,message.Expiration,message.CVV,"Credit card");
            var orderId = Guid.NewGuid();
            var orderDto = new OrderDto
            (
                orderId,
                message.CustomerId,
                message.UserName,
                addressDto,
                addressDto,
                payment,
                OrderStatus.Pending,
                [ 
                    new OrderItemDto (orderId, new Guid("1443df53-7e17-4dd0-ad67-908eccd10914"),2,500), 
                    new OrderItemDto(orderId,new Guid("36fb58d7-1e2c-4ff4-8502-eb2e70ac6e2c"),3,200) 
                ]
            );
            return new CreateOrderCommand(orderDto);
        }
    }
}
