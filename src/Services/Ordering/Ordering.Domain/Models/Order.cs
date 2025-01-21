namespace Ordering.Domain.Models
{
    public class Order :Aggregate<OrderId>
    {
        private List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerId CustomerId { get; set; } = default!;
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus Status { get; private set; } = OrderStatus.Pending;
        public decimal TotalPrice
        {
            get => OrderItems.Sum(x => x.Price * x.Quantity);
            private set { }
        }

        public static Order Create(OrderId orderId, CustomerId customerId, 
            OrderName orderName,Address shippingAddres,Address billingAddress,Payment payment,OrderStatus orderStatus)
        {
            var order = new Order
            {
                Id = orderId,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddres,
                BillingAddress = billingAddress,
                Payment = payment,
                Status = orderStatus
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));
            return order;
        }

        public void Update(OrderName orderName,
           Address shippingAddres, Address billingAddress, Payment payment, OrderStatus orderStatus)
        {
            OrderName = orderName;
            ShippingAddress = shippingAddres;
            BillingAddress = billingAddress;
            Payment = payment;
            Status = orderStatus;
            AddDomainEvent(new OrderUpdatedEvent(this));
        }

        public void Add(ProductId productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(quantity, 0);
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(price);

            var orderItem = new OrderItem(Id,productId,quantity, price);
            _orderItems.Add(orderItem);
        }

        public void Remove(ProductId productId)
        {
            var orderItem = _orderItems.FirstOrDefault(x => x.ProductId == productId);
            if (orderItem != null) 
            {
                _orderItems.Remove(orderItem);
            }
        }
    }
}
