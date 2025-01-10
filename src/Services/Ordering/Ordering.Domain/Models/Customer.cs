namespace Ordering.Domain.Models
{
    public class Customer :Entity<CustomerId>
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

        public static Customer Create(CustomerId customerId, string name, string email)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(email);

            return new Customer { Id = customerId, Email = email, Name = name };
        }    
    }
}
