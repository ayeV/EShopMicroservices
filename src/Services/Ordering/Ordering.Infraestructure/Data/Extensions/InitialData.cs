using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System.Net.WebSockets;

namespace Ordering.Infraestructure.Data.Extensions
{
    internal static class InitialData
    {
        //todo make ids of customer and product not random
        public static IEnumerable<Customer> Customers =>
            [
                Customer.Create(CustomerId.Of(new Guid("a16a8472-c2b4-4296-b5a6-b1d34eb49c51")),"Ayelen","ayelen@email.com"),
                Customer.Create(CustomerId.Of(new Guid("ee22f18c-be18-40fd-be97-7498d28a4258")),"Maria","maria@email.com")

            ];

        public static IEnumerable<Product> Products =>
            [
                Product.Create(ProductId.Of(new Guid("36fb58d7-1e2c-4ff4-8502-eb2e70ac6e2c")),"Heladera Samsung",2000),
                Product.Create(ProductId.Of(new Guid("1443df53-7e17-4dd0-ad67-908eccd10914")),"Cafetera Oster",1500),
            ];

        public static IEnumerable<Order> OrdersWithItems
        {
            get
            {
                var address1 = Address.Of("jhon", "Doe", "jd@email.com", "Praga", "UA", "ZX", "1234");
                var address2 = Address.Of("juana", "arco", "juanad@email.com", "Paris", "FR", "ZX", "1234");

                var payment1 = Payment.Of("Visa", "12345678910", "02/27", "123", 1);
                var payment2 = Payment.Of("Mastercard", "12345678910", "02/27", "123", 2);

                var order1 = Order.Create(OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("a16a8472-c2b4-4296-b5a6-b1d34eb49c51")),
                    OrderName.Of("Jhonn"),
                    address1,
                    address1,
                    payment1,
                    Domain.Enums.OrderStatus.Pending);
                order1.Add(ProductId.Of(new Guid("36fb58d7-1e2c-4ff4-8502-eb2e70ac6e2c")), 2, 500);

                var order2 = Order.Create(OrderId.Of(Guid.NewGuid()),
                    CustomerId.Of(new Guid("ee22f18c-be18-40fd-be97-7498d28a4258")),
                    OrderName.Of("Juana"),
                    address2,
                    address2,
                    payment2,
                    Domain.Enums.OrderStatus.Cancelled);
                order1.Add(ProductId.Of(new Guid("1443df53-7e17-4dd0-ad67-908eccd10914")), 2, 500);

                return new List<Order> { order1, order2 };
            }
        }
    }
}