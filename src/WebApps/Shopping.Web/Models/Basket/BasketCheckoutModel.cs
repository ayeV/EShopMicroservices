namespace Shopping.Web.Models.Basket
{
    public class BasketCheckoutModel
    {
        public string UserName { get; set; } = default!;
        public Guid CustomerId { get; set; } = default!;
        public decimal TotalPrice { get; set; } = default!;

        //Shipping and billing address
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string EmailAddress { get; } = default!;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;

        //Payment
        public string? CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;
    }

    public record CheckoutBasketRequest(BasketCheckoutModel BasketCheckoutDto);

    public record CheckoutBasketResponse(bool IsSuccess);

}
