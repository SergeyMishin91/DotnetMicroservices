using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;

        public BasketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<BasketModel> GetBasket(string userName)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/api/v1/Basket/{userName}");
            return await httpResponseMessage.ReadContentAs<BasketModel>();
        }
    }
}
