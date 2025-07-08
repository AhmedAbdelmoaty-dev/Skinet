
using Application.Contracts.Services;
using Domain.Entites;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IConnectionMultiplexer redis;
        private readonly IDatabase _database;
        public CartService(IConnectionMultiplexer connection)
        {
            redis = connection;
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCartAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<ShoppingCart> GetCartAsync(string key)
        {
         var cart=  await  _database.StringGetAsync(key);
           return cart.IsNullOrEmpty ? null : JsonSerializer.Deserialize<ShoppingCart>(cart);
        }

        public async Task<ShoppingCart> SetCartAsync(ShoppingCart cart)
        {
         var result = _database.StringSet(cart.Id, JsonSerializer.Serialize(cart),TimeSpan.FromDays(30));
            if (!result) return null;
            return await GetCartAsync(cart.Id);
        }
    }
}
