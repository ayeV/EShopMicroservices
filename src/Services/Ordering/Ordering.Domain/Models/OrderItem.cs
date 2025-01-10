namespace Ordering.Domain.Models
{
    public class OrderItem :Entity<OrderItemId>
    {

        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price) 
        {
            Id = OrderItemId.Of(Guid.NewGuid());
            OrderId = orderId.Value;
            ProductId = productId.Value;
            Quantity = quantity;
            Price = price;
        }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
