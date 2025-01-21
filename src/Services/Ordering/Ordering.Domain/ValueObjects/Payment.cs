namespace Ordering.Domain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string Expiration { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public int PaymentMethod { get; set; } = default!;

        protected Payment()
        {
            
        }

        private Payment(string? cardName, string cardNumber, string expirationDate, string cvv, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expirationDate;
            CVV = cvv;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string? cardName, string cardNumber, string expirationDate, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrEmpty(cardNumber);
            ArgumentException.ThrowIfNullOrEmpty(expirationDate);
            ArgumentException.ThrowIfNullOrEmpty(cvv);
            ArgumentOutOfRangeException.ThrowIfNotEqual(cvv.Length, 3);


            return new Payment(cardName,cardNumber,expirationDate,cvv,paymentMethod);
        }
    }
}
