

using Application.Contracts.Seeders;
using Domain.Entites;
using System.Text.Json;

namespace Infrastructure.Data.Seeders
{
    internal class DeliveryMethodSeeder:IDeliveryMethodSeeder
    {
        private readonly AppDbContext _context;
        public DeliveryMethodSeeder(AppDbContext context)
        {
            _context = context;
        }
        public async Task SeedDataAsync()
        {
            if (!_context.DeliveryMethods.Any())
            {
                var methodsJson = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/delivery.json");
                var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(methodsJson);
                if (methods == null) return;

                _context.DeliveryMethods.AddRange(methods);
                await _context.SaveChangesAsync();
            }
        }
    }
   
}
