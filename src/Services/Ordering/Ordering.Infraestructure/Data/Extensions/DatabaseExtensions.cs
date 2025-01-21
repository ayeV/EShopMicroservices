
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infraestructure.Data.Extensions
{
    public static class DatabaseExtensions
    {
        public static async Task InitialiseDbAsync(this WebApplication application)
        {
            using var scope = application.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }

        private static async Task SeedAsync(ApplicationDbContext context)
        {
           await SeedCustomerAsync(context);
           await SeedProductAsync(context);
           await SeedOrdersWithItemsAsync(context);
        }

        private static async Task SeedOrdersWithItemsAsync(ApplicationDbContext context)
        {
            if (!await context.Orders.AnyAsync())
            {
                await context.Orders.AddRangeAsync(InitialData.OrdersWithItems);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductAsync(ApplicationDbContext context)
        {
            if (! await context.Products.AnyAsync())
            {
                context.Products.AddRange(InitialData.Products);
                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedCustomerAsync(ApplicationDbContext context)
        {
            if (!await context.Customers.AnyAsync())
            {
                context.Customers.AddRange(InitialData.Customers);
                await context.SaveChangesAsync();
            }
        }
    }
}
