using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redis;

        public BasketRepository(IDistributedCache redis)
        {
            _redis = redis ?? throw new ArgumentNullException(nameof(redis));
        }

        public async Task DeleteByUserName(string userName)
        {
            await _redis.RemoveAsync(userName);
        }

        public async Task<ShoppingCart> GetByUserName(string userName)
        {
            var shoppingCart = await _redis.GetStringAsync(userName);
            if (string.IsNullOrEmpty(shoppingCart)) { return null; }

            return JsonSerializer.Deserialize<ShoppingCart>(shoppingCart);
        }

        public async Task<ShoppingCart> Update(ShoppingCart shoppingCart)
        {
            await _redis.SetStringAsync(shoppingCart.UserName, JsonSerializer.Serialize(shoppingCart));
            return await GetByUserName(shoppingCart.UserName);
        }
    }
}
