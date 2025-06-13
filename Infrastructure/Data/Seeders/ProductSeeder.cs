using Domain.Entites;
using Infrastructure.Data.SeedData;
using System.Text.Json;

namespace Infrastructure.Data.Seeders
{
    internal class ProductSeeder(AppDbContext context):IProductSeeder
    {
        
        public  async Task SeedDataAsync()
        {
            if (!context.Products.Any())
            {
                var productsJson = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsJson);
                if (products == null) return;
                
                 context.Products.AddRange(products);
              await  context.SaveChangesAsync();
                
            }
        }
    }
}
