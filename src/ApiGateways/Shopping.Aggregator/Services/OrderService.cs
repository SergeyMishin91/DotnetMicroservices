using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

namespace Shopping.Aggregator.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var httpResponseMessage = await _httpClient.GetAsync($"/api/v1/Order/{userName}");
            return await httpResponseMessage.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
