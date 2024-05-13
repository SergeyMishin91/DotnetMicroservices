using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Repositories
{
    public interface IBasketRepository
    {
        Task<ShoppingCart?> GetBasketAsync(string username);

        Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket);

        Task DeleteBasketAsync(string username);
    }

    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task<ShoppingCart?> GetBasketAsync(string username)
        {
            var basket = await _redisCache.GetStringAsync(username);

            if(string.IsNullOrWhiteSpace(basket))
            {
                return null;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
        {
            await _redisCache.SetStringAsync(basket.Username,
                JsonSerializer.Serialize(basket));

            return await GetBasketAsync(basket.Username);
        }

        public async Task DeleteBasketAsync(string username)
        {
            await _redisCache.RemoveAsync(username);
        }
    }
}
