using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger,
        IPublishEndpoint publish,
        IFeatureManager featureManager) 
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled: {DomainEvent}", domainEvent.GetType().Name);

            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = domainEvent.Order.ToOrderDto();

                await publish.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }

        
        }
    }
}
