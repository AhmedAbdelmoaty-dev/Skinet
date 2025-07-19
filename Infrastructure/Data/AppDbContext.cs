using Microsoft.EntityFrameworkCore;
using Domain.Entites;
using Infrastructure.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Infrastructure.IdentityEntities;
using Microsoft.AspNetCore.Identity;
namespace Infrastructure.Data
{
    public class AppDbContext:IdentityDbContext<AppUser,IdentityRole,string>
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly);
        }
        public DbSet<Product>Products { get; set; }
        public DbSet<Address> Addresses { get; set; }
    }
}
