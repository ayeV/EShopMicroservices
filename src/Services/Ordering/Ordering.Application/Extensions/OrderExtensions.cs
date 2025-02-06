namespace Ordering.Application.Extensions
{
    public static class OrderExtensions
    {
        public static List<OrderDto> ToOrderDto(this List<Order> orders)
        {
            List<OrderDto> dtos = new();
            foreach (var order in orders)
            {
                var orderDto = new OrderDto
                (
                    order.Id.Value,
                    order.CustomerId.Value,
                    order.OrderName.Value,
                    new AddressDto
                    (
                        order.ShippingAddress.FirstName,
                        order.ShippingAddress.LastName,
                        order.ShippingAddress.EmailAddress,
                        order.ShippingAddress.AddressLine,
                        order.ShippingAddress.Country,
                        order.ShippingAddress.State,
                        order.ShippingAddress.ZipCode
                    ),
                    new AddressDto
                    (
                        order.BillingAddress.FirstName,
                        order.BillingAddress.LastName,
                        order.BillingAddress.EmailAddress,
                        order.BillingAddress.AddressLine,
                        order.BillingAddress.Country,
                        order.BillingAddress.State,
                        order.BillingAddress.ZipCode
                    ),
                    new PaymentDto
                    (
                        order.Payment.CardName,
                        order.Payment.CardNumber,
                        order.Payment.Expiration,
                        order.Payment.CVV,
                        "Credit card"
                    ),
                    order.Status,
                    order.OrderItems.Select(x => new OrderItemDto
                    (
                        x.OrderId.Value,
                        x.ProductId.Value,
                        x.Quantity,
                        x.Price
                       
                    )).ToList()
                );
                dtos.Add(orderDto);
            }
            return dtos;
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            return new OrderDto(
                order.Id.Value,
                order.CustomerId.Value,
                order.OrderName.Value,
                new AddressDto
                (
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode
                ),
                 new AddressDto
                (
                    order.ShippingAddress.FirstName,
                    order.ShippingAddress.LastName,
                    order.ShippingAddress.EmailAddress,
                    order.ShippingAddress.AddressLine,
                    order.ShippingAddress.Country,
                    order.ShippingAddress.State,
                    order.ShippingAddress.ZipCode
                ),
                 new PaymentDto
                (
                    order.Payment.CardName,
                    order.Payment.CardNumber,
                    order.Payment.Expiration,
                    order.Payment.CVV,
                    "Credit card"
                ),
                 order.Status,
                    order.OrderItems.Select(x => new OrderItemDto
                    (
                        x.OrderId.Value,
                        x.ProductId.Value,
                        x.Quantity,
                        x.Price
                    )).ToList()

                );

        }
    }

    
}
