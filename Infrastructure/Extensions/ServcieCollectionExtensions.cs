using Application.Contracts.Repositories;
using Application.Contracts.Services;
using Infrastructure.Data;
using Infrastructure.Data.SeedData;
using Infrastructure.Data.Seeders;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Infrastructure.Extensions
{
    public static class ServcieCollectionExtensions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            
            services.AddScoped<IProductSeeder, ProductSeeder>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var connectionString = configuration.GetConnectionString("Redis");
                if (connectionString == null) throw new Exception("cannot get redis connection string");
                var configurationOption = ConfigurationOptions.Parse(connectionString, true);
                return ConnectionMultiplexer.Connect(configurationOption);

            });
            services.AddSingleton<ICartService, CartService>(); 


        }
    }
}
