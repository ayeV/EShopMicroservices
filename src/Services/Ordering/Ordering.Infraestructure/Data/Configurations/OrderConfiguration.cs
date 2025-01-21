using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Ordering.Infraestructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasConversion(
                orderId => orderId.Value,
                dbId => OrderId.Of(dbId));

            builder.HasOne<Customer>()
                .WithMany()
            .HasForeignKey(x => x.CustomerId);

            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId);

            builder.ComplexProperty(
                o => o.OrderName, nameBuilder =>
                {
                    nameBuilder.Property(x => x.Value)
                        .HasColumnName(nameof(Order.OrderName))
                        .HasMaxLength(50).IsRequired();
                });

            builder.ComplexProperty(
                o => o.ShippingAddress, shippingAddressBuilder =>
                {
                    shippingAddressBuilder.Property(x => x.FirstName)
                                           .HasMaxLength(50);


                    shippingAddressBuilder.Property(x => x.LastName)
                                           .HasMaxLength(50);

                    shippingAddressBuilder.Property(x => x.AddressLine)
                                           .HasMaxLength(100);

                    shippingAddressBuilder.Property(x => x.EmailAddress)
                                           .HasMaxLength(20);

                    shippingAddressBuilder.Property(x => x.Country)
                                           .HasMaxLength(50);

                    shippingAddressBuilder.Property(x => x.State)
                                           .HasMaxLength(50);


                    shippingAddressBuilder.Property(x => x.ZipCode)
                                           .HasMaxLength(4);

                });

            builder.ComplexProperty(
                o => o.BillingAddress, billingAddressBuilder =>
                {
                    billingAddressBuilder.Property(x => x.FirstName)
                                           .HasMaxLength(50);


                    billingAddressBuilder.Property(x => x.LastName)
                                           .HasMaxLength(50);

                    billingAddressBuilder.Property(x => x.AddressLine)
                                           .HasMaxLength(100);

                    billingAddressBuilder.Property(x => x.EmailAddress)
                                           .HasMaxLength(20);

                    billingAddressBuilder.Property(x => x.Country)
                                           .HasMaxLength(50);

                    billingAddressBuilder.Property(x => x.State)
                                           .HasMaxLength(50);


                    billingAddressBuilder.Property(x => x.ZipCode)
                                           .HasMaxLength(4);

                });


            builder.ComplexProperty(
               o => o.Payment, paymentBuilder =>
               {
                   paymentBuilder.Property(x => x.CardNumber)
                                          .HasMaxLength(20).IsRequired();


                   paymentBuilder.Property(x => x.CVV)
                                          .HasMaxLength(3).IsRequired();

                   paymentBuilder.Property(x => x.Expiration)
                                          .HasMaxLength(100).IsRequired();

                   paymentBuilder.Property(x => x.PaymentMethod);

               });

            builder.Property(x => x.Status).HasDefaultValue(OrderStatus.Draft)
                .HasConversion(s => s.ToString(),
                               dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus),dbStatus));

            builder.Property(x => x.TotalPrice);

        }
    }
}
