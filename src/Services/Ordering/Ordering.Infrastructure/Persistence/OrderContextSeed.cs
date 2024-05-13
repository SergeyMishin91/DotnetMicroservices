using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

// public interface IOrderContextSeed
// {
//     Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger);
// }

public class OrderContextSeed
{
    public static async Task SeedDataAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        await using (orderContext)
        {
            await orderContext.Database.MigrateAsync();
            if (!orderContext.Orders.Any())
            {
                orderContext.Orders.AddRange(GetPreconfiguredOrders());
                await orderContext.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(OrderContext).Name);
            }
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
            {
                new() {
                    UserName = "swn", 
                    TotalPrice = 350, 
                    FirstName = "Mehmet", 
                    LastName = "Ozkaya", 
                    EmailAddress = "ezozkme@gmail.com", 
                    AddressLine = "Bahcelievler", 
                    Country = "Turkey",
                    State = "state",
                    ZipCode = "zipCode",
                    CardName = "cardName",
                    CardNumber = "cardNumber",
                    Expiration = "09/25",
                    CVV = "041",
                    PaymentMethod = 1
                }
            };
    }
}
