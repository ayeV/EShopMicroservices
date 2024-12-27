using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupones { get; set; } = default!;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Amount = 10, Description = "Samsung Discount", ProductName = "Samguns", Id = 1 },
                new Coupon { Amount = 30, Description = "iphone Discount", ProductName = "iphone", Id = 2 }
            );
        }
    }
}
