using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services.Interfaces;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        public Task<BasketModel> GetBasket(string userName)
        {
            throw new NotImplementedException();
        }
    }
}
